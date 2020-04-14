using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    private Image m_DialogImage;
    private Text m_DialogTextObj;
    public StringValue m_MessageToShow;

    private void Awake()
    {
        m_DialogImage = GetComponent<Image>();
        m_DialogTextObj = GetComponentInChildren<Text>();
    }

    public void OnDialogInteractionEvent()
    {
        m_DialogTextObj.text = m_MessageToShow.m_RuntimeValue;
        m_DialogImage.enabled = !m_DialogImage.enabled;
        m_DialogTextObj.enabled = !m_DialogTextObj.enabled;
    }

    public void OnDialogCloseEvent()
    {
        m_DialogImage.enabled = false;
        m_DialogTextObj.enabled = false;
    }
}
