using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour, IBreakable
{
    private int m_Health = 1;
    private bool m_IsDestroyed = false;
    private Animator m_Animator;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void ReceiveDamage(int i_ReceivedDamage)
    {
        Debug.Log("Received dmg!");
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
