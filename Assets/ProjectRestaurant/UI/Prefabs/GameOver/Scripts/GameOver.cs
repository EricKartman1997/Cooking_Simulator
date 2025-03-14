using System;
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

    private void ShowWindowScore()
    {
        windowScore.SetActive(true);
        scoreNumbersText.text = $"{Mathf.Round(score.GetScore())}";
        timeNumbersText.text = $"{timeGame.GetMinutes():00}:{timeGame.GetSeconds():00}";
        assignmentNumbersTimeText.text = $"{TimeGame.TimeLevel[1]:00}:{TimeGame.TimeLevel[0]:00}";
        // дописать кнопку
    }

    private void GameOverMethod()
    {
        Debug.Log("Игра закончена, время больше не идет");
        //Destroy(SecondCheck);
        //Destroy(FirstCheck);
        //Destroy(ThirdCheck);
        //_check.DeleteAllChecks(); или через шину событий
        ShowWindowScore();
            
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    private void OnEnable()
    {
        EventBus.GameOver += GameOverMethod;
    }

    private void OnDisable()
    {
        EventBus.GameOver -= GameOverMethod;
    }
}
