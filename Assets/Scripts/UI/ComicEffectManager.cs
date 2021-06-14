using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicEffectManager : MonoBehaviour
{
    [SerializeField]
    ComicEffect click, fwoosh, thunk;

    //Stores comic effects as game objects in a dict, where the value is time left for the effect
    Dictionary<GameObject, float> effectDict = new Dictionary<GameObject, float>();

    /// <summary>
    /// Plays a 2D effect on our screen,
    /// Gets a world position and converts it into where on the 2D Display to play it
    /// </summary>
    /// <param name="worldPos"></param>
    public void PlayEffect(Transform target, ComicEffectType type)
    {
        ComicEffect effectPrefab = GrabEffect(type);

        //Vector2 pos = GameSingleton.Instance.controller.inputReader.
        //    cam.GetComponent<Camera>().WorldToScreenPoint(worldPos);

        ComicEffect effect = Instantiate(effectPrefab, Vector3.zero, Quaternion.identity, transform);
        effect.target = target;
    }

    private ComicEffect GrabEffect(ComicEffectType type)
    {
        switch (type)
        {
            case ComicEffectType.Click:
                return click;
            case ComicEffectType.Fwoosh:
                return fwoosh;
            case ComicEffectType.Thunk:
                return thunk;
            default:
                Debug.LogError("Invliad effect type.");
                return click;                
        }
    }

    public enum ComicEffectType
    {
        Click,
        Fwoosh,
        Thunk
    }
}
