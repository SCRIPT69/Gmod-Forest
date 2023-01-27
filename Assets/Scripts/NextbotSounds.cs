using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextbotSounds : MonoBehaviour
{
    [SerializeField] string[] _chasingSoundsNames;
    [SerializeField] string _deathScreamerSoundName;

    void Start()
    {
        //Subscribing for required events
        GetComponent<NextbotLogic>().OnNextbotEnteredAttackZone.AddListener(playScarySounds);
        GetComponent<NextbotAppearance>().OnNextbotDisappear.AddListener(stopSmoothlyScarySounds);
        PlayerDeath.OnPlayerDeath.AddListener(stopAllNextbotChasingSounds);
        PlayerDeath.OnPlayerDeath.AddListener(playDeathScreamerSound);
    }

    private void playScarySounds()
    {
        StopAllCoroutines();
        if (!AudioManager.Instance.IsPlaying(_chasingSoundsNames[0]))
        {
            foreach (string soundName in _chasingSoundsNames)
            {
                AudioManager.Instance.Play(soundName);
            }
        }
    }

    private void stopSmoothlyScarySounds()
    {
        foreach (string soundName in _chasingSoundsNames)
        {
            AudioManager.Instance.FadeOutSound(soundName, 1);
        }
    }

    private void stopAllNextbotChasingSounds()
    {
        foreach (string soundName in _chasingSoundsNames)
        {
            AudioManager.Instance.Stop(soundName);
        }
    }

    private void playDeathScreamerSound()
    {
        AudioManager.Instance.Play(_deathScreamerSoundName);
    }
}
