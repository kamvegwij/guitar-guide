using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayArea : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button volumeButton;
    [SerializeField] private AudioSource audioSource;
    private void Start()
    {
        menuButton.onClick.AddListener(OpenMenu);
        volumeButton.onClick.AddListener(ToggleVolume);
    }

    private void OpenMenu()
    {
        SceneManager.LoadScene(0); //menu has build index 0;
    }
    private void ToggleVolume()
    {
        audioSource.mute= !audioSource.mute;
    }
}
