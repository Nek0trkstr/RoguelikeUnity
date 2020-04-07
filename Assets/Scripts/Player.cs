using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IBreakable
{
    public float k_MoovingSpeedBase = 3f;
    public float k_AttackAnimationLength = 0.85f;
    public FloatValue m_InitialHealth;
    public FloatValue m_CurrentHealth;
    public FloatValue m_UntouchableAfterDmg;
    public GameEvent m_HealthEvent;
    public int m_BlinksAfterDmg = 8;
    private Renderer m_Renderer;
    private Rigidbody2D m_RigidBody;
    private Animator m_Animator;
    private Vector2 m_MoveDirection;
    private Hurtbox m_HurtBox;
    private float m_MoveSpeed;
    private bool m_IsAttacking = false;
    private PlayerState m_State;
    private PlayerInputActions m_InputActions;
    private float k_DashSpeedMultiplier = 6f;
    private float k_MaxDashTime = 0.2f;
    private float m_RemainingDashTime;
    private const string k_VerticalAnimationVar = "Vertical";
    private const string k_HorizontalAnimationVar = "Horizontal";
    private const string k_SpeedAnimationVar = "Speed";
    private const string k_AttackAnimationTrigger = "Attack";

    private void Awake()
    {
        m_State = PlayerState.Idle;
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Renderer = GetComponent<Renderer>();
        m_HurtBox = GetComponentInChildren<Hurtbox>();
        m_CurrentHealth.m_RuntimeValue = m_InitialHealth.m_RuntimeValue;
        m_InputActions = new PlayerInputActions();
        m_InputActions.PlayerControls.Move.performed += OnUpdateMoveDirection; 
        m_InputActions.PlayerControls.Attack.performed += _ => m_State = PlayerState.Attacking;
        m_InputActions.PlayerControls.Dash.performed += _ => OnDash();
        m_RemainingDashTime = k_MaxDashTime;
    }

    private void Update()
    {
        switch (m_State)
        {
            case PlayerState.Idle:
                if (m_MoveDirection != Vector2.zero)
                {
                    m_State = PlayerState.Moving;
                }

                break;
            case PlayerState.Moving:
                m_MoveDirection.Normalize();
                m_Animator.SetFloat(k_VerticalAnimationVar, m_MoveDirection.y);
                m_Animator.SetFloat(k_HorizontalAnimationVar, m_MoveDirection.x);
                m_Animator.SetFloat(k_SpeedAnimationVar, m_MoveSpeed);
                m_RigidBody.MovePosition(m_RigidBody.position + m_MoveDirection * (m_MoveSpeed * k_MoovingSpeedBase * Time.fixedDeltaTime));
                break;
            case PlayerState.Dash:
                m_RigidBody.MovePosition(m_RigidBody.position + m_MoveDirection * (m_MoveSpeed * k_MoovingSpeedBase * 4f * Time.fixedDeltaTime));
                StartCoroutine(DoBlinksCo(4, Time.fixedDeltaTime));
                StartCoroutine(m_HurtBox.ToggleHitBoxCo(k_MaxDashTime));
                m_RemainingDashTime -= Time.fixedDeltaTime;
                if (m_RemainingDashTime <= 0)
                {
                    m_RemainingDashTime = k_MaxDashTime;
                    m_State = PlayerState.Idle;
                }
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
        StartCoroutine(DoBlinksCo(m_BlinksAfterDmg, m_UntouchableAfterDmg.m_RuntimeValue));
        m_CurrentHealth.m_RuntimeValue -= i_ReceivedDamage;
        m_HealthEvent.Raise();
        if (m_CurrentHealth.m_RuntimeValue <= 0)
        {
            Debug.Log("Game Over");
        }
    }
    
    IEnumerator DoBlinksCo(int i_BlinksNumber, float i_BlinkingLength)
    {
        float blingInterval = i_BlinkingLength / (i_BlinksNumber * 2);
        for (int i = 0; i < i_BlinksNumber * 2; i++) {
            m_Renderer.enabled = !m_Renderer.enabled;
            yield return new WaitForSeconds(blingInterval);
        }
        
        m_Renderer.enabled = true;
    }

    private void OnUpdateMoveDirection(InputAction.CallbackContext i_Context)
    {
        Vector2 newMoveDirection = i_Context.ReadValue<Vector2>();
        // Dont update move direction if nothing is pressed
        if (newMoveDirection != Vector2.zero && m_State != PlayerState.Dash)
        {
            m_MoveSpeed = Mathf.Clamp(newMoveDirection.sqrMagnitude, 0f, 1f);
            m_MoveDirection = newMoveDirection;
        }
        else
        {
            m_MoveSpeed = 0f;
        }
    }

    private void OnDash()
    {
        if (m_State != PlayerState.Attacking)
        {
            m_State = PlayerState.Dash;
        }
    }
    
    private enum PlayerState
    {
        Idle,
        Moving,
        Dash,
        Attacking,
        Stagger,
    }
}
