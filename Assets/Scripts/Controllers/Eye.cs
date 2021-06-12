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
    public bool isFirstPerson = false;

    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        cam = GameObject.Find("Camera");
        rb = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        //checkCam();
        if (isFirstPerson)
        {
            updateFirstPersonCam();
        }

        if (Input.GetKeyDown("space") && !isFirstPerson)
        {
            activateCam();
        }
        
        if(Input.GetKey(KeyCode.E))
        {
            stopEye();
        }
        if(Input.GetKeyDown(KeyCode.Q) && !isFirstPerson)
        {
            activateFirstPerson();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isFirstPerson)
        {
            deactivateFirstPerson();
        }
    }

    private void FixedUpdate()
    {
        if (isFirstPerson) return;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = cam.transform.TransformDirection(movement);
        movement.y = 0.0f;

        rb.AddForce(movement * speed);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    private void updateFirstPersonCam()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);
        cam.GetComponent<FirstPersonCamera>().updateLocalRotation(Quaternion.Euler(xRotation, yRotation, 0f));
        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
        //transform.eulerAngles = Vector3.zero;
        //transform.Rotate(new Vector3(-mouseY, mouseX, 0), Space.World);
    }

    private void activateCam()
    {
        cam.GetComponent<MouseOrbit>().setTarget(transform);
    }

    private void stopEye()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //transform.Rotate(new Vector3(0f, 0f, 0f));
    }

    private void activateFirstPerson()
    {
        stopEye();
        xRotation = 0;
        yRotation = 0;
        cam.GetComponent<MouseOrbit>().removeTarget();
        transform.localEulerAngles = new Vector3(0, 0, 0);
        cam.GetComponent<FirstPersonCamera>().setCamPosition(transform.position);
        Cursor.lockState = CursorLockMode.Locked;
        isFirstPerson = true;
    }

    private void deactivateFirstPerson()
    {
        isFirstPerson = false;
        cam.GetComponent<MouseOrbit>().setTarget(transform);
    }
}
