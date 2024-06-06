using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour //important: this script should be above the CardFlip script in the game obj.
{
    public CARD_TYPE cardType;
    public bool isFlipped = false;

    [SerializeField] private List<Sprite> sprites = new List<Sprite>(); //sprites I want to attach to the card.
    private Sprite currentSprite;

    private void Start()
    {
        CreateCard();
        gameObject.GetComponent<CardFlip>().ChangeCardFront(currentSprite);
    }

    public void CreateCard()
    {
        switch(cardType) 
        {
            case CARD_TYPE.CROC:
                currentSprite = sprites[0];
                break;

            case CARD_TYPE.LION:
                currentSprite = sprites[1];
                break;

            case CARD_TYPE.SUN:
                currentSprite = sprites[2];
                break;

            case CARD_TYPE.TURTLE:
                currentSprite = sprites[3];
                break;

            default:
                break;
        }
    }
    public enum CARD_TYPE
    {
        BLANK, CROC, LION, SUN, TURTLE
    }
}
