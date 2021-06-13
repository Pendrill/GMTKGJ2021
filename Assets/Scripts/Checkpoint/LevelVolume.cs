using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Volume that defines a level.
/// Leaving it should trigger a checkpoint reset.
/// </summary>
public class LevelVolume : MonoBehaviour
{
    bool volumeActive = false;

    private void OnTriggerExit(Collider other)
    {
        if (volumeActive)
        {
            if (other.GetComponent<PartController>())
            {
                CheckpointSystem.OnCheckpointReload.Invoke();
            }
        }        
    }

    public void EnableVolume()
    {
        volumeActive = true;
    }

    //Disables this volume. Used when moving from one level to next
    public void DisableVolume()
    {
        volumeActive = false;
    }
}
