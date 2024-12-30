using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool _enabled;

    [SerializeField] private AudioClip _flashlightEnabledClip;
    [SerializeField] private AudioClip _flashlightDisabledClip;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private Light _light;
    private void Awake()
    {
        _light.enabled = false;

        PlayerInputSystem.OnInputF += SwitchFlashlight;
    }

    private void SwitchFlashlight()
    {
        _enabled = !_enabled;

        _light.enabled = _enabled;

        _audioSource.clip = _enabled ? _flashlightEnabledClip : _flashlightDisabledClip;

        _audioSource.Play();
    }

    private void OnDestroy()
    {
        PlayerInputSystem.OnInputF -= SwitchFlashlight;
    }
}
