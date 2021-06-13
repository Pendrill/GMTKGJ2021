using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedTutorial : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject eye, hand, leg, halfBody, mainBody;

    public Animator fadeAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tutDone()
    {
        eye.SetActive(false);
        hand.SetActive(false);
        leg.SetActive(false);
        halfBody.SetActive(false);
        mainBody.SetActive(true);

        fadeAnimator.SetBool("startFade", true);
    }
}
