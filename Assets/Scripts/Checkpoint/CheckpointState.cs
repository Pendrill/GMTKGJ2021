using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Specific checkpoint state
/// </summary>
public class CheckpointState : MonoBehaviour
{
    [SerializeField]
    public LevelVolume volume;

    /// <summary>
    /// Objects that need to return to a state to be a checkpoint
    /// </summary>
    [SerializeField]
    public List<GameObject> checkpointObjects;

    /// <summary>
    /// UnityEvent that fires when the checkpoint saves.
    /// </summary>
    [SerializeField]
    UnityEvent OnCheckpointSave;

    public void SaveCheckpoint()
    {
        CheckpointSystem.OnCheckpointEnter.Invoke(this);
        OnCheckpointSave?.Invoke();
    }
}
