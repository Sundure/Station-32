using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager Instance;

    public static event Action OnSceneChange;

    [SerializeField] private string[] _scenes;
    public static string[] Scenes { get; private set; }

    public static readonly string MenuScene = "Menu";


    private void Awake()
    {
        Scenes = _scenes;

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoad;

        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static void LoadScene(string sceneName)
    {
        for (int i = 0; i < Scenes.Length; i++)
        {
            if (sceneName == Scenes[i])
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

                return;
            }
        }

        Debug.LogError("Scene Not Found");
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode) => OnSceneChange?.Invoke();
}
