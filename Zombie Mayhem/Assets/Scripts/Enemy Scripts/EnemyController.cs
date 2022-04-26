using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator m_enemyAnim;
    private UnityEngine.AI.NavMeshAgent m_navAgent;
    private EnemyState m_enemyState;
    public float m_walkSpeed = 0.5f;
    public float m_runSpeed = 4f;
    public float m_chaseDistance = 7f;
    private float m_currentChaseDistance;
    public float m_attackDistance = 1.8f;
    public float m_chaseAfterAttackDistance = 2f;
    public float m_patrolRadiusMin = 20f, m_patrolRadiusMax = 60f;
    public float m_patrolForThisTime = 15f;
    private float m_patrolTimer;
    public float m_waitBeforeAttack = 2f;
    private float m_attackTimer;
    private Transform target;

    void Awake()
    {
        m_enemyAnim = GetComponent<EnemyAnimator>();
        m_navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_enemyState = EnemyState.PATROL;
        m_patrolTimer = m_patrolForThisTime;
        m_attackTimer = m_waitBeforeAttack;
        m_currentChaseDistance = m_chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_enemyState == EnemyState.PATROL)
        {
            Patrol();
        }
        if (m_enemyState == EnemyState.CHASE)
        {
            Chase();
        }
        if (m_enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    void Patrol()
    {
        //Allow agent to move
        m_navAgent.isStopped = false;
        m_navAgent.speed = m_walkSpeed;

        m_patrolTimer += Time.deltaTime;

        if(m_patrolTimer > m_patrolForThisTime)
        {
            SetNewRandomDestination();
            m_patrolTimer = 0f;
        }
        
        if(m_navAgent.velocity.sqrMagnitude > 0)
        {
            m_enemyAnim.Walk(true);
        }
        else
        {
            m_enemyAnim.Walk(false);
        }

        if (Vector3.Distance(transform.position, target.position) <= m_chaseDistance)
        {
            m_enemyAnim.Walk(false);

            m_enemyState = EnemyState.CHASE;

        }
    }

    void Chase()
    {
        //Allow agent to move
        m_navAgent.isStopped = false;
        m_navAgent.speed = m_runSpeed;
        m_navAgent.SetDestination(target.position);

        if(m_navAgent.velocity.sqrMagnitude > 0)
        {
            m_enemyAnim.Chase(true);
        }
        else
        {
            m_enemyAnim.Chase(false);
        }

        //if within distance, attack
        if(Vector3.Distance(transform.position, target.position) <= m_attackDistance)
        {
            m_enemyAnim.Chase(false);
            m_enemyAnim.Walk(false);
            m_enemyState = EnemyState.ATTACK;

            if(m_chaseDistance != m_currentChaseDistance)
            {
                m_chaseDistance = m_currentChaseDistance;
            }
        }
        else if (Vector3.Distance(transform.position, target.position) > m_chaseDistance)
        {
            m_enemyAnim.Chase(false);
            m_enemyState = EnemyState.PATROL;

            m_patrolTimer = m_patrolForThisTime;

            if(m_chaseDistance != m_currentChaseDistance)
            {
                m_chaseDistance = m_currentChaseDistance;
            }

        }
        
    }

    void Attack()
    {
        m_navAgent.velocity = Vector3.zero;
        m_navAgent.isStopped = true;

        m_attackTimer += Time.deltaTime;

        if(m_attackTimer > m_waitBeforeAttack)
        {
            m_enemyAnim.Attack();
            m_attackTimer = 0f;
        }

        if(Vector3.Distance(transform.position, target.position) > m_attackDistance + m_chaseAfterAttackDistance)
        {
            m_enemyState = EnemyState.CHASE;
        }
    }

    void SetNewRandomDestination()
    {
        float rand_radius = Random.Range(m_patrolRadiusMin, m_patrolRadiusMax);

        Vector3 randDir = Random.insideUnitSphere * rand_radius;
        randDir += transform.position;

        UnityEngine.AI.NavMeshHit navHit;

        UnityEngine.AI.NavMesh.SamplePosition(randDir, out navHit, rand_radius, -1);
        m_navAgent.SetDestination(navHit.position);
    }
}
