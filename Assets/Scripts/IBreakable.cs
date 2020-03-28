using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBreakable
{
    void ReceiveDamage(int i_ReceivedDamage);
    void DestroySelf();
}
