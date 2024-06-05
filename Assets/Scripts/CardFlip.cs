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

    private bool isFlipped = false;

    private void Start()
    {
        cardImage = GetComponent<Image>();
    }
    public void FlipCard()
    {
        if (cardImage == null) return; //if component doesn't exist.

        if (isFlipped)
        {
            cardImage.sprite = cardFront;
        }
        else
        {
            cardImage.sprite = cardBack;
        }
    }
}
