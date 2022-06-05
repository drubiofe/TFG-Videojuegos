using System;
using UnityEngine.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Audio[] audioClips;
    private float pitchChange = 0.1f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Audio a in audioClips)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
        }
    }

    public void PlayClip(string name)
    {
        Audio a = Array.Find(audioClips, audio => audio.name == name);
        if (a == null)
            return;
        if (a.isRandomizable)
        {
            a.source.pitch = Random.Range(a.pitch - pitchChange, a.pitch + pitchChange);
        }
        a.source.Play();
    }

    public static void PlayClipStatic(string name)
    {
        if (instance == null)
        {
            return;
        }
        instance.PlayClip(name);
    }
}
