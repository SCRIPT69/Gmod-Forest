using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsOfMovement : MonoBehaviour
{
    [SerializeField] string _soundName = "step";
    private PlayerMovement _movement;

    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _movement.OnStartedMoving.AddListener(startPlayingStepSound);
        _movement.OnStopped.AddListener(stopPlayingStepSound);
    }

    private void startPlayingStepSound()
    {
        StartCoroutine(playingStepSound());
    }
    private IEnumerator playingStepSound()
    {
        if (!AudioManager.Instance.IsPlaying(_soundName))
        {
            AudioManager.Instance.Play(_soundName);
        }
        yield return new WaitForSeconds(getStepTimeInterval());
        StartCoroutine(playingStepSound());
    }

    private float getStepTimeInterval()
    {
        float speed = GetComponent<PlayerMovement>().CurrentSpeed;
        if (speed <= 8) // crawling step interval
        {
            return 0.55f;
        }
        else if (speed <= 16) // walking step interval
        {
            return 0.45f;
        }
        else // running step interval
        {
            return 0.3f;
        }
    }

    private void stopPlayingStepSound()
    {
        StopAllCoroutines();
    }
}
