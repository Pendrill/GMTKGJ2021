using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyController : PartController
{
    [Header("Movement")]
    [SerializeField]
    float baseMoveSpeed;
    [SerializeField]
    float groundMoveMultiplier;
    [SerializeField]
    float groundDrag = 6f;

    [Header("Grounded")]
    [SerializeField]
    Transform groundCheckSphere;
    [SerializeField]
    float groundDistance;
    [SerializeField]
    LayerMask groundMask;
    bool isGrounded;

    [Header("Jumping")]
    [SerializeField]
    float airDrag = 0f;
    [SerializeField]
    float jumpForce = 5f;
    [SerializeField]
    float fallDrag = 0f;

    [Header("Boosting")]
    [SerializeField]
    BoostState boostState = BoostState.Idle;
    [SerializeField]
    float maxBoost = 10f;
    [SerializeField]
    float boostAccel = 1f;
    [SerializeField]
    float boostGravity = 0.2f;
    [SerializeField]
    float fallGravity = 1.0f;
    [SerializeField]
    float hoverGravity = 0.5f;
    [SerializeField]
    float airMoveMultiplier;
    [SerializeField]
    float boostTime = 2f;
    private float boostTimeRemaining = 2f;

    [Header("UI")]
    [SerializeField]
    Image fuelGauge;

    Animator animator;

    public override void InitPart()
    {
        base.InitPart();
        animator = GetComponent<Animator>();
    }

    public override void ControlPart()
    {
        isGrounded = Physics.CheckSphere(groundCheckSphere.position, groundDistance, groundMask);
        ControlDrag();
        ControlBoost();
        HandleBoost();
        ControlGravity();
        ControlUI();
        ControlMoveAnimation();
    }

    private void ControlUI()
    {
        fuelGauge.fillAmount = boostTimeRemaining / boostTime;
    }

    private void ControlRotation()
    {
        Vector3 projectedForward = Vector3.ProjectOnPlane(inputReader.cam.transform.forward, Vector3.up);
        transform.rotation = Quaternion.LookRotation(projectedForward);
    }

    private void ControlBoost()
    {
        if (Input.GetKey(inputReader.jumpKey))
        {
            if (boostTimeRemaining > 0)
            {
                boostState = BoostState.Boost;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BoostUp"))
                {
                    animator.SetTrigger("BoostUp");
                }
            }
            else
            {
                boostState = BoostState.Hover;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BoostHover"))
                {
                    animator.SetTrigger("BoostHover");
                }
            }
        }
        else
        {
            boostState = BoostState.Idle;
        }
    }

    private void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else if (!isGrounded)
        {
            if (boostState == BoostState.Boost)
            {
                rb.drag = airDrag;
            }
            else if (boostState == BoostState.Idle)
            {
                rb.drag = fallDrag;
            }

        }
    }

    private void ControlGravity()
    {
        if (boostState == BoostState.Boost)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (boostGravity - 1) * Time.deltaTime;
        }
        else if (boostState == BoostState.Hover)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (hoverGravity - 1) * Time.deltaTime;
        }
        else if (!isGrounded && boostState == BoostState.Idle)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallGravity - 1) * Time.deltaTime;
        }
    }

    private void HandleBoost()
    {
        if (boostState == BoostState.Boost && boostTimeRemaining > 0)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }

            if (rb.velocity.y < maxBoost)
            {
                rb.AddForce(transform.up * boostAccel, ForceMode.Acceleration);
            }

            boostTimeRemaining -= Time.deltaTime;
        }
        else if (isGrounded)
        {
            boostTimeRemaining = boostTime;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("BoostUp") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("BoostHover"))
            {
                animator.SetTrigger("BoostLand");
            }
        }
    }

    public override void ControlPhysics()
    {
        ControlRotation();
        if (isGrounded)
        {
            rb.AddForce(inputReader.move_vector *
                baseMoveSpeed * groundMoveMultiplier,
                ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(inputReader.move_vector *
                baseMoveSpeed * airMoveMultiplier,
                ForceMode.Acceleration);
        }
    }

    private void ControlMoveAnimation()
    {
        //Flat velocity excludes vertical velocity
        float flatVelocity = Mathf.Abs(new Vector2(rb.velocity.x, rb.velocity.z).magnitude);
        if (isGrounded && flatVelocity < 0.3f)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("BoostLand"))
            {
                animator.SetTrigger("IdleTrigger");
            }
        }
        else if (isGrounded && flatVelocity > 0.3f)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("BoostLand"))
            {
                animator.SetTrigger("WalkTrigger");
            }
        }
    }

    private enum BoostState
    {
        Idle,
        Boost,
        Hover
    }
}
