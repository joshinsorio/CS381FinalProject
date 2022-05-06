using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource m_audioSource;
    public AudioClip m_screamClip, m_dieClip;
    public AudioClip[] m_attackClip;

    void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayScreamSound()
    {
        m_audioSource.clip = m_screamClip;
        m_audioSource.Play();
    }

    public void PlayAttackSound()
    {
        m_audioSource.clip = m_attackClip[Random.Range(0, m_attackClip.Length)];
        m_audioSource.Play();
    }

    public void PlayDeadSound()
    {
        m_audioSource.clip = m_dieClip;
        m_audioSource.Play();
    }
}
