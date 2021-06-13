using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscoverableItem : MonoBehaviour
{
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
        OnAnalyze?.Invoke();
    }

    public void finishedAnalysing()
    {

    }
}
