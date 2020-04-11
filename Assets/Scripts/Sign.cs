using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour, IInteractive
{
    public GameObject m_DialogToShow;
    public Text m_DialogText;
    public string m_DialogContext;
    public GameEvent m_SignInteractedEvent;
    public GameEvent m_SignCloseEvent;

    public void Interact()
    {
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
