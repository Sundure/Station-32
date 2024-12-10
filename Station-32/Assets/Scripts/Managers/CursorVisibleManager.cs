using UnityEngine;

public class CursorVisibleManager : MonoBehaviour
{
    private static CursorVisibleManager _instance;

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

        SceneManager.OnNonMenuSceneLoaded += HideCursor;
        SceneManager.OnMenuSceneLoaded += ShowCursor;
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void ChangeCursorVisible(bool enabled)
    {
        Cursor.visible = enabled;
        Cursor.lockState = !enabled ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void OnDestroy()
    {
        SceneManager.OnNonMenuSceneLoaded -= HideCursor;
        SceneManager.OnMenuSceneLoaded -= ShowCursor;
    }
}
