using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Events;

public class TextDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI text;
    public Image portrait;

    bool textActivated = false;
    UnityEvent currentEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(textActivated)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                textActivated = false;
                currentEvent.Invoke();
                gameObject.SetActive(false);
            }
        }
    }

    public void populateText(DialogueData info, UnityEvent theEvent)
    {
        name.text = info.Name;
        text.text = info.Text;
        portrait.sprite = info.Image;
        textActivated = true;

        currentEvent = theEvent;
    }
}

[Serializable]
public struct DialogueData
{
    public string Name;
    public string Text;
    public Sprite Image;
}
