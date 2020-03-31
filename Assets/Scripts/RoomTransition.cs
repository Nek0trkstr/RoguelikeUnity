using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public CameraMovement m_CameraPosition;
    public Vector3 m_PlayerPositionChange;
    public Room m_NextRoom;

    void OnTriggerEnter2D(Collider2D i_Other)
    {
        if (i_Other.CompareTag("Player"))
        {
            m_CameraPosition.m_MaxPosition = m_NextRoom.m_MaxCameraPosition;
            m_CameraPosition.m_MinPosition = m_NextRoom.m_MinCameraPosition;
            i_Other.transform.position += m_PlayerPositionChange;
        }
    }
}
