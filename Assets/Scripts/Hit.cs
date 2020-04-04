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
            IBreakable otherObject = i_Other.transform.parent.gameObject.GetComponent<IBreakable>();
            if (otherObject != null)
            {
                otherObject.ReceiveDamage(m_Damage.m_RuntimeValue);
            }
        }
        
    }
}
