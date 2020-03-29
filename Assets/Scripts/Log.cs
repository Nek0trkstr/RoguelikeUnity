using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public float m_AttackRadius;
    public float m_ChaseRadius;
    private Vector3 m_TargetPosition;
    private Vector3 m_HomePosition;
    private Animator m_Animator;
    

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_HomePosition = GetComponent<Transform>().position;
    }

    private void FixedUpdate()
    {
        m_TargetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        CalculateState();
    }

    private void CalculateState()
    {
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
            break;
        }
    }
}
