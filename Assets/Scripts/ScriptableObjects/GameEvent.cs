using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> m_GameEventListeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = m_GameEventListeners.Count; i >= 0; i--)
        {
            m_GameEventListeners[i].OnRaise();
        }
    }

    public void Register(GameEventListener i_Listener)
    {
        m_GameEventListeners.Add((i_Listener));
    }

    public void Unregister(GameEventListener i_Listener)
    {
        m_GameEventListeners.Remove(i_Listener);
    }

}
