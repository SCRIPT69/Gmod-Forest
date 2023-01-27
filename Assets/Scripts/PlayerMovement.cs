using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public readonly UnityEvent OnStartedMoving = new UnityEvent();
    public readonly UnityEvent OnStopped = new UnityEvent();

    [SerializeField] float _crawlSpeed = 8;
    [SerializeField] float _walkSpeed = 16;
    [SerializeField] float _runSpeed = 27;

    private CharacterController _hitBox;
    private GravityController _gravityController;

    public float CurrentSpeed { get; private set; }
    public bool IsStaying { get; private set; } = true;


    private void Start()
    {
        _hitBox = gameObject.GetComponent<CharacterController>();
        _gravityController = GetComponent<GravityController>();
    }

    void Update()
    {
        setMovementTypeSpeed();
    }


    private void setMovementTypeSpeed()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            CurrentSpeed = _crawlSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            CurrentSpeed = _runSpeed;
        }
        else
        {
            CurrentSpeed = _walkSpeed;
        }
    }


    //Providing access for controlling movement from outside, by PlayerController
    public void ControlMovement()
    {
        controlMovement();
    }


    private void controlMovement() // private in case of some incapsulation
    {
        _hitBox.Move(GetMovementVector() * CurrentSpeed * Time.deltaTime);
    }

    public Vector3 GetMovementVector()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float rightInput = Input.GetAxis("Horizontal");
        Vector3 movement = transform.right * rightInput + transform.forward * forwardInput;

        if (movement.x != 0 || movement.z != 0)
        {
            if (IsStaying && _gravityController.IsGrounded)
            {
                OnStartedMoving.Invoke();
                IsStaying = false;
            }
        }
        else if (IsStaying == false)
        {
            OnStopped.Invoke();
            IsStaying = true;
        }
        if (!_gravityController.IsGrounded)
        {
            OnStopped.Invoke();
            IsStaying = true;
        }

        return movement;
    }
}
