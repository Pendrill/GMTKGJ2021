using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specific checkpoint state
/// </summary>
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(CheckpointState))]
public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PartController>())
        {
            GetComponent<CheckpointState>().SaveCheckpoint();
        }
    }
}
