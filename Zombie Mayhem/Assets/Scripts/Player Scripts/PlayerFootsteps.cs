using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip[] m_footstepClip;
    public float m_volumeMin, m_volumeMax;
    private AudioSource m_footstepSound;
    private CharacterController m_characterController;
    private float m_accumulatedDistance;
    public float m_stepDistance;

    void Awake()
    {
        m_footstepSound = GetComponent<AudioSource>();
        m_characterController = GetComponentInParent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        if (!m_characterController.isGrounded)
        {
            return;
        }

        //When not moving
        if(m_characterController.velocity.sqrMagnitude > 0)
        {
            //How far classifies a step
            m_accumulatedDistance += Time.deltaTime;

            if(m_accumulatedDistance > m_stepDistance)
            {
                m_footstepSound.volume = Random.Range(m_volumeMin, m_volumeMax);
                m_footstepSound.clip = m_footstepClip[Random.Range(0, m_footstepClip.Length)];
                m_footstepSound.Play();

                m_accumulatedDistance = 0;
            }
        }
        else
        {
            m_accumulatedDistance = 0f;
        }
    }
}
