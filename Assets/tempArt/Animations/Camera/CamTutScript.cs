using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTutScript : MonoBehaviour
{

    public PlayerController thePlayerController;
    public Animator theCamAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePlayer()
    {
        thePlayerController.enabled = true;
        theCamAnimator.enabled = false;
    }

    //-0.05520916, 2.256894, -12.22167
}
