using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller specific to legs
/// </summary>
[RequireComponent(typeof(InputReader))]
public class LegController : MonoBehaviour
{
    Rigidbody rb;
    InputReader inputReader;

    [Header("Movement")]
    [SerializeField]
    float baseMoveSpeed;
    [SerializeField]
    float groundMoveMultiplier;
    [SerializeField]
    float groundDrag = 6f;

    [Header("Grounded")]
    [SerializeField]
    Transform groundCheckSphere;
    [SerializeField]
    float groundDistance;
    [SerializeField]
    LayerMask groundMask;
    bool isGrounded;

    [Header("Jumping")]
    [SerializeField]
    float airDrag = 0f;
    [SerializeField] 
    float jumpForce = 5f;
    [SerializeField]
    float fallDrag = 0f;

    [Header("Boosting")]
    [SerializeField]
    BoostState boostState = BoostState.Idle;
    [SerializeField]
    float maxBoost = 10f;
    [SerializeField]
    float boostAccel = 1f;
    [SerializeField]
    float boostGravity = 0.2f;
    [SerializeField]
    float fallGravity = 1.0f;
    [SerializeField]
    float hoverGravity = 0.5f;
    [SerializeField]
    float airMoveMultiplier;
    [SerializeField]
    float boostTime = 2f;
    private float boostTimeRemaining = 2f;

    [Header("UI")]
    [SerializeField]
    Image fuelGauge;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckSphere.position, groundDistance, groundMask);
        ControlDrag();
        ControlBoost();
        HandleBoost();
        ControlGravity();
        ControlUI();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void ControlUI()
    {
        fuelGauge.fillAmount = boostTimeRemaining / boostTime;
    }

    private void ControlBoost()
    {
        if (Input.GetKey(inputReader.jumpKey))
        {
            if(boostTimeRemaining > 0)
            {
                boostState = BoostState.Boost;
            }
            else
            {
                boostState = BoostState.Hover;
            }
            
        }
        else
        {
            boostState = BoostState.Idle;
        }
    }

    private void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else if (!isGrounded)
        {
            if (boostState == BoostState.Boost)
            {
                rb.drag = airDrag;
            }
            else if(boostState == BoostState.Idle)
            {
                rb.drag = fallDrag;
            }
            
        }
    }
    
    private void ControlGravity()
    {
        if (boostState == BoostState.Boost)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (boostGravity - 1) * Time.deltaTime;
        }
        else if(boostState == BoostState.Hover)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (hoverGravity - 1) * Time.deltaTime;
        }
        else if(!isGrounded && boostState == BoostState.Idle)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallGravity - 1) * Time.deltaTime;
        }
    }

    private void HandleBoost()
    {
        if (boostState == BoostState.Boost && boostTimeRemaining > 0)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }

            if (rb.velocity.y < maxBoost)
            {
                rb.AddForce(transform.up * boostAccel, ForceMode.Acceleration);
            }

            boostTimeRemaining -= Time.deltaTime;
        }
        else if (isGrounded)
        {
            boostTimeRemaining = boostTime;
        }
    }

    private void MovePlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(inputReader.move_vector *
                baseMoveSpeed * groundMoveMultiplier,
                ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(inputReader.move_vector *
                baseMoveSpeed * airMoveMultiplier,
                ForceMode.Acceleration);
        }
       
    }

    private enum BoostState
    {
        Idle,
        Boost,
        Hover
    }
}
