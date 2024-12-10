using UnityEngine;

public class StopAudioOnPause : MonoBehaviour
{
    private AudioSource _audio;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();

        PauseManager.OnGamePause += StopAudio;
    }

    private void StopAudio(bool enabled)
    {
        if (enabled == false)
            _audio.Play();
        else
            _audio.Stop();
    }

    private void OnDestroy()
    {
        PauseManager.OnGamePause -= StopAudio;
    }
}
