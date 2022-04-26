using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator m_Anim;

    void Awake()
    {
        m_Anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk(bool walk)
    {
        m_Anim.SetBool("Walk", walk);
    }

    public void Chase(bool chase)
    {
        m_Anim.SetBool("Chase", chase);
    }

    public void Attack()
    {
        m_Anim.SetTrigger("Attack");
    }

    public void Dead()
    {
        m_Anim.SetTrigger("Dead");
    }
}
