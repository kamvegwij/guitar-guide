using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTable : MonoBehaviour
{
    public CardManager playCard;
    public GameObject cardPrefab;
    //public GameObject currentSpawnCard;
    public Transform table; //current table with grid layout

    public List<CardManager> cardDeck;

    private void Start()
    {
        DrawCards(2, 3);
    }

    private void DrawCards(int row, int col)
    {
        for (int i = 0; i < row; i++)
        {
            for (int k = 0; k < col; k++)
            {
                GameObject spawnCard = Instantiate(cardPrefab, table.position, Quaternion.identity);
                spawnCard.transform.SetParent(table, false);
            }
        }
        
    }
}
