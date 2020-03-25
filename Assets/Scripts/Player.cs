using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_MoovingSpeed = 2f;
    public Rigidbody2D m_RigidBody;
    private Vector2 m_MoveDirection; 
 
    private void FixedUpdate()
    {
        m_MoveDirection.y = Input.GetAxisRaw("Vertical");
        m_MoveDirection.x = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    private void Update()
    {
        m_RigidBody.MovePosition(m_RigidBody.position + m_MoveDirection * m_MoovingSpeed * Time.fixedDeltaTime);
    }
}
