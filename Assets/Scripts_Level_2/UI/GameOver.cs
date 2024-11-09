using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject windowScore;
    [SerializeField] private TextMeshProUGUI scoreNumbersText;
    [SerializeField] private TextMeshProUGUI timeNumbersText;
    [SerializeField] private TextMeshProUGUI assignmentNumbersTimeText;
    [SerializeField] private Button continueButton;
    
    [SerializeField] private Score score;
    [SerializeField] private TimeGame timeGame;

    void Start()
    {
        windowScore.SetActive(false);
    }

    public void ShowWindowScore()
    {
        windowScore.SetActive(true);
        scoreNumbersText.text = $"{Mathf.Round(score.GetScore())}";
        timeNumbersText.text = $"{timeGame.GetMinutes():00}:{timeGame.GetSeconds():00}";
        assignmentNumbersTimeText.text = $"{Level.TimeLevel[1]:00}:{Level.TimeLevel[0]:00}";
    }

}
