using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D i_Other)
    {
        Debug.Log("Attacked!");
        IBreakable otherObject = i_Other.GetComponent<IBreakable>();
        otherObject.ReceiveDamage(5);
    }
}
