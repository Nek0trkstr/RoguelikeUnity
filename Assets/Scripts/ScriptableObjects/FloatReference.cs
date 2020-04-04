using System;
using UnityEngine;

[Serializable]
public class FloatReference : MonoBehaviour
{
    public FloatValue m_FloatValue;
    public float m_ConstantValue;
    public bool m_UseConstantValue;

    public float Value
    {
        get { return m_UseConstantValue ? m_ConstantValue : m_FloatValue.m_RuntimeValue; }
    }
}
