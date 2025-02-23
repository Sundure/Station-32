using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Door : Structure
{

    [Header("Values")]
    [SerializeField] private bool _open;

    [Range(1, 360)][SerializeField] private float _animationAxis;

    [Range(0.1f, 0.99f)][SerializeField] private float _rotationTime;
    [SerializeField] private float _minSpeed = 5;

    [SerializeField] private Transform _doorAxis;

    [Header("Target Axis (Only One Axis Can Be Rotate)")]
    [SerializeField] private bool _targetXAxis;
    [SerializeField] private bool _targetYAxis;
    [SerializeField] private bool _targetZAxis;

    [Header("Door States")]
    [SerializeField] private bool _canOpen = true;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _openDoorClip;
    [SerializeField] private AudioClip _closeDoorClip;
    [SerializeField] private AudioClip _tryOpenDoorClip;

    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;

    private float _velocity;

    private float _angle;
    private float _previusAngle;
    private float _targetAngle;

    private Action<float> _rotate;
    private Func<float> _getEulerAngleValue;

    private void Awake()
    {
        switch (true)
        {
            case var _ when _targetXAxis:
                _rotate = (axis) => _doorAxis.localRotation = Quaternion.Euler(axis, _doorAxis.localRotation.y, _doorAxis.localRotation.z);
                _getEulerAngleValue = () => _doorAxis.localEulerAngles.x;
                break;
            case var _ when _targetYAxis:
                _rotate = (axis) => _doorAxis.localRotation = Quaternion.Euler(_doorAxis.localRotation.x, axis, _doorAxis.localRotation.z);
                _getEulerAngleValue = () => _doorAxis.localEulerAngles.y;
                break;
            case var _ when _targetZAxis:
                _rotate = (axis) => _doorAxis.localRotation = Quaternion.Euler(_doorAxis.localRotation.x, _doorAxis.localRotation.y, axis);
                _getEulerAngleValue = () => _doorAxis.localEulerAngles.z;
                break;
        }
        _angle = 0;

        enabled = false;
    }

    private void Update()
    {
        _angle = Mathf.SmoothDamp(_angle, _targetAngle, ref _velocity, _rotationTime);

        if (_previusAngle > 0)
        {
            if (_open)
                _angle = _angle < _previusAngle + _minSpeed && _open ? _previusAngle + _minSpeed * Time.deltaTime * 70 : _angle;
            else
                _angle = _previusAngle - _angle < _minSpeed ? _previusAngle - _minSpeed * Time.deltaTime * 70 : _angle;
        }

        _previusAngle = _angle;

        _rotate(_angle);

        float currentEulerAngle = _getEulerAngleValue();

        if (_open)
        {
            if (_targetAngle - currentEulerAngle <= _minSpeed || currentEulerAngle >= _targetAngle)
            {
                _rotate(_targetAngle);

                enabled = false;
            }
        }
        else
        {
            if (currentEulerAngle - _targetAngle <= _minSpeed || currentEulerAngle <= _targetAngle || _previusAngle < 0)
            {
                _rotate(_targetAngle);

                _audioSource.PlayOneShot(_closeDoorClip);

                enabled = false;
            }
        }
    }

    protected override void Use()
    {
        if (_canOpen == false)
        {
            _audioSource.PlayOneShot(_tryOpenDoorClip);
            return;
        }

        _open = !_open;

        if (_open)
        {
            _audioSource.PlayOneShot(_openDoorClip);
            _targetAngle = _animationAxis;
        }
        else
            _targetAngle = 0;

        _previusAngle = 0;
        _velocity = 0;

        enabled = true;

        StopAllCoroutines();

        StartCoroutine(StopAnimation());
    }

    private IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(10);
        enabled = false;
    }
}
