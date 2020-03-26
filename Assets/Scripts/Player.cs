using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float m_MoovingSpeed = 2f;
    private Rigidbody2D m_RigidBody;
    private Animator m_Animator;
    private Vector2 m_MoveDirection;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        m_MoveDirection.y = Input.GetAxisRaw("Vertical");
        m_MoveDirection.x = Input.GetAxis("Horizontal");
        m_Animator.SetFloat("Vertical", m_MoveDirection.y);
        m_Animator.SetFloat("Horizontal", m_MoveDirection.x);
        m_Animator.SetFloat("Speed", m_MoveDirection.sqrMagnitude);
    }

    // Update is called once per frame
    private void Update()
    {
        m_RigidBody.MovePosition(m_RigidBody.position + m_MoveDirection * m_MoovingSpeed * Time.fixedDeltaTime);
    }
}
