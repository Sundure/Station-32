using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _gameAudioMixer;
    [SerializeField] private AudioMixer _musicAudioMixer;

    public AudioMixerGroup GameAudioMixerGroup { get;private set; }
    public AudioMixerGroup MusicAudioMixerGroup { get;private set; }

    private const float _minVolume = -40;
    private const float _maxVolume = 20;
    private const float _silentVolume = -80;

    private float _volumeScale;

    private bool _isAwaiking = true;

    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);

            return;
        }
        SceneManager.OnNonMenuSceneLoaded += AudioAwake;

        AudioMixerGroup[] mixerGroup = _gameAudioMixer.FindMatchingGroups("Master");
        GameAudioMixerGroup = mixerGroup[0];
        mixerGroup = _musicAudioMixer.FindMatchingGroups("Master");
        MusicAudioMixerGroup = mixerGroup[0];

        DontDestroyOnLoad(gameObject);

        float db = Mathf.Log10(Mathf.Clamp(GameAudio.GameAudioValue, 0.000001f, 1)) * _maxVolume;

        _gameAudioMixer.SetFloat("Audio", db * _volumeScale);
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

        _gameAudioMixer.SetFloat("Audio", db);
    }

    private void OnDestroy()
    {
        SceneManager.OnNonMenuSceneLoaded -= AudioAwake;
    }
}
