using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardTable : MonoBehaviour
{
    public CardManager playCard;
    public GameObject cardPrefab;
    public GameObject completeScreen;
    public Transform table; //current table with grid layout

    public List<GameObject> itemsToHide; //hide these items when calling the game complete screen. manually add these
    public List<GameObject> spawnedCards;
    public List<GameObject> flippedCards;
    public List<GameObject> unflippedCards;

    public int currentTotalFlipped = 0;

    private List<CardManager.CARD_TYPE> availableCardTypes;
    private CardManager.CARD_TYPE getCardType;

    private GameManager gameManager;

    private void Start()
    {
        completeScreen.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
       // getCardType = GetComponent<CardManager.CARD_TYPE>();

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
                    itemsToHide[i].gameObject.SetActive(false);
                }
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

                int randomCardType = Random.Range(0, availableCardTypes.Count);
                
                spawnCard.GetComponent<CardManager>().cardType = availableCardTypes[randomCardType]; //randomly spawn a card type.

                spawnedCards.Add(spawnCard);

                counter++;
            }
        }
        
    }

    public void ShuffleDrawCards()
    {
        for (int i = 0; i < spawnedCards.Count; i++)
        {
            Destroy(spawnedCards[i].gameObject);
        }
        spawnedCards.Clear();//clean up before re drawing
        flippedCards.Clear();
        unflippedCards.Clear();
        LayoutFromDifficulty();
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
        CheckMatch();
    }
    private void CheckMatch()
    {
        for (int i = 0; i < flippedCards.Count; i++) //O(n)^2
        {
            for (int x = 1; x < flippedCards.Count; x++)
            {
                CardManager currentCard = flippedCards[i].GetComponent<CardManager>();
                CardManager compareCard = flippedCards[x].GetComponent<CardManager>();  

                if (currentCard.cardType == compareCard.cardType && currentCard != compareCard) //can't compare with self.
                {

                    StartCoroutine(RemoveMatchedCards(currentCard.gameObject, compareCard.gameObject));
                }
            }
        }
    }

    IEnumerator RemoveMatchedCards(GameObject card1, GameObject card2)
    {
        //CLEANUP CARD TABLE
        card1.GetComponent<Image>().raycastTarget = false;//disable interaction
        card2.GetComponent<Image>().raycastTarget = false;

        yield return new WaitForSeconds(1f);

        gameManager.totalScore++; //increase score
        Destroy(card1.gameObject);
        Destroy(card2.gameObject);
        spawnedCards.Remove(card1);
        spawnedCards.Remove(card2);
        flippedCards.Remove(card1);
        flippedCards.Remove(card2);
    }
}
