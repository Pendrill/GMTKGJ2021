using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    GameObject target;

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.position = target.transform.position;
        }
    }

    public void updateLocalRotation(Quaternion rotation)
    {
        transform.localRotation = rotation;
    }

    public void setCamTarget(GameObject obj)
    {
        target = obj;
    }

    public void DeactivateCam()
    {
        target = null;
    }
}
