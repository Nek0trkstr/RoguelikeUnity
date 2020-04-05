using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public float m_AttackRadius;
    public float m_ChaseRadius;
    private Vector3 m_TargetPosition;
    private Vector3 m_HomePosition;
    
    void Awake()
    {
        m_HomePosition = GetComponent<Transform>().position;
        m_Animator = GetComponent<Animator>();
        m_HealthPoints = m_MaxHealthPoints.m_RuntimeValue;
        m_Renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        m_TargetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        CalculateState();
    }

    private void CalculateState()
    {
        if (m_State == EnemyState.Stagger)
        {
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, m_TargetPosition);
        if ((distanceToTarget <= m_ChaseRadius) && (distanceToTarget > m_AttackRadius))
        {
            m_State = EnemyState.Move;
        }
        else if(distanceToTarget <= m_AttackRadius)
        {
            m_State = EnemyState.Attack;
        }
        else 
        {
            m_State = EnemyState.Idle;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_State)
        {
            case EnemyState.Idle:
                m_Animator.SetFloat("Speed", 0);
                break;
            case EnemyState.Move:
                transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, Time.deltaTime * m_MooveSpeed);
                Vector3 moveDirection = m_TargetPosition - transform.position;
                m_Animator.SetFloat("Vertical", moveDirection.y);
                m_Animator.SetFloat("Horizontal", moveDirection.x);
                m_Animator.SetFloat("Speed", moveDirection.sqrMagnitude);
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Stagger:
                transform.Translate(new Vector3(0, 0, 0));
                break;
        }
    }
}
