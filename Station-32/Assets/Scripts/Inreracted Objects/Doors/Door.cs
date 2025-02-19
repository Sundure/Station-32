using System.Collections;
using UnityEngine;

public class Door : Structure
{
    [Header("Values")]
    [SerializeField] private bool _open;

    [Range(0, 360)][SerializeField] private float _targetZ;

    [Range(0, 0.99f)][SerializeField] private float _rotationTime;

    [SerializeField] private Transform _doorAxis;
    [Header("Door States")]
    [SerializeField] private bool _canOpen;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _openDoorClip;
    [SerializeField] private AudioClip _closeDoorClip;
    [SerializeField] private AudioClip _tryOpenDoorClip;

    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;

    private float _velocity;

    private float _angle;
    private float _targetAngle;

    private float _previousAnimationPrecent;

    private void Awake()
    {
        enabled = false;

        _angle = _doorAxis.localRotation.eulerAngles.z;
    }

    private void Update()
    {
        float precent = GetValuePrecent(transform.localEulerAngles.z, _targetAngle);

        if (precent >= 99.7f || _previousAnimationPrecent > precent && _previousAnimationPrecent != 0)
        {
            _doorAxis.localRotation = Quaternion.Euler(_doorAxis.localRotation.x, _doorAxis.localRotation.y, _targetAngle);

            enabled = false;

            return;
        }

        _angle = Mathf.SmoothDamp(_angle, _targetAngle, ref _velocity, _rotationTime);

        _doorAxis.localRotation = Quaternion.Euler(_doorAxis.localRotation.x, _doorAxis.localRotation.y, _angle);

        _previousAnimationPrecent = precent;
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
            _targetAngle = _targetZ;
        else
            _targetAngle = 0;

        _velocity = 0;

        _previousAnimationPrecent = 0;

        enabled = true;

        StopAllCoroutines();

        StartCoroutine(StopAnimation());
    }

    /// <summary>
    /// Get Precent Betwen First And Second Value.
    /// If First Value More That First Returns Precent Betwen Second And First Value
    /// </summary>
    /// <param name="firstValue"></param>
    /// <param name="secondValue"></param>
    /// <returns></returns>
    private float GetValuePrecent(float firstValue, float secondValue)
    {
        if (secondValue <= 0.1f) secondValue = 0.1f;

        if (firstValue > secondValue)
            return (secondValue / firstValue) * 100;

        return (firstValue / secondValue) * 100;
    }

    private IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(10);
        enabled = false;
    }
}
