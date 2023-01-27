using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] Sound[] _sounds;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        initialize();
    }

    private void initialize()
    {
        foreach (Sound sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.AudioClip;
            sound.source.volume = sound.Volume;
            sound.source.pitch = sound.Pitch;
            sound.source.loop = sound.Loop;
        }
    }

    public void Play(string soundName)
    {
        Sound sound = findSound(soundName);
        sound.source.Play();
    }

    public void Stop(string soundName)
    {
        Sound sound = findSound(soundName);
        sound.source.Stop();
    }

    public bool IsPlaying(string soundName)
    {
        Sound sound = findSound(soundName);
        return sound.source.isPlaying;
    }

    public void FadeOutSound(string soundName, float fadingTime)
    {
        StartCoroutine(fadeOutSound(soundName, fadingTime));
    }
    private IEnumerator fadeOutSound(string soundName, float fadingTime)
    {
        Sound sound = findSound(soundName);
        float startVolume = sound.source.volume;
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;

        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            sound.source.volume = Mathf.SmoothStep(startVolume, 0, t);
            yield return null;
        }

        sound.source.Stop();
        sound.source.volume = startVolume;
    }

    private Sound findSound(string soundName)
    {
        Sound sound = Array.Find(_sounds, sound => sound.Name == soundName);
        if (sound == null)
        {
            throw new Exception($"Sound {soundName} wasn't found.");
        }
        return sound;
    }
}
