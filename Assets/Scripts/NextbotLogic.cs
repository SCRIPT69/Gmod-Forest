using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class NextbotLogic : MonoBehaviour
{
    [SerializeField] float _speed = 33;
    private CharacterController _target;
    private CharacterController _hitBox;

    public readonly UnityEvent OnNextbotEnteredAttackZone = new UnityEvent();
    public readonly UnityEvent OnNextbotLeftAttackZone = new UnityEvent();

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        _target = player.GetComponent<CharacterController>();

        _hitBox = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        chaseTarget();

        checkForAttackZone();
    }

    private void chaseTarget()
    {
        Vector3 dir = (_target.transform.position - _hitBox.transform.position).normalized;
        _hitBox.Move(dir * _speed * Time.deltaTime);
    }


    private bool _appeared = false;
    private void checkForAttackZone()
    {
        if (Vector3.Distance(_target.transform.position, _hitBox.transform.position) < 60)
        {
            if (!_appeared)
            {
                _appeared = true;
                OnNextbotEnteredAttackZone.Invoke();
            }
        }
        else if (_appeared == true)
        {
            _appeared = false;
            OnNextbotLeftAttackZone.Invoke();
        }
    }
}
