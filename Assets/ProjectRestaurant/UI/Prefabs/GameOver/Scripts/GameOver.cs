using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : IDisposable
{
    private GameObject _windowScore;
    private TextMeshProUGUI _scoreNumbersText;
    private TextMeshProUGUI _timeNumbersText;
    private TextMeshProUGUI _assignmentNumbersTimeText;
    private Button _continueButton;
    
    private TimeGame TimeGame => StaticManagerWithoutZenject.GameManager.TimeGame;
    private Score Score => StaticManagerWithoutZenject.GameManager.Score;

    public GameOver(GameObject windowScore, TextMeshProUGUI scoreNumbersText, TextMeshProUGUI timeNumbersText, TextMeshProUGUI assignmentNumbersTimeText, Button continueButton)
    {
        _windowScore = windowScore;
        _scoreNumbersText = scoreNumbersText;
        _timeNumbersText = timeNumbersText;
        _assignmentNumbersTimeText = assignmentNumbersTimeText;
        _continueButton = continueButton;

        Init();
        Debug.Log("Создать объект: GameOver");
    }

    public void Dispose()
    {
        EventBus.GameOver -= GameOverMethod;
        Debug.Log("У объекта вызван Dispose : GameOver");
    }
    private void Init()
    {
        EventBus.GameOver += GameOverMethod;
        _windowScore.SetActive(false);
    }
    
    private void GameOverMethod()
    {
        Debug.Log("Игра закончена, время больше не идет");
        ShowWindowScore();
            
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
    
    private void ShowWindowScore()
    {
        _windowScore.SetActive(true);
        _scoreNumbersText.text = $"{Mathf.Round(Score.GetScore())}";
        _timeNumbersText.text = $"{TimeGame.CurrentMinutes:00}:{TimeGame.CurrentSeconds:00}";
        _assignmentNumbersTimeText.text = $"{TimeGame.TimeLevel[1]:00}:{TimeGame.TimeLevel[0]:00}";
        // дописать кнопку
    }
    
}
