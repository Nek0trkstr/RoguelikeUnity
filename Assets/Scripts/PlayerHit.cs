using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D i_Other)
    {
        if (i_Other.CompareTag("Hurtbox"))
        {
            IBreakable otherObject = i_Other.transform.parent.gameObject.GetComponent<IBreakable>();
            if (otherObject != null)
            {
                Debug.Log("Atacked hurtbox");
                otherObject.ReceiveDamage(5);
            }
        }
        
    }
}
