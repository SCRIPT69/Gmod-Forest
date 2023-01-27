using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextbotRotation : MonoBehaviour
{
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.LookAt(_player.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
