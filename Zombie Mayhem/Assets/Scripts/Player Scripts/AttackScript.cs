using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float m_damage = 25f;
    public float m_radius = 1f;
    public LayerMask m_layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] m_hits = Physics.OverlapSphere(transform.position, m_radius, m_layerMask);

        if(m_hits.Length > 0)
        {
            m_hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(m_damage);
            gameObject.SetActive(false);
        }
    }
}
