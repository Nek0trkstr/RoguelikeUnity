using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    public float m_MoovingSpeed = 2f;
    private Rigidbody2D m_RigidBody;
    private  Animator m_Animator;
    private Vector2 m_MoveDirection;
    private const string k_VerticalAnimationVar = "Vertical";
    private const string k_HorizontalAnimationVar = "Horizontal";
    private const string k_SpeedAnimationVar = "Speed";
    private const string k_AttackAnimationTrigger = "Attack";
    private const float k_AttackAnimationLength = 0.33f;
    private bool m_IsAttacking = false;
    
    private PlayerInputActions m_InputActions;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_InputActions = new PlayerInputActions();
        m_InputActions.PlayerControls.Move.performed += ctx => m_MoveDirection = ctx.ReadValue<Vector2>();
        m_InputActions.PlayerControls.Attack.performed += _ => OnAttack();
    }
    
    private void FixedUpdate()
    {
        m_Animator.SetFloat(k_VerticalAnimationVar, m_MoveDirection.y);
        m_Animator.SetFloat(k_HorizontalAnimationVar, m_MoveDirection.x);
        m_Animator.SetFloat(k_SpeedAnimationVar, m_MoveDirection.sqrMagnitude);
    }

    private void Update()
    {
        m_RigidBody.MovePosition(m_RigidBody.position + m_MoveDirection * m_MoovingSpeed * Time.fixedDeltaTime);
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
    }

    private void OnEnable()
    {
        m_InputActions.Enable();
    }

    private void OnDisable()
    {
        m_InputActions.Disable();
    }
}
