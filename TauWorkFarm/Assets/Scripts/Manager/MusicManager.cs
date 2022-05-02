using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<MusicManager>();
            return instance;
        }
    }

    [SerializeField] AudioSource musicSource;
    [SerializeField] GameObject efxSourcePrefab;
    [SerializeField] int efxSourceCount;

    List<AudioSource> efxSources;

    //[SerializeField] AudioSource effectSource;
    [SerializeField] float timeToSwitch;

    [SerializeField] AudioClip playOnStart;

    private void Start()
    {
        Play(playOnStart, true);
        Init();
    }

    private void Init()
    {
        efxSources = new List<AudioSource>();

        for (int i = 0; i < efxSourceCount; i++)
        {
            GameObject gobj = Instantiate(efxSourcePrefab, transform);
            gobj.transform.localPosition = Vector3.zero;
            efxSources.Add(gobj.GetComponent<AudioSource>());
        }
    }

    public void Play(AudioClip musicToPlay, bool interrupt = false)
    {
        if (musicToPlay == null) { return; }

        if (interrupt)
        {
            musicSource.volume = 0.2f; //1f
            musicSource.clip = musicToPlay;
            musicSource.Play();
        }
        else
        {
            switchTo = musicToPlay;
            StartCoroutine(SmoothSwitchMusic());
        }
        
    }

    AudioClip switchTo;
    float volume;

    IEnumerator SmoothSwitchMusic()
    {
        volume = 1f;
        while (volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch;
            if (volume < 0f) { volume = 0f; }
            musicSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(switchTo, true);
    }

    public void PlayEfx(AudioClip audioClip)
    {
        if (audioClip == null) { return; }
        AudioSource efxSource = GetFreeEfxSource();

        efxSource.clip = audioClip;
        efxSource.Play();
    }

    private AudioSource GetFreeEfxSource()
    {
        for (int i = 0; i < efxSourceCount; i++)
        {
            if (efxSources[i].isPlaying == false)
            {
                return efxSources[i];
            }
        }

        return efxSources[0];
    }
}
