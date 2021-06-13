using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartDisplay : MonoBehaviour
{
    [SerializeField]
    Image targetImg;

    [SerializeField]
    Sprite activeSprite, inactiveSprite;

    public void DeactivateSprite()
    {
        targetImg.sprite = inactiveSprite;
    }

    public void ActivateSprite()
    {
        targetImg.sprite = activeSprite;
    }
}
