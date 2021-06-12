using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 100f;
    public float maxVelocity = 10f;
    private Rigidbody rb;

    public GameObject cam;
    public bool activateCamera = false;
    public float c_xOffset, c_yOffset, c_zOffset;

    void Start()
    {
        cam = GameObject.Find("Camera");
        rb = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        //checkCam();
        if(Input.GetKeyDown("space"))
        {
            //activateCam();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = cam.transform.TransformDirection(movement);
        movement.y = 0.0f;

        rb.AddForce(movement * speed);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    private void checkCam()
    {
        if(activateCamera)
        {
            cam.transform.position = transform.position + new Vector3(c_xOffset, c_yOffset, c_zOffset);
            cam.transform.LookAt(transform.position);
        }
    }

    private void activateCam()
    {
        activateCamera = !activateCamera;
    }
}
