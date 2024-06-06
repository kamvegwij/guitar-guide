using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardFlip : MonoBehaviour
{
    [SerializeField] private Sprite cardFront;
    [SerializeField] private Sprite cardBack;

    private CardTable cardTable;
    private Image cardImage; //the object component holding sprites.
    private CardManager cardManager;
    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cardTable = GameObject.Find("CardTable").GetComponent<CardTable>();

        cardManager = GetComponent<CardManager>();
        cardImage = GetComponent<Image>();

        cardImage.sprite = cardFront; //default state when starting
        cardImage.raycastTarget = false; //disable interaction when showing the cards in start.
        cardManager.isFlipped = false;

        Invoke("ShowCardStart", 2.0f);
    }
    public void FlipCard()
    {
        if (cardImage == null) return; //if component doesn't exist.

        if (cardManager.isFlipped)
        {
            cardImage.sprite = cardBack;
            cardManager.isFlipped = false;
        }
        else
        {
            //Debug.Log(GetComponent<CardManager>().cardType); //for testing
            cardImage.sprite = cardFront;
            cardManager.isFlipped = true;
        }
        cardTable.HandleMatching();
        gameManager.totalCardFlips++;
    }
    public void ChangeCardFront(Sprite sprite)
    {
        cardFront = sprite;
    }
    private void ShowCardStart()
    {
        //called when placing new cards.
        cardImage.sprite = cardBack;
        cardImage.raycastTarget = true;
    }
}
