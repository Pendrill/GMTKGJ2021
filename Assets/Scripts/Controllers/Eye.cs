using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Eye : PartController
{
    // Start is called before the first frame update

    //Movement variables
    public float speed = 100f;
    public float maxVelocity = 10f;

    //Third person camera variables
    public bool activateCamera = false;

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

    public override void InitPart()
    {
        FirstPersonUI = partUI.transform.GetChild(0).gameObject;
        FirstPersonUI.SetActive(false);
    }

    public override void ControlPart()
    {
        if (pauseEye) return;
        //checkCam();
        if (isFirstPerson)
        {
            updateFirstPersonCam();
            sendRayCast();
        }

        /*
        if (Input.GetKeyDown("space") && !isFirstPerson)
        {
            activateCam();
        }*/

        if (Input.GetKey(inputReader.stopEyeRoll))
        {
            stopEye();
        }
        if (Input.GetKeyDown(inputReader.switchCameraMode) && !isFirstPerson)
        {
            activateFirstPerson();
        }
        else if (Input.GetKeyDown(inputReader.switchCameraMode) && isFirstPerson)
        {
            deactivateFirstPerson();
        }

        checkFlashLight();
    }

    public override void ControlPhysics()
    {
        if (isFirstPerson) return;

        rb.AddForce(inputReader.move_vector * speed);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    private void updateFirstPersonCam()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);
        inputReader.cam.GetComponent<FirstPersonCamera>().updateLocalRotation(Quaternion.Euler(xRotation, yRotation, 0f));
        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
        //transform.eulerAngles = Vector3.zero;
        //transform.Rotate(new Vector3(-mouseY, mouseX, 0), Space.World);
    }

    private void sendRayCast()
    {
        RaycastHit hit;
        Ray ray = inputReader.cam.GetComponent<Camera>().ScreenPointToRay(
            new Vector3(Screen.width/2, Screen.height/2, 0));

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            //Debug.Log(objectHit.tag);
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
                    if (Input.GetKeyDown(inputReader.analyzeKey))
                    {
                        setPauseEye(true);
                        UnityEvent m_event = new UnityEvent();
                        m_event.AddListener(finishedAnalysis);
                        currentSeenObject.GetComponent<DiscoverableItem>().analyzed(m_event);
                    }
                }              
            }
            else
            {
                if (currentSeenObject)
                {
                    currentSeenObject.GetComponent<DiscoverableItem>().rayCastLeft();
                    currentSeenObject = null;
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
        inputReader.cam.GetComponent<MouseOrbit>().setTarget(transform);
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
        inputReader.cam.GetComponent<MouseOrbit>().removeTarget();
        transform.localEulerAngles = new Vector3(0, 0, 0);
        inputReader.cam.GetComponent<FirstPersonCamera>().setCamTarget(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        isFirstPerson = true;
        FirstPersonUI.SetActive(true);
    }

    private void deactivateFirstPerson()
    {
        isFirstPerson = false;
        inputReader.cam.GetComponent<MouseOrbit>().setTarget(transform);
        inputReader.cam.GetComponent<FirstPersonCamera>().DeactivateCam();
        FirstPersonUI.SetActive(false);
    }

    private void checkFlashLight()
    {
        if(Input.GetKeyDown(inputReader.flashlightKey))
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
