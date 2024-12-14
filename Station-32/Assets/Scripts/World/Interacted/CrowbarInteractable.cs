using System.Collections;
using UnityEngine;
public class CrowbarInteractable : MonoBehaviour, IInstrumentInteractable
{
    [SerializeField] private AudioClip _onUseClip;

    [SerializeField] private float _destroyTime;

    private bool _interacted = true;
    public void Interact()
    {

        if (_interacted == false) return;

        _interacted = false;

        GameObject audioObject = new();

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = AudioManager.Instance.GameAudioMixer.outputAudioMixerGroup;
        audioSource.PlayOneShot(_onUseClip);

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.excludeLayers |= PlayerProperties.PlayerLayer;

        StartCoroutine(DestroyAudioObject(audioObject));

        StartCoroutine(DestroyGameObject());
    }

    private IEnumerator DestroyAudioObject(GameObject gameObject)
    {
        yield return new WaitForSeconds(_onUseClip.length);

        Destroy(gameObject);
    }
    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(_destroyTime);

        Destroy(gameObject);
    }
}
