using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _gameAudioMixer;
    [SerializeField] private AudioMixer _musicAudioMixer;

    private const float _minVolume = -40;
    private const float _maxVolume = 20;
    private const float _silentVolume = -80;

    public AudioMixer GameAudioMixer { get { return _gameAudioMixer; } }
    public AudioMixer MusicAudioMixer { get { return _musicAudioMixer; } }

    private float _volumeScale;

    private bool _isAwaiking = true;

    private static AudioManager _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);

            return;
        }

        SceneManager.OnNonMenuSceneLoaded += AudioAwake;

        DontDestroyOnLoad(gameObject);

        float db = Mathf.Log10(Mathf.Clamp(GameAudio.GameAudioValue, 0.000001f, 1)) * _maxVolume;

        GameAudioMixer.SetFloat("Audio", db * _volumeScale);
    }

    private void AudioAwake()
    {
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

        float db = (GameAudio.GameAudioValue * _volumeScale * (_maxVolume - _minVolume)) + _minVolume;

        db = db == _minVolume ? -_silentVolume : db;

        GameAudioMixer.SetFloat("Audio", db);
    }

    private void OnDestroy()
    {
        SceneManager.OnNonMenuSceneLoaded -= AudioAwake;
    }
}
