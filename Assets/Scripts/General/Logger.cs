using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test logger
/// </summary>
public class Logger : MonoBehaviour
{
    public void Log(string log)
    {
        Debug.Log("Log: " + log);
    }
}
