﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource carSource;
    public AudioSource catSource;
    public AudioSource pickUpSource;
    public AudioSource putDownSource;
    public AudioSource collisionSource;
    public AudioSource musicSource;

    public AudioClip carClip;
    public AudioClip catClip;
    public AudioClip pickUpClip;
    public AudioClip putDownClip;
    public AudioClip collisionClip;
    public AudioClip musicClip;


    public static SoundManager MasterSoundManager;

    void Start()
    {
        if (MasterSoundManager == null)
        {
            MasterSoundManager = this;
        }

        carSource = gameObject.AddComponent<AudioSource>();
        catSource = gameObject.AddComponent<AudioSource>();
        pickUpSource = gameObject.AddComponent<AudioSource>();
        putDownSource = gameObject.AddComponent<AudioSource>();
        collisionSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();

        carSource.clip = carClip;
        catSource.clip = catClip;
        pickUpSource.clip = pickUpClip;
        putDownSource.clip = putDownClip;
        collisionSource.clip = collisionClip;
        musicSource.clip = musicClip;

        musicSource.volume = 0.25f;
        musicSource.loop = true;
        musicSource.Play();

        /*
        carSource.volume = 0.5f;
        carSource.loop = true;
        carSource.Play();*/

    }
}
