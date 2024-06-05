using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;
    public Button playBtn;
    public Button quitBtn;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);
    }

    public void ChangeScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene > SceneManager.sceneCount)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene); //load scene by index.
        return;
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        ChangeScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
