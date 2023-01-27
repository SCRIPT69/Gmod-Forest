using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrawl : MonoBehaviour
{
    private CharacterController _hitBox;
    private GravityController _gravity;

    [SerializeField] GameObject _playerBody;
    [SerializeField] GameObject _camera;
    [SerializeField] Transform _groundCheck;

    [SerializeField] float _flexionSize = 2.5f;
    private float _flexionDistance;

    private float _cameraStartPosY;
    private float _groundCheckStartPosY;
    private float _hitBoxStartHeight;
    private float _bodyStartHeight;
    private float _hitBoxStartCenterY;
    private float _bodyStartPosY;

    void Start()
    {
        _hitBox = gameObject.GetComponent<CharacterController>();
        _gravity = gameObject.GetComponent<GravityController>();

        _cameraStartPosY = _camera.transform.localPosition.y;
        _hitBoxStartHeight = _hitBox.height;
        _bodyStartHeight = _playerBody.transform.localScale.y;
        _groundCheckStartPosY = _groundCheck.transform.localPosition.y;
        _hitBoxStartCenterY = _hitBox.center.y;
        _bodyStartPosY = _playerBody.transform.localPosition.y;

        _flexionDistance = _flexionSize / 2;
    }


    //Providing access for controlling crawling from outside, by PlayerController
    public void ControlCrawl()
    {
        StartCoroutine(controlCrawl());
    }


    private bool _crawlWhileJumping;
    private IEnumerator controlCrawl()
    {
        //checking, if crawling starts while jumping
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _crawlWhileJumping = !_gravity.IsGrounded;
        }

        float cameraCrawlingPosY = _cameraStartPosY - _flexionDistance;
        float hitBoxCrawlingHeight = _hitBoxStartHeight - _flexionSize;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (_camera.transform.localPosition.y != cameraCrawlingPosY)
            {
                //starting crawling
                _hitBox.height = hitBoxCrawlingHeight;

                if (!_crawlWhileJumping) // if crawling in jump, then there should be no camera animation
                {
                    animateCameraPosYToNewValue(cameraCrawlingPosY, 10);
                }

                yield return new WaitForSeconds(0.2f); // delay for hitBox to move down before player body

                preparePlayerBodyForCrawling();
            }
        }
        else if (_camera.transform.localPosition.y != _cameraStartPosY || _crawlWhileJumping)
        {
            //standing up
            _hitBox.height = _hitBoxStartHeight;
            _hitBox.center = new Vector3(_hitBox.center.x, _hitBoxStartCenterY, _hitBox.center.z);

            _groundCheck.transform.localPosition = new Vector3(_groundCheck.transform.localPosition.x, _groundCheckStartPosY, _groundCheck.transform.localPosition.z);

            if (_crawlWhileJumping)
            {
                yield return new WaitForSeconds(0.0001f); // delay for _groundCheck to check, if player "isGrounded"
                if (_gravity.IsGrounded && _playerBody.transform.localScale.y != _bodyStartHeight)
                {
                    //moving the camera down for animation
                    _camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _cameraStartPosY - _flexionDistance, _camera.transform.localPosition.z);
                }
                _crawlWhileJumping = false; // casting _crawlWhileJumping to its initial value
            }

            animateCameraPosYToNewValue(_cameraStartPosY, 7);

            _playerBody.transform.localScale = new Vector3(_playerBody.transform.localScale.x, _bodyStartHeight, _playerBody.transform.localScale.z);
            _playerBody.transform.localPosition = new Vector3(_playerBody.transform.localPosition.x, _bodyStartPosY, _playerBody.transform.localPosition.z);
        }
    }
    private void animateCameraPosYToNewValue(float newPosY, float timeCoef)
    {
        Vector3 cameraNewPos = new Vector3(_camera.transform.localPosition.x, newPosY, _camera.transform.localPosition.z);
        _camera.transform.localPosition = Vector3.MoveTowards(_camera.transform.localPosition, cameraNewPos, Time.deltaTime * timeCoef);
    }
    private void preparePlayerBodyForCrawling()
    {
        float hitBoxCrawlingHeight = _hitBoxStartHeight - _flexionSize;
        float cameraCrawlingPosY = _cameraStartPosY - _flexionDistance;

        //if player has already standed up, then there is no need for changing player's body size
        if (_hitBox.height != hitBoxCrawlingHeight) { return; }

        if (_crawlWhileJumping)
        {
            //if player crawls while jumping, player's body bending should be centered to the camera
            changePlayerBodySizeForCrawling();
            centerPlayerBendToCamera();
        }
        else if (_camera.transform.localPosition.y == cameraCrawlingPosY)
        {
            //changing player's body size, only after the camera is moved down
            changePlayerBodySizeForCrawling();
        }
    }
    private void changePlayerBodySizeForCrawling()
    {
        _playerBody.transform.localScale = new Vector3(_playerBody.transform.localScale.x, _bodyStartHeight - _flexionDistance, _playerBody.transform.localScale.z);
        _groundCheck.transform.localPosition = new Vector3(_groundCheck.transform.localPosition.x, _groundCheckStartPosY + _flexionDistance, _groundCheck.transform.localPosition.z);
    }
    private void centerPlayerBendToCamera()
    {
        _hitBox.center = new Vector3(_hitBox.center.x, _hitBoxStartCenterY + _flexionDistance, _hitBox.center.z);
        _playerBody.transform.localPosition = new Vector3(_playerBody.transform.localPosition.x, _bodyStartPosY + _flexionDistance, _playerBody.transform.localPosition.z);
        _groundCheck.transform.localPosition = new Vector3(_groundCheck.transform.localPosition.x, _groundCheckStartPosY + _flexionSize, _groundCheck.transform.localPosition.z);
    }
}
