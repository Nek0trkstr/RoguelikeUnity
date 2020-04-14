using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public FloatValue m_UntouchableAfterDmg;
    private Collider2D m_Collider2D;
    private IBreakable m_Parent;
    private bool m_IsColliderEnabled = true;
    private bool m_IsPlayer;
    
    private void Awake()
    {
        m_Parent = GetComponentInParent<IBreakable>();
        m_Collider2D = GetComponent<Collider2D>();
        Player m_PlayerComponent = GetComponentInParent<Player>();
        m_IsPlayer = m_PlayerComponent ? true : false;
    }
    
    public void ReceiveDamage(float i_ReceivedDamage)
    {
        if (m_UntouchableAfterDmg.m_RuntimeValue > 0)
        {
            StartCoroutine(ToggleHitBoxCo(m_UntouchableAfterDmg.m_RuntimeValue));
        }

        m_Parent.ReceiveDamage(i_ReceivedDamage);
    }

    public IEnumerator ToggleHitBoxCo(float i_Interval)
    {
        if (m_IsColliderEnabled)
        {
            m_IsColliderEnabled = false;
            m_Collider2D.enabled = false;
            yield return new WaitForSeconds(i_Interval);
            m_Collider2D.enabled = true;
            m_IsColliderEnabled = true;
        }
    }

    public bool IsPlayer
    {
        get => m_IsPlayer;
    }
}
