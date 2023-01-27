using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : MonoBehaviour
{
    public static readonly UnityEvent OnPlayerDeath = new UnityEvent();

    public bool IsAlive { get; private set; }


    void Start()
    {
        IsAlive = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Nextbot"))
        {
            IsAlive = false;
            OnPlayerDeath.Invoke();
        }
    }
}
