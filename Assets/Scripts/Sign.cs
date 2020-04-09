using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour, IInteractive
{
    public GameObject m_DialogToShow;
    public Text m_DialogText;
    public string m_DialogContext;
    public GameEvent m_SignInteractedEvent;
    public GameEvent m_SignCloseEvent;
    private bool m_IsPlayerInRange;
    private PlayerInputActions m_InputActions;
    private double m_LastActionTriggerTime = 0;

    private void Awake()
    {
        m_InputActions = new PlayerInputActions();
        // m_InputActions.PlayerControls.Attack.performed += OnPlayerAction;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         m_IsPlayerInRange = true;
    //     }
    //     
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         m_IsPlayerInRange = false;
    //         if (m_DialogToShow.activeInHierarchy)
    //         {
    //             m_DialogToShow.SetActive(false);
    //         }
    //     }
    // }

    private void OnPlayerAction(InputAction.CallbackContext i_Ctx)
    {
        if (m_IsPlayerInRange == true && (i_Ctx.time - m_LastActionTriggerTime) >= 0.5)
        {
            m_LastActionTriggerTime = i_Ctx.time;
            if (m_DialogToShow.activeInHierarchy)
            {
                m_DialogToShow.SetActive(false);
            }
            else
            {
                m_DialogToShow.SetActive(true);
                m_DialogText.text = m_DialogContext;
            }
        }
    }

    public void Interact()
    {
        m_SignInteractedEvent.Raise();
    }

    public void Close()
    {
        m_SignCloseEvent.Raise();
    }
    
    private void OnEnable()
    {
        m_InputActions.Enable();
    }

    private void OnDisable()
    {
        m_InputActions.Disable();
    }
}
