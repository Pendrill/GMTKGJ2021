using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parses in inputs and translates it into usable vectors
/// </summary>
public class InputReader : MonoBehaviour
{
    [Header("Key Bindings")]
    [SerializeField]
    public KeyCode leftKey = KeyCode.A,
        rightKey = KeyCode.D,
        upKey = KeyCode.W,
        downKey = KeyCode.S,
        jumpKey = KeyCode.Space,
        switchKey = KeyCode.Q,
        switchCameraMode = KeyCode.E,
        flashlightKey = KeyCode.F,
        stopEyeRoll = KeyCode.Z,
        analyzeKey = KeyCode.Mouse0,
        resetCheckpoint = KeyCode.R;


    [Header("Camera")]
    public GameObject cam;
    public bool activateCamera = false;
    public bool rotateObj = false;
    Vector3 camForward;


    public Vector3 move_vector = Vector3.zero;

    private void Start()
    {
        cam = GameObject.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        RotateObj();
        SetMoveVector();
        ControlReloadCheckpoint();
    }

    private void ControlReloadCheckpoint()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CheckpointSystem.OnCheckpointReload.Invoke();
        }
    }

    private void SetMoveVector()
    {
        move_vector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        move_vector = cam.transform.TransformDirection(move_vector);
        move_vector.y = 0.0f;
    }

    private void RotateObj()
    {
        //Get the camera's forward
        //Project the forward down onto a flat plane
        camForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up);

        //Rotate object so it's forward matches camForward
        transform.rotation = Quaternion.LookRotation(camForward);
    }
}
