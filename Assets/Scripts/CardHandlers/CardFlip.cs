using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardFlip : MonoBehaviour
{
    [SerializeField] private Sprite cardFront;
    [SerializeField] private Sprite cardBack;

    public bool canBeFlipped = false;
    public bool isFlipped = false;

    private bool gameStarted = false;

    private Animator animator;
    private CardTable cardTable;
    private Image cardImage; //the object component holding sprites.
    private CardManager cardManager;
    private SoundManager soundManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cardManager = GetComponent<CardManager>();
        cardImage = GetComponent<Image>();
    }

    private void Start()
    {
        cardTable = GameObject.Find("CardTable").GetComponent<CardTable>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        

        cardImage.sprite = cardFront; //default state when starting
        cardImage.raycastTarget = false; //disable interaction when showing the cards in start.
        isFlipped = false;

        Invoke("ShowCardStart", 5.0f);
    }
    private void Update()
    {
        HandleCardImage();
    }
    private void HandleCardImage()
    {
        if (!gameStarted) return;

        if (!canBeFlipped)
        {
            cardImage.sprite = cardFront;
        }
        else
        {
            if (!isFlipped)
            {
                cardImage.sprite = cardBack;
            }
            else
            {
                cardImage.sprite = cardFront;
            }
        }
    }
    public void FlipCard()
    {
        if (cardImage == null) return; //if component doesn't exist.
        soundManager.PlayFlipSound();
        isFlipped = !isFlipped;
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
        if (cardManager.cardType != CardManager.CARD_TYPE.BLANK)
        {
            canBeFlipped = true;
            animator.Play("CardFlipAnim", 0, 0f);
        }
        else
        {
            canBeFlipped = false;
        }
    }
}
