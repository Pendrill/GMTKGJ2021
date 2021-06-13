using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : PartController
{
    //Movement variables
    public float speed = 100f;
    public float maxVelocity = 10f;

    public float groundDrag = 6f;

    float moveDelay = 0.5f;
    float currentDelay = 0.5f;

    bool temp = true;

    private bool grabbing = false;
    private bool isGrabbing = false;

    Animator animator;

    [SerializeField]
    Transform grabLockTarget;
    //Previous parent for holdable
    private Transform prevParent;
    private Vector3 prevPos;

    //start
    public override void InitPart()
    {
       
        currentDelay = moveDelay;
        animator = GetComponent<Animator>();
    }

    //update
    public override void ControlPart()
    {
        if(temp)
        {
            temp = !temp;
            inputReader.cam.GetComponent<MouseOrbit>().setMouseToRotate(false);
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            updateArmRotation();
        }
        ControlMoveAnimation();
        ControlGrabAnimation();
        ControlGrabUI();
    }

    //physics update
    public override void ControlPhysics()
    {
        if(Input.GetAxis("Vertical") > 0)
        {
            currentDelay -= Time.deltaTime;
        }
        else
        {
            currentDelay = moveDelay;
        }

        if(currentDelay <= 0 && !isGrabbing)
        {
            rb.drag = groundDrag;
            rb.AddForce(Input.GetAxis("Vertical") * transform.forward * speed);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
            if(currentDelay <= -0.3)
            {
                currentDelay = moveDelay;
            }
        }
        
    }

    private void ControlMoveAnimation()
    {
        //Flat velocity excludes vertical velocity
        float flatVelocity = Mathf.Abs(new Vector2(rb.velocity.x, rb.velocity.z).magnitude);
        if (flatVelocity < 0.1f)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Arm_Walk") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("ArmGrab_Idle"))
            {
                animator.SetTrigger("IdleTrigger");
            }
        }
        else if (flatVelocity > 0.1f)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                animator.SetTrigger("WalkTrigger");
            }
        }
    }

    private void ControlGrabAnimation()
    {
        if (inputReader.grabbable && Input.GetKeyDown(inputReader.armUse))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Arm_Walk") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                GameSingleton.Instance.checkpointSystem.ignoreArm = true;
                GetComponent<Rigidbody>().isKinematic = true;
                animator.SetTrigger("GrabTrigger");
                isGrabbing = true;
                prevParent = inputReader.holdable.transform.parent;
                prevPos = inputReader.holdable.transform.localPosition;
                inputReader.holdable.GetComponent<Rigidbody>().isKinematic = true;
                inputReader.holdable.transform.parent = grabLockTarget;
                inputReader.holdable.transform.localPosition = Vector3.zero;
                inputReader.usable.OnUse?.Invoke();
                
                
            }
            else if (isGrabbing && animator.GetCurrentAnimatorStateInfo(0).IsName("ArmGrab_Idle"))
            {
                animator.SetTrigger("ReleaseTrigger");
                isGrabbing = false;
                inputReader.holdable.GetComponent<Rigidbody>().isKinematic = false;
                inputReader.holdable.transform.parent = prevParent;
                //inputReader.holdable.transform.localPosition = prevPos;
                //GameSingleton.Instance.checkpointSystem.curCheckpoint.volume.DisableVolume();
                GetComponent<Rigidbody>().isKinematic = false;
                //GameSingleton.Instance.checkpointSystem.ignoreArm = false;
               // GameSingleton.Instance.checkpointSystem.curCheckpoint.volume.EnableVolume();                
                inputReader.usable.OnRelease?.Invoke();               
            }
        }
    }

    private void ControlGrabUI()
    {
        if (inputReader.grabbable && isGrabbing)
        {
            GameSingleton.Instance.uiManager.SetMessage("Press '" + inputReader.armUse + "' to release " + inputReader.grabbableName);
        }
        else if (inputReader.grabbable && !isGrabbing)
        {
            GameSingleton.Instance.uiManager.SetMessage("Press '" + inputReader.armUse + "' to use " + inputReader.grabbableName);
        }
    }

    public void updateArmRotation()
    {
        if (Input.GetKey(inputReader.rotateRight))
            transform.Rotate(Vector3.up * speed * Time.deltaTime);

        if (Input.GetKey(inputReader.rotateLeft))
            transform.Rotate(-Vector3.up * speed * Time.deltaTime);
    }
}
