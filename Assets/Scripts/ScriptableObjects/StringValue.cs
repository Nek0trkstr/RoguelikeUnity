using System;
using UnityEngine;

[CreateAssetMenu]
public class StringValue : ScriptableObject, ISerializationCallbackReceiver
{
    public string m_InitialValue;

    [NonSerialized]
    public string m_RuntimeValue;

    public void OnAfterDeserialize()
    {
        m_RuntimeValue = m_InitialValue;
    }

    public void OnBeforeSerialize() { }
}