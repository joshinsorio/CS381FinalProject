using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    NONE,
    BULLET
}

public class WeaponHandler : MonoBehaviour
{
    private Animator m_anim;
    public WeaponAim m_weaponAim;
    public GameObject m_muzzleFlash;
    public AudioSource m_shootSound, m_reloadSound;
    public WeaponFireType m_fireType;
    public WeaponBulletType m_bulletType;
    public GameObject m_attackPoint;

    void Awake()
    {
        m_anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootAnimation()
    {
        m_anim.SetTrigger("Shoot");
    }

    void Turn_On_MuzzleFlash()
    {
        m_muzzleFlash.SetActive(true);
    }

    void Turn_Off_MuzzleFlash()
    {
        m_muzzleFlash.SetActive(false);
    }

    void Play_ShootSound()
    {
        m_shootSound.Play();
    }

    void Play_ReloadSound()
    {
        m_reloadSound.Play();
    }

    void Turn_On_AttackPoint()
    {
        m_attackPoint.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (m_attackPoint.activeInHierarchy)
        {
            m_attackPoint.SetActive(false);
        }
    }
}
