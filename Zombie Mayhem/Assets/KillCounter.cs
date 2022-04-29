using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static KillCounter instance;
    [SerializeField]
    TextMeshProUGUI killCounter_TMP;
    public int killCount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateKillCounterUI()
    {
        killCounter_TMP.text = killCount.ToString();
    }
}
