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
        SceneManager.LoadScene(0);
    }

    public void ExitStealthLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void EnterStealthLevel()
    {
        SceneManager.LoadScene(2);
    }
}
