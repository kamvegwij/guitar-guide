using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameManager
{
    public static int totalCardFlips = 0;//variables to be shared across scripts
    public static int totalScore = 0;
    public static int gameMode = 2;
    public static int incorrectSelections = 0;

    public static List<GameObject> cardDeck;
}
