using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator m_enemyAnim;
    private UnityEngine.AI.NavMeshAgent m_navMeshAgent;
    private EnemyController m_enemyController;

    public float m_health = 100f;
    public bool m_isPlayer, m_isZombie;
    private bool m_isDead;

    PlayerStats m_playerStats;

    void Awake()
    {
        if (m_isZombie)
        {
            m_enemyAnim = GetComponent<EnemyAnimator>();
            m_enemyController = GetComponent<EnemyController>();
            m_navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        }
        if (m_isPlayer)
        {
            m_playerStats = GetComponent<PlayerStats>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        if (m_isDead)
            return;

        m_health -= damage;

        if (m_isPlayer)
        {
            //display health UI
            m_playerStats.Display_HealthStats(m_health);
        }

        if (m_isZombie)
        {
            if(m_enemyController.m_enemyState == EnemyState.PATROL)
            {
                m_enemyController.m_chaseDistance = 50f;
            }
        }

        if(m_health <= 0f)
        {
            PlayerDied();
            m_isDead = true;
        }
    }
    
    void PlayerDied()
    {
        if (m_isZombie)
        {
            m_navMeshAgent.velocity = Vector3.zero;
            m_navMeshAgent.isStopped = true;
            m_enemyController.enabled = false;
            m_enemyAnim.Dead();
            KillCounter.instance.killCount++;
            KillCounter.instance.UpdateKillCounterUI();

            //spawn more enemies
            EnemyManager.m_instance.EnemyDied();
        }

        if (m_isPlayer)
        {
            //stop all enemies
            GameObject[] m_enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for(int i = 0; i < m_enemies.Length; i++)
            {
                m_enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            //stop spawning enemies
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }

        if(tag == "Player")
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ZombieMayhem");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
}
