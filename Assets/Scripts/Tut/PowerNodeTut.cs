using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerNodeTut : MonoBehaviour
{

    public GameObject gate;
    public GameObject arm, leg;
    public GameObject textArm, textLeg, startingTut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateTutOtherParts()
    {
        gate.SetActive(false);
        arm.SetActive(true);
        leg.SetActive(true);
        textArm.SetActive(true);
        textLeg.SetActive(true);
        startingTut.SetActive(false);
    }
}
