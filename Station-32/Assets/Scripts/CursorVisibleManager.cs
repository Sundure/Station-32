using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorVisibleManager : MonoBehaviour
{
    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += ChangeCursorVisible;
    }

    private void ChangeCursorVisible(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == SceneManager.MenuScene)
        {
            return;
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void ChangeCursorVisible(bool enabled)
    {
        Cursor.visible = enabled;
        Cursor.lockState = !enabled ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
