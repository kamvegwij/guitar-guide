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

    public List<GameObject> itemsToHide; //hide these items when calling the game complete screen. manually add these
    public List<GameObject> spawnedCards;
    public List<GameObject> flippedCards;
    public List<GameObject> unflippedCards;

    public int currentTotalFlipped = 0;
    public int cardsSpawnTotal = 0; //use this to scale

    public List<CardManager.CARD_TYPE> availableCardTypes;

    private CardManager.CARD_TYPE getCardType;
    private SoundManager soundManager;

    private int maxIndex;

    private void Start()
    {
        completeScreen.SetActive(false);
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        availableCardTypes = new List<CardManager.CARD_TYPE>() { CardManager.CARD_TYPE.SUN , CardManager.CARD_TYPE.LION, CardManager.CARD_TYPE.CROC, CardManager.CARD_TYPE.TURTLE };
        maxIndex = availableCardTypes.Count;
        LayoutFromDifficulty();
        //UpdateCardStateList();
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
                    itemsToHide[i].gameObject.SetActive(false);
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
            //cardsSpawnTotal = a * b
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

        /*
         * if cardsSpawnTotal == 4
         *      then if cardIndex > 
         */
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

                cardTypeIndex++;
            }
            counter++;
        }
    }
    private void RandomCardOrder()
    {
        //use this method to randomly sort the card deck.
        int count = availableCardTypes.Count;
        //int index = 0;

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
    }
    public void ShuffleDrawCards()
    {
        for (int i = 0; i < spawnedCards.Count; i++)
        {
            Destroy(spawnedCards[i].gameObject);
        }
        spawnedCards.Clear();
        LayoutFromDifficulty();
    }
    public void HandleMatching()
    {
        UpdateCardStateList();
        CheckMatch();
        currentTotalFlipped = flippedCards.Count;
    }

    private void CheckMatch()
    {
        Debug.Log("Hello");
        //if (flippedCards.Count < 2) return; //only start check when 2 or more cards are flipped

        for (int i = 0; i < flippedCards.Count; i++) //O(n)^2
        {
            for (int x = 0; x < flippedCards.Count; x++)
            {
                CardManager card1 = flippedCards[i].GetComponent<CardManager>();
                CardManager card2 = flippedCards[x].GetComponent<CardManager>();  

                if (card1.cardType == card2.cardType && card1 != card2) //can't compare with self.
                {
                    StartCoroutine(RemoveMatchedCards(card1.gameObject, card2.gameObject));
                }
                if (card1.cardType != card2.cardType)
                {
                    
                    StartCoroutine(ResetFlipCards(card1.gameObject, card2.gameObject));
                }
            }
        }
        
    }
    IEnumerator ResetFlipCards(GameObject card1, GameObject card2)
    {
        yield return new WaitForSeconds(1f);
        card1.GetComponent<CardManager>().isFlipped = false;
        card2.GetComponent<CardManager>().isFlipped = false;
        UpdateCardStateList();
        soundManager.PlayIncorrectSound();
        GameManager.totalCardFlips++;
        GameManager.incorrectSelections++;
    }
    IEnumerator RemoveMatchedCards(GameObject card1, GameObject card2)
    {
        //CLEANUP CARD TABLE
        card1.GetComponent<Image>().raycastTarget = false;//disable interaction
        card2.GetComponent<Image>().raycastTarget = false;

        yield return new WaitForSeconds(1f);
        
        Destroy(card1.gameObject);
        Destroy(card2.gameObject);
        spawnedCards.Remove(card1);
        spawnedCards.Remove(card2);
        UpdateCardStateList();
        soundManager.PlayMatchSound();
        GameManager.totalScore++; //increase score
    }
}
