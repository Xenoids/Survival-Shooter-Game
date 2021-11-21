using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform p;
    PlayerHealth pHP;

    EnemyHealth eHP;

    UnityEngine.AI.NavMeshAgent nav;

    private void Awake()
    {
        // cari GO dgn tag player
        p = GameObject.FindGameObjectWithTag("Player").transform;

        // dptkan reference komponen
        pHP = p.GetComponent<PlayerHealth>();
        eHP = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

   private void Update()
    {
        // pindahin posisi player
        if(eHP.currHP > 0 && pHP.currHP > 0)
        nav.SetDestination(p.position);
        else nav.enabled = false; // hentikan pergerakan
    }

}
