using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutSquareScript : MonoBehaviour
{

    public Animator theAnimator;
    public Animator camAnimator;
    public bool startFading = false;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateExitSequence()
    {
        startFading = true;
        theAnimator.SetBool("startFading", true);
    }

    public void EndAnim()
    {
        text.SetActive(false);
        camAnimator.SetBool("MoveCamBack", true);
        gameObject.SetActive(false);
    }

    // 0, 2.25, -12
}
