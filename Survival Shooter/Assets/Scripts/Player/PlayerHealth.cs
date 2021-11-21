using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startHP = 100;
    public int currHP;

    public Slider HPSlider;

    public Image dmgImg;

    public AudioClip deadClip;

    public float flashSpeed = 5f;

    public Color flashColor = new Color(1f,0f,0f,0.1f);

    Animator anim;
    AudioSource pAudio;
    PlayerMovement pMovement;

    PlayerShooting PShooting;

    bool isded;
    bool dmged;

    void Awake()
    {
        // dptkan reference komponen
        anim = GetComponent<Animator>();
        pAudio = GetComponent<AudioSource>();
        pMovement = GetComponent<PlayerMovement>();

        PShooting = GetComponentInChildren<PlayerShooting>();
        currHP = startHP;
    }

    // Update is called once per frame
    void Update()
    {
        // kena dmged
        if(dmged) 
        {
            // merubah warna gambar menjadi value dari flashColor
            dmgImg.color = flashColor;
        }
        else
        {
            // fade out dmg img
            dmgImg.color =  Color.Lerp(dmgImg.color,Color.clear,flashSpeed * Time.deltaTime);
        }

        // set dmg to false
        dmged = false;
    }

    public void Takedmg(int amount)
    {
        dmged = true;

        // HP dikurangi
        currHP -= amount;

        // Merubah tampilan dri HP Slider
        HPSlider.value = currHP;

        // Memainkan suara kalo kena dmg
        pAudio.Play();

        // panggil method ded() kalo darah <= 10 dn blm mati
        if(currHP <= 0 && !isded) ded();


    }

    void ded()
    {
        isded = true;
        PShooting.DisableEffects();

        // mentrigger anim mati
        anim.SetTrigger("Die");

        // Memainkan suara kalo mati
        pAudio.clip = deadClip;
        pAudio.Play();

        // matikan script player pMovement
        pMovement.enabled = false;

        PShooting.enabled = false;
    }

    public void Restart()
    {
        // load ulang scene dgn idx 0 pd build setting
        SceneManager.LoadScene(0);
    }
}
