using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Does specific clean up when a part is gained
/// </summary>
public class PartGainedCleanup : MonoBehaviour
{
    public void Cleanup()
    {
        Destroy(transform.GetComponent<Outline>());
        Destroy(transform.GetComponent<DiscoverableItem>());
        Destroy(this);
    }
}
