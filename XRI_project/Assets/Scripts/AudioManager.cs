using HoetingProductions.Singleton; //make sure namespace matches company name in player settings
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [Header("Background Music Tracks")]

    [SerializeField]
    private AudioClip[] tracks; //select from list and eventually randomised
    private AudioSource audioSource;

    [Header("Events")]
    public Action onCurrentTrackEnded;

    public void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ShuffleWhenStopPlaying());
        ShuffleAndPlay();
    }

    public void ShuffleAndPlay(GameState gameState = GameState.Playing)//GameState comes from enum
    {
        if (tracks.Length > 0)
        {
            Debug.Log("Shuffle and play is working");
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);

            audioSource.clip = tracks[UnityEngine.Random.Range(0,tracks.Length -1)];
            audioSource.Play();

        }
    }
    private IEnumerator ShuffleWhenStopPlaying() //play another track when one completes
    {
        while (true)
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);
            ShuffleAndPlay();
            onCurrentTrackEnded?.Invoke(); //invokes an actionto anything listening
        }
    }
}
