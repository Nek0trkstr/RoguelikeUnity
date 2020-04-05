using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public FloatValue m_Damage;
    void OnTriggerEnter2D(Collider2D i_Other)
    {
        if (i_Other.CompareTag("Hurtbox"))
        {
            Hurtbox hurtbox = i_Other.GetComponent<Hurtbox>();
            if (hurtbox != null)
            {
                hurtbox.ReceiveDamage(m_Damage.m_RuntimeValue);
            }
        }
        
    }
}
