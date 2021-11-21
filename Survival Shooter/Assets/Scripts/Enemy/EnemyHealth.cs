using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
   public int startHP = 100;
   public int currHP;

    public float sinkspd = 2.5f;

    public int scoreVal = 10;

    public AudioClip dedClip;

    Animator anim;
    AudioSource eAudio;

    ParticleSystem hitParticles;

    CapsuleCollider capsuleCollider;

    bool isded;
    bool issinking;

    void Awake()
    {
        //Mendptkan reference komponen
        anim = GetComponent<Animator>();
        eAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // set HP
        currHP = startHP;
    }

    void Update()
    {
        // jika sinking
        if(issinking) 
        {
            // pindah object ke bawah
            transform.Translate(-Vector3.up * sinkspd * Time.deltaTime);
        }
    }

    public void Takedmg(int amount, Vector3 hitPoint)
    {
        // check jika dead
        if(isded) return;

        // putar suara audio
        eAudio.Play();

        // kurangi HP
        currHP -= amount;

        // Ganti posisi particle
        hitParticles.transform.position = hitPoint;

        // Play particle System
        hitParticles.Play();

        // ded jika HP <= 0
        if(currHP <= 0) Ded();
    }

    void Ded()
    {
        // set isded
        isded = true;

        // set Capcollider ke trigger
        capsuleCollider.isTrigger = true;

        // trigger play anim Ded
        anim.SetTrigger("Dead");

        // play sound ded
        eAudio.clip = dedClip;
        eAudio.Play();
    }

    public void StartSinking()
    {
        //disable Navmesh Component
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //Set rb ke kinematic
        GetComponent<Rigidbody> ().isKinematic = true;
        issinking = true;
        ScoreManager.score += scoreVal;
        Destroy(gameObject, 2f);
    }

}
