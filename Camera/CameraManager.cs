using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    private const string _playerTag = "Player";
    private const string _joystick = "CanonMove";
    private const float _moveLimit = 3f;

    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField]
    private Camera _mainCamera;
    private CinemachineTransposer _transposer;
    private Vector3 _initPos;
    private float _backSpeed = 2;
    private float _moveSpeed = 7;
    private float _modifiedValue = 0.01f;

    public void Initialize()
    {
        Transform target = GameObject.FindGameObjectWithTag(_playerTag).transform;
        _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _virtualCamera.Follow = target;
        _initPos = _transposer.m_FollowOffset;
        _virtualCamera.LookAt = target;
    }

    private void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        float hori = UltimateJoystick.GetHorizontalAxis(_joystick);
        float vert = UltimateJoystick.GetVerticalAxis(_joystick);



        if (UltimateJoystick.GetJoystickState(_joystick))
        {
            _transposer.m_FollowOffset += new Vector3(hori, 0, vert) * _moveSpeed * Time.unscaledDeltaTime;
            if (_transposer.m_FollowOffset.x >= _initPos.x + _moveLimit + _modifiedValue ||
                _transposer.m_FollowOffset.x <= _initPos.x - _moveLimit - _modifiedValue)
            {
                _transposer.m_FollowOffset.x = Mathf.Clamp(_transposer.m_FollowOffset.x, _initPos.x - _moveLimit, _initPos.x + _moveLimit);
            }
            if (_transposer.m_FollowOffset.z >= _initPos.z + _moveLimit + _modifiedValue ||
                     _transposer.m_FollowOffset.z <= _initPos.z - _moveLimit - _modifiedValue)
            {
                _transposer.m_FollowOffset.z = Mathf.Clamp(_transposer.m_FollowOffset.z, _initPos.z - _moveLimit, _initPos.z + _moveLimit);
            }
        }
        if (!UltimateJoystick.GetJoystickState(_joystick))
        {
            _transposer.m_FollowOffset = Vector3.MoveTowards(_transposer.m_FollowOffset, _initPos, _backSpeed * Time.deltaTime);
        }
    }
}
