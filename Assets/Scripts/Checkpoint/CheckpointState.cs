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

    [SerializeField]
    MeshRenderer renderer;

    [SerializeField]
    Material offCheckpoint;

    [SerializeField]
    Material onCheckpoint;

    private void OnEnable()
    {
        CheckpointSystem.OnCheckpointEnter += HandleNewCheckpoint;
    }

    private void OnDisable()
    {
        CheckpointSystem.OnCheckpointEnter -= HandleNewCheckpoint;
    }

    public void SaveCheckpoint()
    {
        CheckpointSystem.OnCheckpointEnter.Invoke(this);
        OnCheckpointSave?.Invoke();
    }

    private void HandleNewCheckpoint(CheckpointState checkpoint)
    {
        if(checkpoint != this)
        {
            renderer.material = offCheckpoint;
        }
        else
        {
            renderer.material = onCheckpoint;
        }
    }
}
