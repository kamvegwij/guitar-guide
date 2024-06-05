using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTable : MonoBehaviour
{
    public CardManager playCard;
    public GameObject cardPrefab;

    public Transform table; //current table with grid layout

    public List<CardManager> cardDeck;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        LayoutFromDifficulty();
    }

    private void LayoutFromDifficulty()
    {
        switch(gameManager.gameMode)
        {
            case 0:
                Debug.Log("Easy Mode");
                DrawCards(2, 2);
                break;
            case 1:
                Debug.Log("Medium Mode");
                DrawCards(2, 3);
                break;
            case 2:
                Debug.Log("Difficult Mode");
                DrawCards(5, 6);
                break;
            default:
                break;
        }
        
    }
    private void DrawCards(int row, int col)
    {
        int counter = 1;
        for (int i = 0; i < row; i++)
        {
            for (int k = 0; k < col; k++)
            {
                GameObject spawnCard = Instantiate(cardPrefab, table.position, Quaternion.identity);
                spawnCard.name = cardPrefab.name + counter;
                spawnCard.transform.SetParent(table, false);
                counter++;
            }
        }
        
    }
}
