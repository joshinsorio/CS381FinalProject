using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr2 : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;

    public void ToggleDeathPanel()
    {
        deathPanel.SetActive(!deathPanel.activeSelf);
    }
}
