using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI matchesText;
    [SerializeField] private TextMeshProUGUI flipsText;

    private void Start()
    {

        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (matchesText != null && flipsText != null) 
        {
            matchesText.text = GameManager.totalScore.ToString();
            flipsText.text = GameManager.totalCardFlips.ToString();
        }
    }
}
