using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound
{
    [SerializeField] string _name;
    public string Name
    {
        get
        {
            return _name;
        }
    }

    [SerializeField] AudioClip _audioClip;
    public AudioClip AudioClip
    {
        get
        {
            return _audioClip;
        }
    }

    [Range(0f, 1f)]
    [SerializeField] float _volume;
    public float Volume
    {
        get
        {
            return _volume;
        }
    }

    [Range(.1f, 3f)]
    [SerializeField] float _pitch;
    public float Pitch
    {
        get
        {
            return _pitch;
        }
    }

    [SerializeField] bool _loop;
    public bool Loop
    {
        get
        {
            return _loop;
        }
    }

    [HideInInspector]
    public AudioSource source;
}
