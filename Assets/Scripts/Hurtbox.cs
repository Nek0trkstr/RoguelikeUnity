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
    
    private void Awake()
    {
        m_Parent = GetComponentInParent<IBreakable>();
        m_Collider2D = GetComponent<Collider2D>();
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
        m_Collider2D.enabled = false;
        yield return new WaitForSeconds(i_Interval);
        m_Collider2D.enabled = true;
    }
}
