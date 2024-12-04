using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    private void Start()
    {
        _pausePanel.SetActive(false);

        PauseManager.OnGamePause += SetActivePanel;
    }

    private void SetActivePanel(bool active) => _pausePanel.SetActive(active);

    private void OnDestroy()
    {
        PauseManager.OnGamePause -= SetActivePanel;
    }
}
