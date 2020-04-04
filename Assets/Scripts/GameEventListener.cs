﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent m_Event;
    public UnityEvent m_Response;

    private void OnEnable()
    {
        m_Event.Register(this);
    }

    private void OnDisable()
    {
        m_Event.Unregister(this);
    }

    public void OnRaise()
    {
        m_Response.Invoke();
    }
}
