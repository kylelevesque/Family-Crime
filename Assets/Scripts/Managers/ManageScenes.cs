using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public static ManageScenes Instance;

    private void Awake()
    {
        if(Instance !=null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ToMainMenu()
    {
        ToLoadingScreen(0);
    }

    public void ExitStealthLevel(int day)
    {
        ToLoadingScreen(day);
    }

    public void StartGame()
    {
        ToLoadingScreen(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void EnterStealthLevel(int night)
    {
        ToLoadingScreen(night);
    }

    void ToLoadingScreen(int sceneIndexToLoad)
    {
        SceneManager.LoadScene("Loading");
        Debug.Log("Hit Loading");
        SceneManager.LoadSceneAsync(sceneIndexToLoad);
    }
}
