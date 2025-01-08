using System.Collections;
using UnityEngine;
public class Crowbar : Item
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip[] _audioClips;

    [SerializeField] private float _atackInterval;

    [SerializeField] private int _atackAnimCount;

    private int _lastRandomAtackNumber;
    private int _lastRandomAtackAudioClipNumber;

    private bool _readyToAtack = true;

    private void Start()
    {
        _animator.enabled = false;
    }

    public override void Use()
    {
        if (_readyToAtack)
            Attack();
    }
    protected override void OverrideOnItemDrop()
    {
        _animator.enabled = false;
    }

    private void Attack()
    {
        int randomNumber = GetRandomNumber(ref _lastRandomAtackNumber, _atackAnimCount, 1);

        _audioSource.clip = _audioClips[GetRandomNumber(ref _lastRandomAtackAudioClipNumber, _audioClips.Length -1, 0)];
        _audioSource.Play();

        _animator.SetInteger("Atack Anim", randomNumber);

        PlayerCamera.GetComponentRaycast<ICrowbarInteractable>()?.Interact();

        StartCoroutine(RemoveAnim());
        StartCoroutine(WaitAtackInterval());
    }

    private int GetRandomNumber(ref int lastNumber, int maxNumber, int minNumber)
    {
        int random = Random.Range(minNumber, lastNumber + 1);
        while (true)
        {
            if (lastNumber == random)
                random = Random.Range(minNumber, maxNumber + 1);
            else break;
        }

        lastNumber = random;

        return random;
    }

    private IEnumerator RemoveAnim()
    {
        yield return null;
        _animator.SetInteger("Atack Anim", 0);
    }
    private IEnumerator WaitAtackInterval()
    {
        _readyToAtack = false;

        yield return new WaitForSeconds(_atackInterval);

        _readyToAtack = true;
    }

    private void OnDisable()
    {
        _readyToAtack = false;
        _animator.enabled = false;
    }
    private void OnEnable()
    {
        _readyToAtack = true;
        _animator.enabled = true;
    }
}
