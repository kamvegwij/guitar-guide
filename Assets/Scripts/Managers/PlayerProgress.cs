using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
    public int totalCardFlips;
    public int points;
    public int matches;
    public int incorrectSelections;
    public int comboStreak;

    //constructor
    public PlayerProgress()
    {
        //load progress
        totalCardFlips = GameManager.totalCardFlips; 
        points = GameManager.points;
        matches = GameManager.matches;
        incorrectSelections = GameManager.incorrectSelections;
        comboStreak= GameManager.comboStreak;
    }
}
