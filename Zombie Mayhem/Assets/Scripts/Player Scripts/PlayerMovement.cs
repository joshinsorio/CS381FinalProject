using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController m_characterController;
    private Vector3 m_velocity;

    public float m_speed = 3f;
    public float m_jumpForce = 5f;
    private float m_gravity = 15f;
    private float m_verticalVelocity;


    void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        m_velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        m_velocity = transform.TransformDirection(m_velocity);
        m_velocity *= m_speed * Time.deltaTime;

        ApplyGravity();

        m_characterController.Move(m_velocity);

    }

    void ApplyGravity()
    {
        if (m_characterController.isGrounded)
        {
            m_verticalVelocity -= m_gravity * Time.deltaTime;

            PlayerJump();
        }
        else
        {
            m_verticalVelocity -= m_gravity * Time.deltaTime;
        }

        m_velocity.y = m_verticalVelocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if (m_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            m_verticalVelocity = m_jumpForce;
        }
    }

}
