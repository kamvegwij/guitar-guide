using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayArea : MonoBehaviour
{
    [SerializeField] private Button menuButton;

    private void Start()
    {
        menuButton.onClick.AddListener(OpenMenu);
    }

    private void OpenMenu()
    {
        SceneManager.LoadScene(0); //menu has build index 0;
    }
}
