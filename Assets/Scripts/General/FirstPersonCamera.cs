using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLocalRotation(Quaternion rotation)
    {
        transform.localRotation = rotation;
    }

    public void setCamPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
