using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour, IInteractive
{
    public GameEvent m_SignInteractedEvent;
    public GameEvent m_SignCloseEvent;
    public StringValue m_DialodueString;
    public string m_MessageToShow;
    
    public void Interact()
    {
        m_DialodueString.m_RuntimeValue = m_MessageToShow;
        m_SignInteractedEvent.Raise();
    }

    public void Close()
    {
        if (m_SignCloseEvent)
        {
            m_SignCloseEvent.Raise();
        }
    }
}
