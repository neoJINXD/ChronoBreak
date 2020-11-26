using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    // Start is called before the first frame update
    public Sound[] sounds;

    void Start()
    {
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
            return;

        s.source.Play();
        
    }
}
