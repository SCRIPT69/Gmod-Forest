using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float _jumpHeight = 3.8f;
    [SerializeField] float _crawlJumpHeight = 2;
    [SerializeField] string _landSoundName = "step";

    private GravityController _gravity;
    private PlayerMovement _movement;
    private CharacterController _hitBox;


    void Start()
    {
        _gravity = gameObject.GetComponent<GravityController>();
        _movement = gameObject.GetComponent<PlayerMovement>();
        _hitBox = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        onGrounded(); // suppressing the inertia after jump
    }


    //Providing access for controlling crawling from outside, by PlayerController
    public void ControlJump()
    {
        controlJump();
    }


    private void controlJump()
    {
        if (Input.GetButtonDown("Jump") && _gravity.IsGrounded)
        {
            if (!AudioManager.Instance.IsPlaying(_landSoundName))
            {
                AudioManager.Instance.Play(_landSoundName);
            }

            if (Input.GetKey(KeyCode.LeftControl)) // jumping sitting down
            {
                _gravity.GravityStrength.y = Mathf.Sqrt(_crawlJumpHeight * 2 * _gravity.GravityCoef);
            }
            else
            {
                _gravity.GravityStrength.y = Mathf.Sqrt(_jumpHeight * 2 * _gravity.GravityCoef);
            }

            //movement inertia in jump
            _gravity.GravityStrength += (_movement.GetMovementVector() * _movement.CurrentSpeed);
        }
    }
    private bool _suppressingJumpInertia;
    private void onGrounded()
    {
        if (!_gravity.IsGrounded || _gravity.GravityStrength.y >= 0) { return; }
        if (_gravity.GravityStrength.z == 0 && _gravity.GravityStrength.x == 0) { return; }

        //slowly stopping jump inertia
        _gravity.GravityStrength.z -= _gravity.GravityStrength.z / 5;
        _gravity.GravityStrength.x -= _gravity.GravityStrength.x / 5;

        if (!_suppressingJumpInertia)
        {
            //playing landing sound
            if (!AudioManager.Instance.IsPlaying(_landSoundName))
            {
                AudioManager.Instance.Play(_landSoundName);
            }

            //fully suppressing the inertia after 0.05 seconds
            StartCoroutine(suppressJumpInertia());
            _suppressingJumpInertia = true;
        }
    }
    private IEnumerator suppressJumpInertia()
    {
        yield return new WaitForSeconds(0.06f);
        _gravity.GravityStrength.z = 0;
        _gravity.GravityStrength.x = 0;
        _suppressingJumpInertia = false;
    }
}
