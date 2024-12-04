using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _gameAudioMixer;
    [SerializeField] private AudioMixer _musicAudioMixer;

    public AudioMixer GameAudioMixer { get { return _gameAudioMixer; } }
    public AudioMixer MusicAudioMixer { get { return _musicAudioMixer; } }

    private float _volumeScale;

    private bool _isAwaiking = true;

    private static AudioManager Instance;
    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += AudioAwake;

        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);


        DontDestroyOnLoad(gameObject);

        float db = Mathf.Log10(Mathf.Clamp(GameAudio.GameAudioValue, 0.000001f, 1)) * 20;

        GameAudioMixer.SetFloat("Audio", db * _volumeScale);
    }

    private void AudioAwake(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneManager.MenuScene)
        {
            return;
        }

        _isAwaiking = true;

        _volumeScale = 0;
    }

    private void Update()
    {
        if (_isAwaiking)
        {
            _volumeScale += Time.deltaTime / 3;

            if (_volumeScale >= 1)
            {
                _volumeScale = 1;

                _isAwaiking = false;
            }
        }

        float db = (GameAudio.GameAudioValue * _volumeScale * (20 - -80)) + -80;

        GameAudioMixer.SetFloat("Audio", db);
    }
}
