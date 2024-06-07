using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScaleGridLayout : MonoBehaviour
{
    public GridLayoutGroup gridLayout;
    public RectTransform rectTransform;

    private int cardsSpawned;
    private float rectWidth;
    private float rectHeight;

    private void Awake()
    {
        gridLayout= GetComponent<GridLayoutGroup>();
        rectTransform= GetComponent<RectTransform>();
    }

    private void Start()
    {
        DynamicCellSize();
    }

    private void DynamicCellSize()
    {
        cardsSpawned = gridLayout.transform.childCount; //resize cards based on how many cards are initially placed on the deck.

        rectWidth = rectTransform.rect.width;
        rectHeight = rectTransform.rect.height;

        int columns = Mathf.CeilToInt(Mathf.Sqrt(cardsSpawned));
        int rows = Mathf.CeilToInt((float)cardsSpawned / columns);

        float cardX = rectWidth / columns;
        float cardY = rectHeight / rows;

        gridLayout.cellSize = new Vector2(cardX / 2, cardY / 1.5f);
    }
}
