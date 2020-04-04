using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IBreakable
{
    public float m_MooveSpeed;
    public FloatValue m_MaxHealthPoints;
    protected float m_HealthPoints;
    protected bool m_IsDestroyed = false;
    protected EnemyState m_State = EnemyState.Idle;
    protected Animator m_Animator;
    private bool m_IsAttacking = false;
    
    public void ReceiveDamage(float i_Dmg)
    {
        m_HealthPoints = m_HealthPoints - i_Dmg;
        if(m_State != EnemyState.Stagger)
        {
            StartCoroutine(StaggerCo());
        }
        if ( m_HealthPoints <= 0)
        {
            DestroySelf();
        }
    }

    protected virtual void Attack()
    {
        if (m_IsAttacking == false)
        {
            StartCoroutine(AttackCo());
        }
    }

    private void DestroySelf()
    {
        m_IsDestroyed = true;
        Destroy(gameObject);
    }

    private IEnumerator StaggerCo()
    {
        m_Animator.enabled = false;
        EnemyState interuptedState = m_State;
        m_State = EnemyState.Stagger;
        yield return new WaitForSeconds(0.2f);
        m_State = interuptedState;
        m_Animator.enabled = true;
    }

    private IEnumerator AttackCo()
    {
        m_IsAttacking = true;
        Debug.Log("Enemy attacks");
        yield return new WaitForSeconds(1f);
        m_IsAttacking = false;
    }
}

public enum EnemyState 
{
    Idle,
    Move,
    Attack,
    Stagger,
}
