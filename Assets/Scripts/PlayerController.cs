using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerDeath _playerDeath;

    private PlayerMovement _movement;
    private PlayerCrawl _crawling;
    private PlayerJump _jump;
    private PlayerPickUp _pickUp;

    void Start()
    {
        _playerDeath = GetComponent<PlayerDeath>();

        _movement = GetComponent<PlayerMovement>();
        _crawling = GetComponent<PlayerCrawl>();
        _jump = GetComponent<PlayerJump>();
        _pickUp = GetComponent<PlayerPickUp>();
    }

    void Update()
    {
        if (_playerDeath.IsAlive) // if player is alive, then allowing control to all actions
        {
            _movement.ControlMovement();
            _crawling.ControlCrawl();
            _jump.ControlJump();
            _pickUp.ControlPickUp();
        }
    }
}
