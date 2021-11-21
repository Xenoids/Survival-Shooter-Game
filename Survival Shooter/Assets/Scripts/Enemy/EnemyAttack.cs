using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float timeBetweenATK = 0.5f;
    public int ATKdmg = 10;

    Animator anim;
    GameObject p;

    PlayerHealth pHP;

    EnemyHealth eHP;

    bool pInRange;

    float timer;

    void Awake()
    {
        // cari tag "Player"
        p = GameObject.FindGameObjectWithTag("Player");

        // dptkan komponen p HP
        pHP = p.GetComponent<PlayerHealth>();

        // dptkan komponen Animator
        anim = GetComponent<Animator>();

        eHP = GetComponent<EnemyHealth>();
    }

    // panggil balik kalo ada object masuk ke dlm trigger
    void OnTriggerEnter(Collider other)
    {
        // set p in range
        if(other.gameObject == p) pInRange = true;
    }

    // panggil balik jika object kluar dari trigger
    void OnTriggerExit(Collider other)
    {
        // Set p not in range
        if(other.gameObject == p) pInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenATK && pInRange /* && eHP.currHP > 0 */ )
        {
            Attack();
        }

        // mentrigger animasi player ded kalo darah p <= 0
        if(pHP.currHP <= 0) anim.SetTrigger("PlayerDead");
    }

    void Attack()
    {
        // reset timer
        timer = 0f;

        // Taking dmg
        if(pHP.currHP > 0) pHP.Takedmg(ATKdmg);
    }
}
