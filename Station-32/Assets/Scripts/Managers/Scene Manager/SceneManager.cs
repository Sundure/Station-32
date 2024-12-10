using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;

    public static event Action OnMenuSceneLoaded;
    public static event Action OnNonMenuSceneLoaded;

    private const string MenuScene = "Menu";


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

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoad;

        DontDestroyOnLoad(gameObject);

        int sceneCount = Enum.GetValues(typeof(SceneList)).Length;

        if (UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings != sceneCount)
        {
            Debug.LogError($"\"Scene Manger\" Scenes Count Does Not Equal \"SceneList\" Scenes Count ");
        }

        for (int i = 0; i < Enum.GetValues(typeof(SceneList)).Length; i++)
        {
            string sceneName = ((SceneList)i).ToString();

            for (int j = 0; j < sceneCount; j++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(j);
                string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                if (sceneFileName == sceneName)
                {
                    break;
                }

               if (j == sceneCount -1)
               {
                   Debug.LogError($"\"{sceneName}\" Scene Does Not Exist");
              
                   break;
               }
            }

        }
    }

    public static void LoadScene(SceneList scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == MenuScene)
            OnMenuSceneLoaded?.Invoke();
        else
            OnNonMenuSceneLoaded?.Invoke();
    }

    private void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
