using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
   public int DPS = 20;
   public float timebetweenBullets = 0.15f;

   public float range  = 100f;

   float timer;
   Ray shootRay = new Ray();
   RaycastHit shootHit;
   int shootableMask;
   ParticleSystem gunParticles;
   LineRenderer gunLine;
   AudioSource gunAudio;
   Light gunLight;
   float effectsDisplayTime = 0.2f;

   void Awake()
    {
        // GetMask
        shootableMask = LayerMask.GetMask("Shootable");
        // mendapatkan reference komponen
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if(Input.GetButton("Fire1") && timer>= timebetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if(timer >= timebetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        // disable line LineRenderer
        gunLine.enabled = false;

        // disable Light
        gunLight.enabled = false;
    }

    public void Shoot()
    {
        timer = 0f;
        // Play audio
        gunAudio.Play();

        // enable Light
        gunLight.enabled = true;

         //Play gun particle
        gunParticles.Stop ();
        gunParticles.Play ();
 
        //enable Line renderer dan set first position
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);
 
        //Set posisi ray shoot dan direction
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

          if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            //Lakukan raycast hit hace component Enemyhealth
            EnemyHealth eHP = shootHit.collider.GetComponent <EnemyHealth> ();
 
            if(eHP != null)
            {
                // take dmg
                eHP.Takedmg(DPS, shootHit.point);
            }
 
            //Set line end position ke hit position
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            //set line end position ke range barrel
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }

}
