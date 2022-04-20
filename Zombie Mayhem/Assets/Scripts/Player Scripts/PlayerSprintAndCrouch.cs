using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement m_playerMovement;
    public float m_sprintSpeed = 5f;
    public float m_moveSpeed = 3f;
    public float m_crouchSpeed = 1f;
    private Transform m_lookRoot;
    private float m_standHeight = 0;
    private float m_crouchHeight = -0.3f;
    private PlayerFootsteps m_playerFootsteps;
    private float m_sprintVol = 1f;
    private float m_crouchVol = 0.1f;
    private float m_walkVolMin = 0.2f, m_walkVolMax = 0.6f;
    private float m_walkStepDistance = 0.4f;
    private float m_sprintStepDistance = 0.25f;
    private float m_crouchStepDistance = 0.5f;


    // Awake is called before the first frame update
    void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerFootsteps = GetComponentInChildren<PlayerFootsteps>();
        m_lookRoot = transform.GetChild(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_playerFootsteps.m_volumeMin = m_walkVolMin;
        m_playerFootsteps.m_volumeMax = m_walkVolMax;
        m_playerFootsteps.m_stepDistance = m_walkStepDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_playerMovement.m_speed = m_sprintSpeed;
            m_playerFootsteps.m_stepDistance = m_sprintStepDistance;
            m_playerFootsteps.m_volumeMin = m_sprintVol;
            m_playerFootsteps.m_volumeMax = m_sprintVol;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_playerMovement.m_speed = m_moveSpeed;
            m_playerFootsteps.m_stepDistance = m_walkStepDistance;
            m_playerFootsteps.m_volumeMin = m_walkVolMin;
            m_playerFootsteps.m_volumeMax = m_walkVolMax;
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //Enable Crouch
            m_lookRoot.localPosition = new Vector3(0f, m_crouchHeight, 0f);
            m_playerMovement.m_speed = m_crouchSpeed;
            m_playerFootsteps.m_stepDistance = m_crouchStepDistance;
            m_playerFootsteps.m_volumeMin = m_crouchVol;
            m_playerFootsteps.m_volumeMax = m_crouchVol;

        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            //Disable Crouch
            m_lookRoot.localPosition = new Vector3(0f, m_standHeight, 0f);
            m_playerMovement.m_speed = m_moveSpeed;
            m_playerFootsteps.m_stepDistance = m_walkStepDistance;
            m_playerFootsteps.m_volumeMin = m_walkVolMin;
            m_playerFootsteps.m_volumeMax = m_walkVolMax;


        }
    }
}
