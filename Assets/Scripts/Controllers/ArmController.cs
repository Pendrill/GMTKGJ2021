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

    //start
    public override void InitPart()
    {
       
        currentDelay = moveDelay;
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
            
    }

    //physics update
    public override void ControlPhysics()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            currentDelay -= Time.deltaTime;
        }
        else
        {
            currentDelay = moveDelay;
        }

        if(currentDelay <= 0)
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

    public void updateArmRotation()
    {
        if (Input.GetKey(inputReader.rotateRight))
            transform.Rotate(Vector3.up * speed * Time.deltaTime);

        if (Input.GetKey(inputReader.rotateLeft))
            transform.Rotate(-Vector3.up * speed * Time.deltaTime);
    }
}
