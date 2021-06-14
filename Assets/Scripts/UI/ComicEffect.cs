using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicEffect : MonoBehaviour
{
    [Header("General")]
    Image img;
    [SerializeField]
    public Transform target;
    private Camera cam;

    [Header("Life")]
    [SerializeField]
    float lifeTimer = 2f;
    private float curTime;

    [Header("Alpha")]
    [SerializeField]
    float timeToSolid = 1.5f;
    private float curFade = 0, fadeTimer = 0;

    [Header("Scaling")]
    [SerializeField]
    float timeToScale = 1.5f;
    [SerializeField]
    float initScale = 0.1f;
    [SerializeField]
    float finalScale = 0.4f;
    private float curScale, scaleTimer = 0;
  
    private void Start()
    {
        cam = GameSingleton.Instance.controller.inputReader.cam.GetComponent<Camera>();
        img = GetComponent<Image>();
        curTime = lifeTimer;
        curScale = initScale;
    }
    private void Update()
    {
        Position();
        RunTimer();
        RunFade();
        RunScale();
    }

    private void Position()
    {
        transform.position = cam.WorldToScreenPoint(target.position);
    }

    private void RunTimer()
    {
        if (curTime > 0)
        {
            curTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RunFade()
    {
        if(fadeTimer < timeToSolid)
        {
            fadeTimer += Time.deltaTime;
            curFade = fadeTimer / timeToSolid;
            img.color = new Color(img.color.r, img.color.g, img.color.b, curFade);
        }        
    }

    private void RunScale()
    {
        if(scaleTimer < timeToScale)
        {
            scaleTimer += Time.deltaTime;
            curScale = scaleTimer / timeToScale * (finalScale - initScale);
            transform.localScale = new Vector3(curScale, curScale);
        }     
    }
}
