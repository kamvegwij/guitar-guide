using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardTable : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject completeScreen;
    public Transform table; //current table with grid layout
    public GridLayoutGroup gridLayout;


    public List<GameObject> itemsToHide; //hide these items when calling the game complete screen. manually add these
    public List<GameObject> spawnedCards; //store cards in this inventory system.
    public List<GameObject> flippedCards;
    public List<GameObject> unflippedCards;
    public List<CardManager.CARD_TYPE> availableCardTypes;

    public int currentTotalFlipped = 0;
    public int cardsSpawnTotal = 0; //use this to scale

    private CardManager.CARD_TYPE getCardType;
    private SoundManager soundManager;

    private void Start()
    {
        //gridLayout = GetComponent<GridLayoutGroup>();
        //cellSizes = gridLayout.cellSize;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        
        completeScreen.SetActive(false);
        availableCardTypes = new List<CardManager.CARD_TYPE>() { CardManager.CARD_TYPE.SUN , CardManager.CARD_TYPE.LION, CardManager.CARD_TYPE.CROC, CardManager.CARD_TYPE.TURTLE };
        LayoutFromDifficulty();
    }
    private void Update()
    {
        if (spawnedCards.Count == 0)
        {
            if (!completeScreen.gameObject.activeSelf)
            {
                completeScreen.SetActive(true);
                for (int i = 0; i < itemsToHide.Count; i++)
                {
                    //itemsToHide[i].gameObject.SetActive(false);
                }
                soundManager.PlayGameOverSound();
                Invoke("OnGameComplete", 5f);
            }
        }
    }

    private void OnGameComplete()
    {
        SceneManager.LoadScene(0);
    }
    
    private void LayoutFromDifficulty()
    {
        //this method changes the layout of the grid based on number of cards produced.
        switch(GameManager.gameMode)
        {
            case 0:
                DrawCards(2, 2);
                break;
            case 1:
                DrawCards(2, 3);
                break;
            case 2:
                DrawCards(4, 4);
                break;
            default:
                break;
        }
        UpdateCardStateList();
    }
  
    private void DrawCards(int row, int col)
    {
        cardsSpawnTotal = row * col; //how many cards to draw. n = a*b, we can get duplicate cards spawned from this value.
        int counter = 1;
        int cardTypeIndex = 0;
        int total = availableCardTypes.Count; // int = 4
        RandomCardOrder();

        for (int k = 0; k < row; k++)
        {
            for (int j = 0; j < col; j++)
            {
                if (cardTypeIndex >= cardsSpawnTotal / 2 || cardTypeIndex >= total)
                {
                    cardTypeIndex = 0; //spawn the set of cards again to have duplicates
                    
                }
                GameObject spawnCard = Instantiate(cardPrefab, table.position, Quaternion.identity);

                spawnCard.name = cardPrefab.name + counter; //every spawned card must be unique 
                spawnCard.transform.SetParent(table, false);
                spawnCard.GetComponent<CardManager>().cardType = availableCardTypes[cardTypeIndex]; //randomly spawn a card type.
                spawnedCards.Add(spawnCard);
                counter++;
                cardTypeIndex++;
            }
            
        }
    }
    private void RandomCardOrder()
    {
        //use this method to randomly sort the card deck.
        int count = availableCardTypes.Count;

        for (int k = 0; k < availableCardTypes.Count; k++)
        {
            int randomIndex = Random.Range(k, count);
            CardManager.CARD_TYPE stemp = availableCardTypes[k];

            availableCardTypes[k] = availableCardTypes[randomIndex];
            availableCardTypes[randomIndex] = stemp;
        }
    }
    private void UpdateCardStateList()
    {
        flippedCards.Clear();
        unflippedCards.Clear();
        for (int i = 0; i < spawnedCards.Count; i++)
        {
            CardFlip currentCard = spawnedCards[i].GetComponent<CardFlip>();
            
            if (currentCard.isFlipped)
            {
                flippedCards.Add(currentCard.gameObject);
            }
            else
            {
                unflippedCards.Add(currentCard.gameObject);
            }
        }
    }
    private void RefreshCardTable()
    {
        for (int i = 0; i < spawnedCards.Count; i++)
        {
            CardManager currentCard = spawnedCards[i].GetComponent<CardManager>();
            currentCard.GetComponent<CardManager>().UpdateCard();
        }
        GameManager.comboInfo = GameManager.comboStreak.ToString();
    }
    public void ShuffleDrawCards()
    {
        if (GameManager.points >= 10000)
        {
            for (int i = 0; i < spawnedCards.Count; i++)
            {
                Destroy(spawnedCards[i].gameObject);
            }
            spawnedCards.Clear();
            LayoutFromDifficulty();
            RefreshCardTable();

            GameManager.points -= 1500;
        }
        else
        {
            Debug.Log("Cannot Shuffle");
        }
        
    }
    public void HandleMatching()
    {
        UpdateCardStateList();
        CheckMatch();
        currentTotalFlipped = flippedCards.Count;
    }

    private void CheckMatch()
    {
        int index = 0;
        for (int i = 0; i < flippedCards.Count -1 ; i++) //O(n)^2
        {
                CardManager card1 = flippedCards[i].GetComponent<CardManager>();
                CardManager card2 = flippedCards[index+1].GetComponent<CardManager>();  

                if (card1.cardType == card2.cardType && card1 != card2) //can't compare with self.
                {
                    GameManager.IncreaseMatchCount();
                    StartCoroutine( CardMatchFound(card1.gameObject, card2.gameObject) );
                }
                else if (card1.cardType != card2.cardType)
                {
                    GameManager.CardMismatch();
                    StartCoroutine( NoCardMatchFound(card1.gameObject, card2.gameObject) ) ;
                }
        }
        
    }

    IEnumerator NoCardMatchFound(GameObject card1, GameObject card2)
    {
        soundManager.PlayIncorrectSound();
        yield return new WaitForSeconds(1f);

        card1.GetComponent<CardAnimations>().PlayShakeAnimation();
        card2.GetComponent<CardAnimations>().PlayShakeAnimation();

        card1.GetComponent<CardFlip>().isFlipped = false;
        card2.GetComponent<CardFlip>().isFlipped = false;

        
        RefreshCardTable();
        UpdateCardStateList();

    }
    IEnumerator CardMatchFound(GameObject card1, GameObject card2)
    {
        soundManager.PlayMatchSound();

        //CLEANUP CARD TABLE
        card1.GetComponent<Image>().raycastTarget = false;//disable interaction
        card2.GetComponent<Image>().raycastTarget = false;
        card2.GetComponent<CardAnimations>().PlayMatchedAnimation();
        card1.GetComponent<CardAnimations>().PlayMatchedAnimation();
        yield return new WaitForSeconds(1f);

        card1.GetComponent<CardManager>().cardType = CardManager.CARD_TYPE.BLANK;
        card2.GetComponent<CardManager>().cardType = CardManager.CARD_TYPE.BLANK;

        //UpdateCardStateList();
        RefreshCardTable();
        spawnedCards.Remove(card1);
        spawnedCards.Remove(card2);
        //RefreshCardTable();
        UpdateCardStateList();
    }
}
