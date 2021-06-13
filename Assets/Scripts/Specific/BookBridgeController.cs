using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBridgeController : MonoBehaviour
{
    [SerializeField]
    Vector3 position1, position2;

    [SerializeField]
    Vector3 rotation1, rotation2;

    public void ResetBridge()
    {
        transform.localPosition = position1;
        transform.localRotation = Quaternion.Euler(rotation1);
    }

    public void LowerBridge()
    {
        transform.localPosition = position2;
        transform.localRotation = Quaternion.Euler(rotation2);
    }
}
