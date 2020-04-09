using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    private Image m_DialogImage;
    private Text m_DialogTextObj;

    private void Awake()
    {
        m_DialogImage = GetComponent<Image>();
        m_DialogTextObj = GetComponentInChildren<Text>();
    }

    public void OnDialogInteractionEvent()
    {
        m_DialogImage.enabled = !m_DialogImage.enabled;
        m_DialogTextObj.enabled = !m_DialogTextObj.enabled;
        m_DialogTextObj.text = "Event worked!";
    }

    public void OnDialogCloseEvent()
    {
        m_DialogImage.enabled = false;
        m_DialogTextObj.enabled = false;
    }
}
