using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Reacts to weights above it
/// </summary>
public class WeightSensitiveInteractable : MonoBehaviour
{
    [SerializeField]
    public List<InteractableWeight.WeightValue> requiredWeights;

    /// <summary>
    /// The weights we need to invoke the event.
    /// Is calculated at start.
    /// </summary>
    Dictionary<InteractableWeight.WeightValue, int> keyDict = new Dictionary<InteractableWeight.WeightValue, int>();

    Dictionary<InteractableWeight.WeightValue, int> curWeightDict = new Dictionary<InteractableWeight.WeightValue, int>();

    [SerializeField]
    UnityEvent OnWeighted;

    [SerializeField]
    UnityEvent OnUnweighted;

    [SerializeField]
    ComicEffectManager.ComicEffectType effectType;

    private bool eventInvoked = false;

    private void OnCollisionEnter(Collision collision)
    {
        InteractableWeight interactableWeight = collision.collider.gameObject.GetComponent<InteractableWeight>();
        if (interactableWeight != null)
        {
            AddWeight(interactableWeight.weight, curWeightDict);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        InteractableWeight interactableWeight = collision.collider.gameObject.GetComponent<InteractableWeight>();
        if (interactableWeight != null)
        {
            RemoveWeight(interactableWeight.weight, curWeightDict);
        }
    }

    private void Start()
    {
        CalculateKey();
    }

    private void Update()
    {
        bool weightValid = CheckWeights();
        if (weightValid && !eventInvoked)
        {
            OnWeighted?.Invoke();
            PlayEffect();
            eventInvoked = true;
        }
        else if (!weightValid && eventInvoked)
        {
            OnUnweighted?.Invoke();
            eventInvoked = false;
        }
    }

    private void AddWeight(InteractableWeight.WeightValue weight, Dictionary<InteractableWeight.WeightValue, int> targetDict)
    {
        int value;
        if (targetDict.TryGetValue(weight, out value))
        {
            targetDict.Remove(weight);
            targetDict.Add(weight, value + 1);
        }
        else
        {
            targetDict.Add(weight, 1);
        }
    }

    private void RemoveWeight(InteractableWeight.WeightValue weight, Dictionary<InteractableWeight.WeightValue, int> targetDict)
    {
        int value;
        if (targetDict.TryGetValue(weight, out value))
        {
            targetDict.Remove(weight);
            if(value - 1 > 0)
            {
                targetDict.Add(weight, value - 1);
            }         
        }
    }

    private void CalculateKey()
    {
        foreach (InteractableWeight.WeightValue weight in requiredWeights)
        {
            AddWeight(weight, keyDict);
        }
    }

    private bool CheckWeights()
    {
        foreach(string name in Enum.GetNames(typeof(InteractableWeight.WeightValue)))
        {
            InteractableWeight.WeightValue weight;
            if(Enum.TryParse<InteractableWeight.WeightValue>(name, out weight))
            {
                int keyValue, curValue;
                //Try to get key value
                if(keyDict.TryGetValue(weight, out keyValue))
                {
                    //Attempt to get current weights.
                    if(curWeightDict.TryGetValue(weight, out curValue))
                    {
                        //If we have that weight compare values.
                        if (keyValue != curValue)
                        {
                            return false;
                        }
                    }
                    else //If we don't have that weight at all.
                    {
                        return false;
                    }   
                }             
            }
            else
            {
                Debug.LogError("Unable to parse enum correctly.");
            }           
        }
        return true;
    }

    public void PlayEffect()
    {
        GameSingleton.Instance.uiManager.effectManager.PlayEffect(transform, effectType);
    }
}
