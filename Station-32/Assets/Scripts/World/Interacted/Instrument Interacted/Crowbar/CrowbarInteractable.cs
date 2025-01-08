using System.Collections;
using UnityEngine;
public class CrowbarInteractable : MonoBehaviour, ICrowbarInteractable
{
    [SerializeField] private IgnoredRBLayers _ignoredRBLayers;

    [SerializeField] private AudioClip[] _onAtackClip;

    [SerializeField] private float _destroyTime;

    [SerializeField] private int _health;

    private int _lastRandomValue = -1;

    private bool _interacted = true;

    public void Interact()
    {
        if (_interacted == false) return;

        GameObject audioObject = new();

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        int random = Random.Range(0, _onAtackClip.Length);

        while (true)
        {
            if (random == _lastRandomValue)
                random = Random.Range(0, _onAtackClip.Length);
            else break;
        }

        _lastRandomValue = random;

        audioSource.outputAudioMixerGroup = AudioManager.Instance.GameAudioMixer.outputAudioMixerGroup;
        audioSource.PlayOneShot(_onAtackClip[random]);


        _health--;

        StartCoroutine(DestroyGameObject(_onAtackClip[random].length, audioObject));

        if (_health == 0)
            Destroy();
    }

    private void Destroy()
    {
        _interacted = false;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        _ignoredRBLayers.AddIgnoredRBLayer(PlayerProperties.PlayerLayer);
        StartCoroutine(DestroyGameObject(_destroyTime, gameObject));
    }

    private IEnumerator DestroyGameObject(float time, GameObject gameObject)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
