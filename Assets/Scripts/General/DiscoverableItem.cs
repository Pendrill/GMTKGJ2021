using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscoverableItem : MonoBehaviour
{
    public bool removeOnInteraction;
    public bool requiresLight;
    public float outlineWidth;
    public Outline outline;
    GameObject TextUI;

    [SerializeField]
    private DialogueData dData = new DialogueData();

    [SerializeField]
    public UnityEvent OnAnalyze;

    // Start is called before the first frame update
    void Start()
    {
        TextUI = GameSingleton.Instance.uiManager.textUI;
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
        outline.OutlineWidth = outlineWidth;
    }

    public void deactivateOutline()
    {
        outline.OutlineWidth = 0;
    }

    public void analyzed(UnityEvent currentEvent)
    {
        TextUI.SetActive(true);
        TextUI.GetComponent<TextDisplayManager>().populateText(dData, currentEvent);
        OnAnalyze?.Invoke();
        if(removeOnInteraction)
        {
            gameObject.SetActive(false);
        }
    }

    public void finishedAnalysing()
    {

    }
}
