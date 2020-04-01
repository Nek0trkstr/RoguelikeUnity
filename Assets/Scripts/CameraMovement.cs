using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform m_PlayerTransform;
    public Vector2 m_MaxPosition;
    public Vector2 m_MinPosition;
    public float m_Smoothing;

    void LateUpdate()
    {
        Vector3 newCameraPosition = new Vector3(m_PlayerTransform.position.x, m_PlayerTransform.position.y, -10);
        newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, m_MinPosition.x, m_MaxPosition.x);
        newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, m_MinPosition.y, m_MaxPosition.y);
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, m_Smoothing);
        GetComponent<Camera>().orthographicSize = 3;
    }
}
