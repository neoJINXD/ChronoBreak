using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] sounds;

    public static AudioManager instance;


    void Awake()
    {
        if (instance == null) //dont have an existing instance
        {
            instance = this;
        }
        else //there is at least an existing instance
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); //dont destroy when loading another scence

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); //each component of the sound

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("MainMusic");
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        s.source.Play();
        
    }
}
