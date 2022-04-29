using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform m_playerRoot, m_lookRoot;
    public bool m_invert;
    public bool m_unlockCursor = true;
    public float m_sensitivity = 2f;
    public int m_smoothSteps = 10;
    public float m_smoothWeight = 0.4f;
    public float m_rollAngle = 10f;
    public float m_rollSpeed = 1f;
    public Vector2 m_defaultLookLimits = new Vector2(-70f, 80f);
    private Vector2 m_lookAngle;
    private Vector2 m_currMouseLook;
    private Vector2 m_smoothMove;
    private float m_currentRollAngle;
    private int m_lastLookFrame;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CursorToggle();

         if(Cursor.lockState == CursorLockMode.Locked)
         {
             LookAround();
         }
    }

    void CursorToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            { 
                Cursor.lockState = CursorLockMode.None;
            }

            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround()
    {
        m_currMouseLook = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        m_lookAngle.x += m_currMouseLook.x * m_sensitivity * (m_invert ? 1f : -1f);
        m_lookAngle.y += m_currMouseLook.y * m_sensitivity;

        //Clamp angle to not go below -70 and above 80 (basically cant break your neck looking up)
        m_lookAngle.x = Mathf.Clamp(m_lookAngle.x, m_defaultLookLimits.x, m_defaultLookLimits.y);

        m_currentRollAngle = Mathf.Lerp(m_currentRollAngle, Input.GetAxisRaw("Mouse X") * m_rollAngle, Time.deltaTime * m_rollSpeed);

        //Rotate player and camera based on POV
        m_lookRoot.localRotation = Quaternion.Euler(m_lookAngle.x, 0f, m_currentRollAngle);
        m_playerRoot.localRotation = Quaternion.Euler(0f, m_lookAngle.y, 0f);
    }
}
