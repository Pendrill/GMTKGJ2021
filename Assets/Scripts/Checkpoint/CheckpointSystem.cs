using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls checkpoints
/// </summary>
public class CheckpointSystem : MonoBehaviour
{
    public delegate void OnCheckpointEnterEvent(CheckpointState checkpoint);
    public static OnCheckpointEnterEvent OnCheckpointEnter;

    public delegate void OnCheckpointEvent();
    public static OnCheckpointEvent OnCheckpointReload;

    [SerializeField]
    CheckpointState curCheckpoint = null;

    Dictionary<GameObject, ObjSavedState> savedDict = new Dictionary<GameObject, ObjSavedState>();

    public void OnEnable()
    {
        OnCheckpointEnter += HandleCheckpointEnter;
        OnCheckpointReload += HandleCheckpointReload;
    }

    public void OnDisable()
    {
        OnCheckpointEnter -= HandleCheckpointEnter;
        OnCheckpointReload -= HandleCheckpointReload;
    }

    /// <summary>
    /// Handles a checkpoint being reached and saving the checkpoint.
    /// </summary>
    /// <param name="checkpoint"></param>
    private void HandleCheckpointEnter(CheckpointState checkpoint)
    {
        curCheckpoint = checkpoint;
        SaveObjects(checkpoint.checkpointObjects);
    }

    /// <summary>
    /// Handles a checkpoint being reloaded
    /// </summary>
    private void HandleCheckpointReload()
    {
        if(curCheckpoint != null)
        {
            curCheckpoint.volume.GetComponent<Collider>().enabled = false;
            foreach (GameObject obj in curCheckpoint.checkpointObjects)
            {
                ObjSavedState state;
                savedDict.TryGetValue(obj, out state);
                SetObjState(obj, state);
            }
            curCheckpoint.volume.GetComponent<Collider>().enabled = true;
        }   
    }

    /// <summary>
    /// Clears the saved obj dict and
    /// Saves all the objects in the object list
    /// </summary>
    /// <param name="objList"></param>
    private void SaveObjects(List<GameObject> objList)
    {
        savedDict.Clear();
        foreach(GameObject obj in objList)
        {
            ObjSavedState savedState = MakeObjState(obj);
            savedDict.Add(obj, savedState);
        }
    }

    /// <summary>
    /// Makes an obj state from a given obj
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private ObjSavedState MakeObjState(GameObject obj)
    {
        ObjSavedState state = new ObjSavedState();
        state.Position = obj.transform.position;
        state.Rotation = obj.transform.rotation;
        return state;
    }

    /// <summary>
    /// Sets a given object to an ObjSavedState's values
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="state"></param>
    private void SetObjState(GameObject obj, ObjSavedState state)
    {
        obj.transform.position = state.Position;
        obj.transform.rotation = state.Rotation;
        if (obj.GetComponent<Rigidbody>())
        {
            //obj.GetComponent<Rigidbody>().con
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private class ObjSavedState
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }
}
