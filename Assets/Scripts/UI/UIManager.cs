using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text messageText;

    [SerializeField]
    public GameObject textUI;

    [SerializeField]
    public ComicEffectManager effectManager;
    public void SetMessage(string msg)
    {
        messageText.text = msg;
    }

    public void EmptyMessage()
    {
        messageText.text = "";
    }
}
