using UnityEngine;
using System.Collections;

public class DoorVector3 : Structure
{
    [Header("Values")]
    [SerializeField] private bool _open;

    [Range(0, 360)][SerializeField] private float _targetX;
    [Range(0, 360)][SerializeField] private float _targetY;
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

    private Vector3 _velocity;

    private Vector3 _angle;
    private Vector3 _targetAngle;

    private float _previousAnimationPrecent;

    private void Awake()
    {
        enabled = false;

        _angle = _doorAxis.localRotation.eulerAngles;
    }

    private void Update()
    {
        float precent = GetValuePrecent(transform.localEulerAngles.z, _targetAngle.z);

        if (precent >= 99.7f || _previousAnimationPrecent > precent && _previousAnimationPrecent != 0)
        {
            _doorAxis.localRotation = Quaternion.Euler(_targetAngle);

            enabled = false;

            return;
        }

        _angle = Vector3.SmoothDamp(_angle, _targetAngle, ref _velocity, _rotationTime);

        _doorAxis.localRotation = Quaternion.Euler(_angle);

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
            _targetAngle = new Vector3(_targetX, _targetY, _targetZ);
        else
            _targetAngle = Vector3.zero;

        _velocity = Vector3.zero;

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
