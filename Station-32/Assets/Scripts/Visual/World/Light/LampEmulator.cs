using System.Collections;
using UnityEngine;

public class LampEmulator : MonoBehaviour
{
    [SerializeField] private Material _enabledMaterial;
    [SerializeField] private Material _disabledMaterial;

    [SerializeField] private float _disablingTime;
    [SerializeField] private float _enabligTime;

    private AudioSource _audioSource;

    private Light _light;

    private Renderer _renderer;

    private void Awake()
    {
        TryGetComponent(out _audioSource);

        if (_audioSource.clip == null )
            _audioSource = null;

        TryGetComponent(out _light);

        TryGetComponent(out _renderer);


        StartCoroutine(DisableLamp());
    }

    private IEnumerator EnableLamp()
    {
        yield return new WaitForSeconds(_enabligTime);

        _renderer.material = _enabledMaterial;

        if (_audioSource != null)
            _audioSource.Play();

        if (_light != null)
            _light.enabled = true;

        StartCoroutine(DisableLamp());
    }
    private IEnumerator DisableLamp()
    {
        yield return new WaitForSeconds(_disablingTime);

        _renderer.material = _disabledMaterial;

        if (_light != null)
            _light.enabled = false;

        StartCoroutine(EnableLamp());
    }
}
