using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IBreakable
{
    public int m_HealthPoints;
    public float m_MooveSpeed;
    public string m_EnemyName;
    protected bool m_IsDestroyed = false;
    protected EnemyState m_State = EnemyState.Idle;

    public void ReceiveDamage(int i_Dmg)
    {
        m_HealthPoints = m_HealthPoints - i_Dmg;
        if ( m_HealthPoints <= 0)
        {
            DestroySelf();
        }
    }

    public virtual void DestroySelf()
    {
        m_IsDestroyed = true;
        Destroy(gameObject);
    }
}

public enum EnemyState 
{
    Idle,
    Move,
    Attack
}
