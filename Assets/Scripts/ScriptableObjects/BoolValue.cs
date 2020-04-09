using System;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    public bool m_InitialValue;

    [NonSerialized]
    public bool m_RuntimeValue;

    public void OnAfterDeserialize()
    {
        m_RuntimeValue = m_InitialValue;
    }

    public void OnBeforeSerialize() { }
}