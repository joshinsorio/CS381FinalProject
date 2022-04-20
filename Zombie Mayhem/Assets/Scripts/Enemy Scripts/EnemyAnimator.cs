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

    public void Run(bool run)
    {
        m_Anim.SetBool("Run", run);
    }

    public void Attack(bool attack)
    {
        m_Anim.SetBool("Attack", attack);
    }

    public void Dead()
    {
        m_Anim.SetTrigger("Dead");
    }
}
