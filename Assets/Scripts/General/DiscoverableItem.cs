using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscoverableItem : MonoBehaviour
{
    public Outline outline;
    public GameObject TextUI;

    [SerializeField]
    private DialogueData dData = new DialogueData();

    // Start is called before the first frame update
    void Start()
    {
        deactivateOutline();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rayCastHit()
    {
        activateOutline();
    }

    public void rayCastLeft()
    {
        deactivateOutline();
    }

    public void activateOutline()
    {
        outline.OutlineWidth = 3;
    }

    public void deactivateOutline()
    {
        outline.OutlineWidth = 0;
    }

    public void analyzed(UnityEvent currentEvent)
    {
        TextUI.SetActive(true);
        TextUI.GetComponent<TextDisplayManager>().populateText(dData, currentEvent);
    }

    public void finishedAnalysing()
    {

    }
}
