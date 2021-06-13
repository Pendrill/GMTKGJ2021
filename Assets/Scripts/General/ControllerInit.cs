using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller init script
/// </summary>
public class ControllerInit : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, new Vector3(0, 3f, 0), Quaternion.identity);
    }
}
