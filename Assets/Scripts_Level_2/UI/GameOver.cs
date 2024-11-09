using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject windowScore;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI assignmentTimeText;
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
        scoreText.text = $"Score: {score.GetScore()}";
        timeText.text = "Time: " + $"{timeGame.GetMinutes():00}:{timeGame.GetSeconds():00}";
        assignmentTimeText.text = "Assignment Time: " + $"{Level.TimeLevel[1]:00}:{Level.TimeLevel[0]:00}";
    }

}
