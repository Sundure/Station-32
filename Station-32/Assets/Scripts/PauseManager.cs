using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool Pause {  get; private set; }

    public static event Action<bool> OnGamePause;

    private static PauseManager _instance;

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Pause = !Pause;

        Time.timeScale = Pause ? 0.0f : 1.0f;

        OnGamePause?.Invoke(Pause);

        CursorVisibleManager.ChangeCursorVisible(Pause);
    }
}
