using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] float _gravityCoef;
    public float GravityCoef
    {
        get
        {
            return _gravityCoef;
        }
        private set
        {
            _gravityCoef = value;
        }
    }
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundMask;

    [HideInInspector]
    public Vector3 GravityStrength;

    public bool IsGrounded { get; private set; }

    private CharacterController _hitBox;

    void Start()
    {
        _hitBox = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        calculateGravity();
        checkIsGrounded();
    }

    private void calculateGravity()
    {
        GravityStrength.y -= _gravityCoef * Time.deltaTime;
        _hitBox.Move(GravityStrength * Time.deltaTime);
    }

    private void checkIsGrounded()
    {
        float groundDistance = 0.05f;
        IsGrounded = Physics.CheckSphere(_groundCheck.position, groundDistance, _groundMask);

        if (IsGrounded && GravityStrength.y < 0) // slow down gravity strength
        {
            GravityStrength.y = -0.01f; // graviaty velocity
        }
    }
}
