using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI text;
    public Image portrait;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void populateText(Text newName, Text newText, Image newPortrait)
    {
        name.text = newName.text;
        text.text = newText.text;
        portrait.sprite = newPortrait.sprite;
    }
}
