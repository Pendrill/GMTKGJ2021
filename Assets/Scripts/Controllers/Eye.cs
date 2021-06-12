using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Eye : MonoBehaviour
{
    // Start is called before the first frame update

    //Movement variables
    public float speed = 100f;
    public float maxVelocity = 10f;
    private Rigidbody rb;

    //Third person camera variables
    public GameObject cam;
    public bool activateCamera = false;
    public float c_xOffset, c_yOffset, c_zOffset;

    //First person camera variables
    public GameObject FirstPersonUI;
    public bool isFirstPerson = false;
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;
    GameObject currentSeenObject;
    

    //Flashlight variables
    public bool flashLightOn = false;
    public Light flashlight;


    //
    private bool pauseEye = false;

    void Start()
    {
        cam = GameObject.Find("Camera");
        FirstPersonUI = GameObject.Find("FirstPersonUI");
        FirstPersonUI.SetActive(false);
        rb = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseEye) return;
        //checkCam();
        if (isFirstPerson)
        {
            updateFirstPersonCam();
            sendRayCast();
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

        checkFlashLight();
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

    private void sendRayCast()
    {
        RaycastHit hit;
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Debug.Log(objectHit.tag);
            if(objectHit.tag == "Discover")
            {
                if(currentSeenObject != objectHit.gameObject)
                {
                    if (currentSeenObject)
                    {
                        currentSeenObject.GetComponent<DiscoverableItem>().rayCastLeft();
                    }
                    currentSeenObject = objectHit.gameObject;
                    currentSeenObject.GetComponent<DiscoverableItem>().rayCastHit();
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        setPauseEye(true);
                        UnityEvent m_event = new UnityEvent();
                        m_event.AddListener(finishedAnalysis);
                        currentSeenObject.GetComponent<DiscoverableItem>().analyzed(m_event);
                    }
                }
                
            }
            //objectHit.gameObject.SetActive(false);

            // Do something with the object that was hit by the raycast.
        }
        else
        {
            if (currentSeenObject)
            {
                currentSeenObject.GetComponent<DiscoverableItem>().rayCastLeft();
                currentSeenObject = null;
            }
        }
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
        FirstPersonUI.SetActive(true);
    }

    private void deactivateFirstPerson()
    {
        isFirstPerson = false;
        cam.GetComponent<MouseOrbit>().setTarget(transform);
        FirstPersonUI.SetActive(false);
    }

    private void checkFlashLight()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            flashLightOn = !flashLightOn;
        }
        flashlight.enabled = flashLightOn;

    }

    public void setPauseEye(bool set)
    {
        pauseEye = set;
    }

    public void finishedAnalysis()
    {
        setPauseEye(false);
    }
}
