using UnityEngine;

public class StopAudioOnPause : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        PauseManager.OnGamePause += StopAudio;
    }

    private void StopAudio(bool enabled)
    {
        if (enabled == false && _audioSource.clip != null)
            _audioSource.Play();
        else if (_audioSource.clip != null)
            _audioSource.Stop();
    }

    private void OnDestroy()
    {
        PauseManager.OnGamePause -= StopAudio;
    }
}
