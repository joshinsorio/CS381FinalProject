using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager m_weaponManager;
    public float m_fireRate = 15f;
    private float m_nextTimetoFire;
    public float m_damage = 20f;
    private GameObject m_crosshair;
    private Camera m_mainCam;

    void Awake()
    {
        m_weaponManager = GetComponent<WeaponManager>();
        m_crosshair = GameObject.FindWithTag("Crosshair");
        m_mainCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
    }

    void WeaponShoot()
    {
        //If assault rifle
        if(m_weaponManager.GetCurrentSelectedWeapon().m_fireType == WeaponFireType.MULTIPLE)
        {
            if(Input.GetMouseButton(0) && Time.time > m_nextTimetoFire)
            {
                m_nextTimetoFire = Time.time + 1f / m_fireRate;
                m_weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                BulletFired();
            }
        }
        //Any other weapon
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(m_weaponManager.GetCurrentSelectedWeapon().tag == "Axe")
                {
                    m_weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                if (m_weaponManager.GetCurrentSelectedWeapon().m_bulletType == WeaponBulletType.BULLET)
                {
                    m_weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                    BulletFired();
                }
            }
        }
    }

    void BulletFired()
    {
        RaycastHit m_hit;
        if(Physics.Raycast(m_mainCam.transform.position, m_mainCam.transform.forward, out m_hit))
        {
            if(m_hit.transform.tag == "Enemy")
            {
                m_hit.transform.GetComponent<HealthScript>().ApplyDamage(m_damage);
            }
        }
    }
}
