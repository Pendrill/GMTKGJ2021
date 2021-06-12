using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to objects that have a weight that can be used to push down on stuff
/// </summary>
public class InteractableWeight : MonoBehaviour
{
    public enum WeightValue
    {
        Light,
        Heavy
    }

    public WeightValue weight;
}
