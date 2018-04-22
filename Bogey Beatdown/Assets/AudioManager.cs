using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }

    [HideInInspector]
    public GlobalAudioList audioList;
    [HideInInspector]
    public List<AudioSource> source;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        audioList = GetComponent<GlobalAudioList>();
        var sources = GetComponentsInChildren<AudioSource>();
        for (int i = 1; i < sources.Length; ++i)
            source.Add(sources[i]);
    }

    public static void PlaySFX(AudioClip clip, float volume)
    {
        foreach (var s in Instance.source)
        {
            if (!s.isPlaying)
                s.PlayOneShot(clip, volume);
            else if(s == Instance.source[Instance.source.Count - 1])
            {
                Instance.source[0].Stop();
                Instance.source[0].PlayOneShot(clip, volume);
            }
        }
    }

    public static void ChangeMusic(AudioClip clip)
    {
        var source = Instance.GetComponent<AudioSource>();
        source.Stop();
        source.clip = clip;
        source.Play();
    }
}
