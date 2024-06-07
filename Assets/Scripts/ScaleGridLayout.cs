using System.Collections;
using System.Collections.Generic;
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

        float cellWidth = rectWidth / columns;
        float cellHeight = rectHeight / rows;

        gridLayout.cellSize = new Vector2(cellWidth/2, cellHeight/1.5f);
    }
}
