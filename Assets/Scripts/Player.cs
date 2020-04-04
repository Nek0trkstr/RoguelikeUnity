﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IBreakable
{
    public float m_MoovingSpeed = 2f;
    public float k_AttackAnimationLength = 0.85f;
    public FloatValue m_InitialHealth;
    public FloatValue m_CurrentHealth;
    public GameEvent m_HealthEvent;
    private Rigidbody2D m_RigidBody;
    private Animator m_Animator;
    private Vector2 m_MoveDirection;
    private const string k_VerticalAnimationVar = "Vertical";
    private const string k_HorizontalAnimationVar = "Horizontal";
    private const string k_SpeedAnimationVar = "Speed";
    private const string k_AttackAnimationTrigger = "Attack";
    
    private bool m_IsAttacking = false;
    private PlayerState m_State;
    private PlayerInputActions m_InputActions;

    private void Awake()
    {
        m_State = PlayerState.Idle;
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_CurrentHealth.m_RuntimeValue = m_InitialHealth.m_RuntimeValue;
        m_InputActions = new PlayerInputActions();
        m_InputActions.PlayerControls.Move.performed += ctx => m_MoveDirection = ctx.ReadValue<Vector2>();
        m_InputActions.PlayerControls.Attack.performed += _ => m_State = PlayerState.Attacking;
    }
    
    private void FixedUpdate()
    {

    }

    private void Update()
    {
        switch (m_State)
        {
            case PlayerState.Idle:
                if (m_MoveDirection != Vector2.zero)
                {
                    m_State = PlayerState.Mooving;
                }

                break;
            case PlayerState.Mooving:
                m_MoveDirection.Normalize();
                m_Animator.SetFloat(k_VerticalAnimationVar, m_MoveDirection.y);
                m_Animator.SetFloat(k_HorizontalAnimationVar, m_MoveDirection.x);
                m_Animator.SetFloat(k_SpeedAnimationVar, m_MoveDirection.sqrMagnitude);
                m_RigidBody.MovePosition(m_RigidBody.position + m_MoveDirection * m_MoovingSpeed * Time.fixedDeltaTime);
                break;
            case PlayerState.Attacking:
                OnAttack();
                
                break;
            case PlayerState.Stagger:
                break;
        }
    }

    private void OnAttack()
    {
        if(!m_IsAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        m_IsAttacking = true;
        m_Animator.SetTrigger(k_AttackAnimationTrigger);
        yield return new WaitForSeconds(k_AttackAnimationLength);
        m_IsAttacking = false;
        m_State = PlayerState.Idle;
    }

    private void OnEnable()
    {
        m_InputActions.Enable();
    }

    private void OnDisable()
    {
        m_InputActions.Disable();
    }

    public void ReceiveDamage(float i_ReceivedDamage)
    {
        Debug.Log("Player received dmg");
        m_CurrentHealth.m_RuntimeValue -= i_ReceivedDamage;
        m_HealthEvent.Raise();
        if (m_CurrentHealth.m_RuntimeValue <= 0)
        {
            Debug.Log("Game Over");
        }
    }
    
    private enum PlayerState
    {
        Idle,
        Mooving,
        Attacking,
        Stagger,
    }
}
