using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioBehaviourOnCollide : MonoBehaviour
{
    private AudioSource launchHookAudioSource;
    public bool onCollide = true;
    public bool onTrigger = true;
    
    // Start is called before the first frame update
    void Start()
    {
        launchHookAudioSource = GetComponent<AudioSource>();
        if (launchHookAudioSource == null) throw new Exception(" audio source is required");
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(onTrigger) launchHookAudioSource.Play();
    }
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other1)
    {
        if(onCollide) launchHookAudioSource.Play();
    }
}
