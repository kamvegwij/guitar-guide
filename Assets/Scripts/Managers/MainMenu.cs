using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playBtn;
    public Button quitBtn;
    public Slider difficultySlider;

    private void Start()
    {
        playBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);

        difficultySlider.value = 0; //default
        difficultySlider.onValueChanged.AddListener(val => ToggleDifficulty());
    }

    private void ToggleDifficulty()
    {
        GameManager.gameMode = (int)difficultySlider.value;

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
