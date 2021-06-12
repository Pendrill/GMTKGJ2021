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
        jumpKey = KeyCode.Space;

    public Vector3 move_vector = Vector3.zero;
    public float vert_vector;

    // Update is called once per frame
    void Update()
    {
        move_vector = transform.forward * Input.GetAxisRaw("Vertical")
            + transform.right * Input.GetAxisRaw("Horizontal");
    }
}
