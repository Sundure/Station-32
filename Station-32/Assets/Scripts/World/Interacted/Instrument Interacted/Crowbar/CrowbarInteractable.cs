using System.Collections;
using UnityEngine;
public class CrowbarInteractable : MonoBehaviour, ICrowbarInteractable
{
    [SerializeField] private PhysicsManager _physicsManager;

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

        audioSource.outputAudioMixerGroup = AudioManager.Instance.GameAudioMixerGroup;
        audioSource.PlayOneShot(_onAtackClip[random]);


        _health--;

        StartCoroutine(Destroy(_onAtackClip[random].length, audioObject));

        if (_health == 0)
            Destroy();
    }

    private void Destroy()
    {
        _interacted = false;

        _physicsManager.RB.isKinematic = false;
        _physicsManager.RB.useGravity = true;
        _physicsManager.RB.collisionDetectionMode = CollisionDetectionMode.Continuous;

        _physicsManager.AddIgnoredRBLayer(PlayerProperties.PlayerLayer);
        StartCoroutine(Destroy(_destroyTime, gameObject));
    }

    private IEnumerator Destroy(float time, GameObject gameObject)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
