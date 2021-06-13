using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTut : MonoBehaviour
{

    public GameObject oldTutorial;
    public GameObject newTutorial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArmAnalyzed()
    {
        oldTutorial.SetActive(false);
        newTutorial.SetActive(true);
    }
}
