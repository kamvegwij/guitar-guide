using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardFlip : MonoBehaviour
{
    [SerializeField] private Sprite cardFront;
    [SerializeField] private Sprite cardBack;

    private bool gameStarted = false;

    private CardTable cardTable;
    private Image cardImage; //the object component holding sprites.
    private CardManager cardManager;
    private SoundManager soundManager;

    private void Start()
    {
        cardTable = GameObject.Find("CardTable").GetComponent<CardTable>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        cardManager = GetComponent<CardManager>();
        cardImage = GetComponent<Image>();

        cardImage.sprite = cardFront; //default state when starting
        cardImage.raycastTarget = false; //disable interaction when showing the cards in start.
        cardManager.isFlipped = false;

        Invoke("ShowCardStart", 2.0f);
    }
    private void Update()
    {
        //TODO: optimise this code block.
        if (!gameStarted) return;
        if (!cardManager.isFlipped)
        {
            cardImage.sprite = cardBack;
        }
        else
        {
            cardImage.sprite = cardFront;
        }
    }
    public void FlipCard()
    {
        if (cardImage == null) return; //if component doesn't exist.
        soundManager.PlayFlipSound();
        cardManager.isFlipped = !cardManager.isFlipped;
        cardTable.HandleMatching();
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
        gameStarted = true;
    }
}
