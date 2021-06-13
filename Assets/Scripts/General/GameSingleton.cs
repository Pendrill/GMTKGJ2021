using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton manager that will hold references to global needs
/// </summary>
public class GameSingleton : MonoBehaviour
{
    //References that need to be in singleton
    [SerializeField]
    public UIManager uiManager;

    [SerializeField]
    public CheckpointSystem checkpointSystem;

    //Singleton pattern here:
    private static GameSingleton _instance;

    public static GameSingleton Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
