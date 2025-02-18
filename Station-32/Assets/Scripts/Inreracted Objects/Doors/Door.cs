using System.Collections;
using UnityEngine;

public class Door : Structure
{
    [SerializeField] private bool _opened; // Close-Open Door State

    [Header("Door States")]
    [SerializeField] private bool _canOpen;

    private bool _canUse = true;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _openDoorClip;
    [SerializeField] private AudioClip _closeDoorClip;
    [SerializeField] private AudioClip _tryOpenDoorClip;

    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _animator;

    protected override void Use()
    {
        if (_canUse == false) return;

        if (_canOpen == false)
        {
            _audioSource.PlayOneShot(_tryOpenDoorClip);
            StartCoroutine(WaitEndOfAnimation(_tryOpenDoorClip.length));
        }

        else if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            _opened = !_opened;

            if (_opened)
            {
                _animator.SetTrigger("Open");
                _audioSource.PlayOneShot(_openDoorClip);
            }
            else
            {
                _animator.SetTrigger("Close");
                _audioSource.PlayOneShot(_closeDoorClip);
            }

            _canUse = false;

            StartCoroutine(GetAnimationTime()); // Idk How To Return Value In Coroutine 
        }
    }

    private IEnumerator GetAnimationTime()
    {
        yield return new WaitForEndOfFrame();

        StartCoroutine(WaitEndOfAnimation(_animator.GetCurrentAnimatorClipInfo(0).Length));
    }

    private IEnumerator WaitEndOfAnimation(float time)
    {
        yield return new WaitForSeconds(time);

        _canUse = true;
    }
}
