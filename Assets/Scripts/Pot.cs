using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour, IBreakable
{
    private float m_Health = 1;
    private bool m_IsDestroyed = false;
    private Animator m_Animator;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void ReceiveDamage(float i_ReceivedDamage)
    {
        m_Health = m_Health - i_ReceivedDamage;
        if(m_Health <= 0)
        {
            m_Animator.SetBool("IsDestroyed", true);
        }
    }

    public void DestroySelf()
    {
        m_IsDestroyed = true;
    }
}
