using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Uses script from https://answers.unity.com/questions/1257281/how-to-rotate-camera-orbit-around-a-game-object-on.html
/// as a base 
/// </summary>

public class MouseOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 2.0f;
    public float xSpeed = 20.0f;
    public float ySpeed = 20.0f;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    public float distanceMin = 10f;
    public float distanceMax = 10f;
    public float smoothTime = 2f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;
    [SerializeField]
    float startingDistance;

    bool hitting = false;

    bool useMouseToRotate = true;

    public float xOffset, yOffset, zOffset;
    Vector3 currentPos;
    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
        //startingDistance = distance;
        currentPos = transform.position;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
    void LateUpdate()
    {
        if (target)
        {

            Cursor.lockState = CursorLockMode.Locked;
            Quaternion rotation; 

            if (useMouseToRotate)
            {
                velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
                velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;


                rotationYAxis += velocityX;
                rotationXAxis -= velocityY;
                rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
                Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
                Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
                rotation = toRotation;
            }
            else
            {
                //Debug.Log("ever here00");
                Quaternion toRotation = Quaternion.Euler(target.rotation.eulerAngles.x + xOffset, target.rotation.eulerAngles.y + yOffset, 0 + zOffset);
                rotation = toRotation;
            }

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
                distance = Mathf.Clamp(distance, distanceMin, distanceMax);
                hitting = true;

            }
            /*
            if(Input.GetKeyDown("space"))
            {
                distance = startingDistance;
            }
            else if(!Physics.Linecast(target.position, currentPos, out hit))
            {
                Debug.Log("are we here");
                distance = startingDistance;
                hitting = false;
            }*/
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            /*if (!hitting)
            {
                currentPos = position;
            }*/

            transform.rotation = rotation;
            transform.position = position;
            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);

        }
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    
    public void setTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void removeTarget()
    {
        target = null;
    }

    public void setMouseToRotate(bool setValue)
    {
        useMouseToRotate = setValue;
    }

    public void ResetDistance()
    {
        distance = startingDistance;
    }
}