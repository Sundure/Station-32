using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool Pause { get; private set; }

    public static event Action<bool> OnGamePause;

    private static PauseManager _instance;

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

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }
    }

    private void SwitchPause()
    {
        Pause = !Pause;

        Time.timeScale = Pause ? 0.0f : 1.0f;

        OnGamePause?.Invoke(Pause);

        CursorVisibleManager.ChangeCursorVisible(Pause);
    }
}
