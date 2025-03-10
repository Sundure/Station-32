using System;
using UnityEngine;

public class Door : Structure
{
    [Header("Values")]
    [SerializeField] private bool _negativeRotation;
    [SerializeField] private bool _open;

    [Range(1, 360)][SerializeField] private float _animationAxis;

    [Range(0.1f, 0.99f)][SerializeField] private float _rotationTime;
    [SerializeField] private float _minSpeed = 5;

    [SerializeField] private Transform _doorAxis;

    [SerializeField] private RotationAxis _rotationAxis;

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
    private float _previousAngle;
    private float _targetAngle;

    private Action<float> _rotate;

    private enum RotationAxis
    {
        X,
        Y,
        Z
    }

    private void Awake()
    {
        SetTargetRotationAxis();

        enabled = false;
    }

    private void Update()
    {
        _angle = Mathf.SmoothDamp(_angle, _targetAngle, ref _velocity, _rotationTime);

        float minDelta = _minSpeed * Time.deltaTime * 70;

        if (_angle != _previousAngle)
        {
            if (_open)
            {
                if (_negativeRotation)
                    _angle = _angle > _previousAngle - minDelta ? _previousAngle - minDelta : _angle;
                else
                    _angle = _angle < _previousAngle + minDelta ? _previousAngle + minDelta : _angle;
            }
            else
            {
                if (_negativeRotation)
                    _angle = _angle < _previousAngle + minDelta ? _previousAngle + minDelta : _angle;
                else
                    _angle = _angle > _previousAngle - minDelta ? _previousAngle - minDelta : _angle;
            }
        }

        _previousAngle = _angle;

        _rotate(_angle);

        if (_open)
        {
            if (Mathf.Abs(_previousAngle) + minDelta >= _targetAngle)
            {
                if (_negativeRotation)
                    _rotate(-_targetAngle);
                else
                    _rotate(_targetAngle);

                enabled = false;
            }
        }
        else
        {
            if (Mathf.Abs(_angle) <= minDelta)
            {
                _angle = 0;

                _rotate(_angle);

                _audioSource.PlayOneShot(_closeDoorClip);

                enabled = false;
            }
        }

    }

    protected override void Use()
    {
        if (_canOpen == false)
        {
            _audioSource.clip = _tryOpenDoorClip;
            _audioSource.Play();

            return;
        }

        _open = !_open;

        if (_open)
        {
            _audioSource.clip = _openDoorClip;
            _audioSource.Play();

            _targetAngle = _animationAxis;
        }
        else
            _targetAngle = 0;

        _velocity = 0;

        enabled = true;
    }

    private void SetTargetRotationAxis()
    {
        switch (_rotationAxis)
        {
            case RotationAxis.X:
                _rotate = (axis) => _doorAxis.localEulerAngles = new(axis, 0, 0);
                break;
            case RotationAxis.Y:
                _rotate = (axis) => _doorAxis.localEulerAngles = new(0, axis, 0);
                break;

            case RotationAxis.Z:
                _rotate = (axis) => _doorAxis.localEulerAngles = new(0, 0, axis);
                break;
        }
    }
}
