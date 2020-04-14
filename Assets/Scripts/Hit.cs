using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public FloatValue m_Damage;
    private bool m_IsPlayer;

    private void Awake()
    {
        Player m_PlayerComponent = GetComponentInParent<Player>();
        m_IsPlayer = m_PlayerComponent ? true : false;
    }

    void OnTriggerEnter2D(Collider2D i_Other)
    {
        if (i_Other.CompareTag("Hurtbox"))
        {
            Hurtbox hurtbox = i_Other.GetComponent<Hurtbox>();
            if (hurtbox != null && hurtbox.IsPlayer != m_IsPlayer)
            {
                hurtbox.ReceiveDamage(m_Damage.m_RuntimeValue);
            }
        }
        
    }
}
