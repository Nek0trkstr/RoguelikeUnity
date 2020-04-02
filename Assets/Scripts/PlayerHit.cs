﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D i_Other)
    {
        IBreakable otherObject = i_Other.GetComponent<IBreakable>();
        if (otherObject != null)
        {
            otherObject.ReceiveDamage(5);
        }
    }
}
