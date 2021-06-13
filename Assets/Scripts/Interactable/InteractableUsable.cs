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

    public bool needMultipleParts;
    public int numberOfParts = 3;
    int currentNumberOfParts = 0;

    public PartController.PartType firstType, secondType, thirdType;
    PartController.PartType[] types = new PartController.PartType[3];

    private void Start()
    {
        types[0] = firstType;
        types[1] = secondType;
        types[2] = thirdType;
    }

    private void Update()
    {
        if (active)
        {
           if(Input.GetKeyDown(KeyCode.E))
            {
                OnUse.Invoke();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (needMultipleParts)
        {
            multipleObjectEnter(other);
        }
        else
        {
            singleObjectEnter(other);
        }
        
    }

    public void singleObjectEnter(Collider other)
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

    public void multipleObjectEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PartController>() != null)
        {
            PartController.PartType tempType;
            for (int i = 0; i < types.Length; i++)
            {
                if (other.gameObject.GetComponent<PartController>().Type == types[i])
                {
                    currentNumberOfParts += 1;
                    if(currentNumberOfParts == numberOfParts)
                    {
                        GameSingleton.Instance.uiManager.SetMessage("Press E to join B.O.B. together");
                        active = true;
                    }
                    else
                    {
                        GameSingleton.Instance.uiManager.SetMessage(currentNumberOfParts + " of the 3 parts have been joined");
                        active = false;
                    }
                    
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (needMultipleParts)
        {
            multipleObjectExit(other);
        }
        else
        {
            singleObjectExit(other);
        }

       
    }

    public void singleObjectExit(Collider other)
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

    public void multipleObjectExit(Collider other)
    {
        if (other.gameObject.GetComponent<PartController>() != null)
        {
            PartController.PartType tempType;
            for (int i = 0; i < types.Length; i++)
            {
                if (other.gameObject.GetComponent<PartController>().Type == types[i])
                {
                    currentNumberOfParts -= 1;
                    if (currentNumberOfParts == numberOfParts)
                    {
                        GameSingleton.Instance.uiManager.SetMessage("Press E to join B.O.B. together");
                        active = true;
                    }
                    else
                    {
                        GameSingleton.Instance.uiManager.SetMessage(currentNumberOfParts + " of the 3 parts have been joined");
                        active = false;
                    }

                }
            }
        }
    }
}
