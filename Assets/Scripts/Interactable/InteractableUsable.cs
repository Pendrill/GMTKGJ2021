using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Usable object
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class InteractableUsable : MonoBehaviour
{
    [SerializeField]
    public UnityEvent OnUse;

    [SerializeField]
    private string usable_name = "Generic Object";

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PartController>() != null &&
            other.gameObject.GetComponent<PartController>().Type == PartController.PartType.Arm)
        {
            GameSingleton.Instance.uiManager.SetMessage("Press 'E' to use " + usable_name);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PartController>() != null &&
          other.gameObject.GetComponent<PartController>().Type == PartController.PartType.Arm)
        {
            GameSingleton.Instance.uiManager.EmptyMessage();
        }
    }
}
