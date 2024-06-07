using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI matchesText;
    [SerializeField] private TextMeshProUGUI flipsText;
    [SerializeField] private TextMeshProUGUI combosText;
    [SerializeField] private TextMeshProUGUI combosInfo;
    [SerializeField] private TextMeshProUGUI pointsText;
    private void Start()
    {

        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        matchesText.text = GameManager.matches.ToString();
        flipsText.text = GameManager.totalCardFlips.ToString();
        combosText.text = GameManager.comboStreak.ToString();
        combosInfo.text = GameManager.comboInfo;
        pointsText.text = GameManager.points.ToString();
    }
}
