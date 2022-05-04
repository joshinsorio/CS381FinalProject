using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager m_instance;
    public GameObject m_cannibalPrefab;
    public Transform[] m_cannibalSpawnPoints;
    public int m_cannibalEnemyCount;
    private int m_initialCannibalCount;
    public float m_waitBeforeSpawnEnemiesTime = 10f;

    void Awake()
    {
        MakeInstace();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_initialCannibalCount = m_cannibalEnemyCount;

        SpawnEnemiesTime();
        StartCoroutine("CheckToSpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeInstace()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
    }

    void SpawnEnemiesTime() {
        SpawnCannibals();
    }

    void SpawnCannibals()
    {
        int index = 0;
        for(int i = 0; i < m_cannibalEnemyCount; i++)
        {
            if(index >= m_cannibalSpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(m_cannibalPrefab, m_cannibalSpawnPoints[index].position, Quaternion.identity);
            index++;
        }

        m_cannibalEnemyCount = 0;
    }
    
    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(m_waitBeforeSpawnEnemiesTime);
        SpawnCannibals();
        StartCoroutine("CheckToSpawnEnemies");
    }

    public void EnemyDied()
    {
        m_cannibalEnemyCount = m_initialCannibalCount * 2;
        if(m_cannibalEnemyCount > m_initialCannibalCount)
        {
            m_cannibalEnemyCount = m_initialCannibalCount;
        }
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }

}
