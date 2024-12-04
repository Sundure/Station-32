using UnityEngine;
using UnityEngine.UI;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private Slider _gameAudioSlider;

    public static float GameAudioValue { get; private set; } = 0.5f;

    private void Start()
    {
        _gameAudioSlider.onValueChanged.AddListener(ChangeGameAudioValue);

        _gameAudioSlider.value = GameAudioValue;
    }

    private void ChangeGameAudioValue(float value)
    {
        GameAudioValue = value;
    }
}
