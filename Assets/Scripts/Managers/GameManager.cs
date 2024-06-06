using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalCardFlips = 0;
    public int totalScore = 0;
    public int gameMode = 0;
    public int incorrectSelections = 0;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
