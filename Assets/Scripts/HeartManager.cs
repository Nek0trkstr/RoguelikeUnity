using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] m_HeartImages;
    public Sprite m_FullHeartSprite;
    public Sprite m_HalfHeartSprite;
    public Sprite m_EmptyHeartSprite;
    public FloatValue m_InitialPlayerHealth;

    private void Start()
    {
        for (int i = 0; i < m_InitialPlayerHealth.m_Value; i++)
        {
            m_HeartImages[i].sprite = m_FullHeartSprite;
            m_HeartImages[i].enabled = true;
        }
    }
}
