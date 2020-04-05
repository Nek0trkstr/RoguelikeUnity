using System;
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
    protected Renderer m_Renderer;

    public void ReceiveDamage(float i_Dmg)
    {
        m_HealthPoints = m_HealthPoints - i_Dmg;
        StartCoroutine(DoBlinksCo(3, 0.4f));
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
        yield return new WaitForSeconds(0.4f);
        m_State = interuptedState;
        m_Animator.enabled = true;
    }

    private IEnumerator AttackCo()
    {
        m_IsAttacking = true;
        yield return new WaitForSeconds(1f);
        m_IsAttacking = false;
    }
    
    IEnumerator DoBlinksCo(int i_BlinksNumber, float i_BlinkingLength)
    {
        float blingInterval = i_BlinkingLength / (i_BlinksNumber*2);
        for (int i=0; i<i_BlinksNumber*2; i++) {
            m_Renderer.enabled = !m_Renderer.enabled;
            yield return new WaitForSeconds(blingInterval);
        }
        
        m_Renderer.enabled = true;
    }
}

public enum EnemyState 
{
    Idle,
    Move,
    Attack,
    Stagger,
}
