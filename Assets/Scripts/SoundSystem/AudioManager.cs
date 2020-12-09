using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    void Start()
    {
        // print(sounds);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); //each component of the sound

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        Play("MainMusic");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("ERROR: INVALID SOUND");
            return;
        }
        // print($"Playing {name}");
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("ERROR: INVALID SOUND");
            return;
        }
        // print($"Playing {name}");
        s.source.Stop();
    }
}
