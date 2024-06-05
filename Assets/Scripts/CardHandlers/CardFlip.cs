using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardFlip : MonoBehaviour
{
    [SerializeField] private Sprite cardFront;
    [SerializeField] private Sprite cardBack;

    private Image cardImage; //the object component holding sprites.
    private CardManager cardManager;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        cardManager = GetComponent<CardManager>();
        cardImage = GetComponent<Image>();
        ResetCard();
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
            cardImage.sprite = cardFront;
            cardManager.isFlipped = true;
        }
        gameManager.totalCardFlips++;
    }
    private void ResetCard()
    {
        //called when placing new cards.
        cardImage.sprite = cardBack; //default state
        cardManager.isFlipped = false;
    }
}
