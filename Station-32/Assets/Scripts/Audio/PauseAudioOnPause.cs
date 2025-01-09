using UnityEngine;

public class PauseAudioOnPause : MonoBehaviour
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
            _audioSource.UnPause();
        else if (_audioSource.clip != null)
            _audioSource.Pause();
    }

    private void OnDestroy()
    {
        PauseManager.OnGamePause -= StopAudio;
    }
}
