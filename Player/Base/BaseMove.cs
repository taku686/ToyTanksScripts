using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMove : MonoBehaviour
{
    private const string _joystickName = "BaseMove";
    private const float _slopeLimit = 20;
    private const float _stepOffset = 0.5f;
    private const float _skinWidth = 0.08f;
    private const float _radius = 0.8f;
    private const float _height = 1f;
    private const float _gravity = 20f;
    private Vector3 _center = new Vector3(0, 0.85f, 0);

    private Rigidbody _rigidbody;
    private CharacterController _characterController;
    Vector3 dir = Vector3.zero;

    private void Start()
    {
        //  _rigidbody = this.gameObject.AddComponent<Rigidbody>();
        InitializeCharcterController();
    }

    private void InitializeCharcterController()
    {
        _characterController = this.gameObject.AddComponent<CharacterController>();
        _characterController.slopeLimit = _slopeLimit;
        _characterController.slopeLimit = _slopeLimit;
        _characterController.stepOffset = _stepOffset;
        _characterController.skinWidth = _skinWidth;
        _characterController.radius = _radius;
        _characterController.height = _height;
        _characterController.center = _center;
    }

    public void Move(float speed)
    {
        float hori;
        float vert;

        if (Application.platform == RuntimePlatform.Android)
        {
            hori = UltimateJoystick.GetHorizontalAxis(_joystickName);
            vert = UltimateJoystick.GetVerticalAxis(_joystickName);
        }
        else
        {
            hori = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");
        }

        if (_characterController.isGrounded)
        {
            dir = new Vector3(hori, 0, vert);
            dir *= speed;
        }
        dir.y -= _gravity * Time.deltaTime;
        _characterController.Move(dir * Time.deltaTime);
        Rotate(hori, vert);
    }

    private void Rotate(float hori, float vert)
    {
        if (hori != 0 || vert != 0)
        {
            var direction = new Vector3(hori, 0, vert);
            transform.localRotation = Quaternion.LookRotation(direction);
        }
    }

}
