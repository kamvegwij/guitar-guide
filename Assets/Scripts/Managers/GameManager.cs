using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameManager
{
    public static int totalCardFlips = 0;//variables to be shared across scripts
    public static int points = 0;
    public static int matches = 0;
    public static int gameMode = 0;
    public static int incorrectSelections = 0;
    public static int comboStreak = 1;
    public static string comboInfo = comboStreak.ToString();
    public static List<GameObject> cardDeck;

    public static void ResetStats()
    {
        totalCardFlips = 0;//variables to be shared across scripts
        points = 0;
        matches = 0;
        incorrectSelections = 0;
        comboStreak = 1;
}
    public static void IncreaseMatchCount()
    {
        
        comboStreak++;
        matches++;
        totalCardFlips++;
        points += 50 * comboStreak; 
    }

    public static void CardMismatch()
    {
        comboStreak = 1;
        incorrectSelections++;
    }

    
}
