using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoboDisplayManager : MonoBehaviour
{
    [SerializeField]
    PartDisplay armL, armR, legL, legR, body, head, eyeL, eyeR;

    private void OnEnable()
    {
        PlayerController.OnPartAdd += HandlePartAdd;
        PlayerController.OnPartRemove += HandlePartRemove;
    }

    private void OnDisable()
    {
        PlayerController.OnPartAdd -= HandlePartAdd;
        PlayerController.OnPartRemove -= HandlePartRemove;
    }

    public void DiplayBodyHead()
    {
        body.ActivateSprite();
        //head.ActivateSprite();
    }

    private void HandlePartAdd(PartController.PartType type, PartController.PartSpecificType specific)
    {
        PartDisplay display = GrabPartDisplay(type, specific);

        if(type == PartController.PartType.Body)
        {
            body.ActivateSprite();
            head.ActivateSprite();
        }
        else
        {
            display.ActivateSprite();
        }    
    }

    private void HandlePartRemove(PartController.PartType type, PartController.PartSpecificType specific)
    {
        PartDisplay display = GrabPartDisplay(type, specific);

        if (type == PartController.PartType.Body)
        {
            body.DeactivateSprite();
            head.DeactivateSprite();
        }
        else
        {
            display.DeactivateSprite();
        }
    }

    private PartDisplay GrabPartDisplay(PartController.PartType type, PartController.PartSpecificType specific)
    {
        switch (type)
        {
            case PartController.PartType.Arm:
                switch (specific)
                {
                    case PartController.PartSpecificType.Left:
                        return armL;
                    case PartController.PartSpecificType.Right:
                        return armR;
                    default:
                        Debug.LogError("Invalid arm, no specific type.");
                        break;
                }
                break;
            case PartController.PartType.Leg:
                switch (specific)
                {
                    case PartController.PartSpecificType.Left:
                        return legL;
                    case PartController.PartSpecificType.Right:
                        return legR;
                    default:
                        Debug.LogError("Invalid arm, no specific type.");
                        break;
                }
                break;
            case PartController.PartType.Eye:
                switch (specific)
                {
                    case PartController.PartSpecificType.Left:
                        return eyeL;
                    case PartController.PartSpecificType.Right:
                        return eyeR;
                    default:
                        Debug.LogError("Invalid arm, no specific type.");
                        break;
                }
                break;
            case PartController.PartType.Body:
                return body;
            default:
                Debug.LogError("Invalid arm, no specific type.");
                break;
        }
        return null;
    }
}
