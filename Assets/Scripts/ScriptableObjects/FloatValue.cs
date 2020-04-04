using System;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float m_InitialValue;

    [NonSerialized]
    public float m_RuntimeValue;

    public void OnAfterDeserialize()
    {
        m_RuntimeValue = m_InitialValue;
    }

    public void OnBeforeSerialize() { }
    
    
}
