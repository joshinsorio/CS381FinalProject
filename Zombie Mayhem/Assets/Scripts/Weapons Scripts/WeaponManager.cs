using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponHandler [] m_weapons;

    private int m_currentWeaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_currentWeaponIndex = 0;
        m_weapons[m_currentWeaponIndex].gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }
    }

    void TurnOnSelectedWeapon(int m_weaponIndex)
    {
        if (m_currentWeaponIndex == m_weaponIndex)
            return;

        m_weapons[m_currentWeaponIndex].gameObject.SetActive(false);
        m_weapons[m_weaponIndex].gameObject.SetActive(true);
        m_currentWeaponIndex = m_weaponIndex;
    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return m_weapons[m_currentWeaponIndex];
    }

}
