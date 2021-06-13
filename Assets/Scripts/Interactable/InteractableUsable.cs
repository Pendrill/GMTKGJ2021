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
    public UnityEvent OnRelease;

    [SerializeField]
    private string usable_name = "Generic Object";

    private bool active = false;

    [Tooltip("Object that is put into arm when grabbed.")]
    [SerializeField]
    public Transform holdable;

    private void Update()
    {
        if(active)
        {
           
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PartController>() != null &&
            other.gameObject.GetComponent<PartController>().Type == PartController.PartType.Arm)
        {
            GameSingleton.Instance.controller.inputReader.grabbable = true;
            GameSingleton.Instance.controller.inputReader.grabbableName = usable_name;
            GameSingleton.Instance.controller.inputReader.holdable = holdable;
            GameSingleton.Instance.controller.inputReader.usable = this;
            active = true;
        }

       
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PartController>() != null &&
          other.gameObject.GetComponent<PartController>().Type == PartController.PartType.Arm)
        {
            GameSingleton.Instance.uiManager.EmptyMessage();
            GameSingleton.Instance.controller.inputReader.grabbable = false;
            GameSingleton.Instance.controller.inputReader.grabbableName = "";
            GameSingleton.Instance.controller.inputReader.holdable = null;
            GameSingleton.Instance.controller.inputReader.usable = null;
            active = false;
        }
    }
}
