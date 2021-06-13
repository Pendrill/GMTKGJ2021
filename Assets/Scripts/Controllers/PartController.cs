using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General part controller.
/// Parts should inherit from this
/// </summary>
public class PartController : MonoBehaviour
{
    public enum PartType
    {
        Eye,
        Leg,
        Arm,
        Body
    }
    [SerializeField]
    private PartType type;

    [HideInInspector]
    public PartType Type
    {
        get
        {
            return type;
        }
    }

    private bool inUse = false;
    [SerializeField]
    public bool useMouseToRotate;

    public void EnablePart()
    {
        inUse = true;
        partUI.gameObject.SetActive(true);
        OnUsePart();
    }

    public void DisablePart()
    {
        inUse = false;
        partUI.gameObject.SetActive(false);
        OnDisablePart();
    }

    public bool GetPartInUse()
    {
        return inUse;
    }

    public InputReader inputReader;
    protected Rigidbody rb;

    [SerializeField]
    protected Canvas partUI;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitPart();
    }

    public void Update()
    {
        if (inUse)
        {
            ControlPart();
        }
    }

    public void FixedUpdate()
    {
        if (inUse)
        {
            ControlPhysics();
        }
    }

    /// <summary>
    /// Runs in Start
    /// </summary>
    public virtual void InitPart() 
    { 
    }

    /// <summary>
    /// Runs in Update
    /// </summary>
    public virtual void ControlPart()
    { 
    }

    /// <summary>
    /// Runs in FixedUpdate
    /// </summary>
    public virtual void ControlPhysics()
    {

    }

    /// <summary>
    /// Runs when switching to this part
    /// </summary>
    public virtual void OnUsePart()
    {

    }

    /// <summary>
    /// Runs when switching off this part.
    /// </summary>
    public virtual void OnDisablePart()
    {

    }
}
