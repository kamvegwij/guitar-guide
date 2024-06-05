using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTable : MonoBehaviour
{
    public CardManager playCard;
    public GameObject cardPrefab;

    public Transform table; //current table with grid layout

    public List<GameObject> spawnedCards;

    public List<GameObject> flippedCards;
    public List<GameObject> unflippedCards;

    private int currentTotalFlipped = 0;

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
                spawnCard.name = cardPrefab.name + counter; //every spawned card must be unique 
                spawnCard.transform.SetParent(table, false);

                spawnedCards.Add(spawnCard);

                counter++;
            }
        }
        
    }


    public void HandleMatching()
    {
        flippedCards.Clear(); //cleanup before check.
        unflippedCards.Clear();

        //keep track of cards on the table.
        for (int i = 0; i < spawnedCards.Count; i++)
        {
            CardManager currentCard = spawnedCards[i].GetComponent<CardManager>();
            if (currentCard.isFlipped)
            {
                flippedCards.Add(currentCard.gameObject);
            }
            else
            {
                unflippedCards.Add(currentCard.gameObject);
            }
        }
        currentTotalFlipped = flippedCards.Count;
    }
    private void ResetCards()
    {
        //
    }
}
