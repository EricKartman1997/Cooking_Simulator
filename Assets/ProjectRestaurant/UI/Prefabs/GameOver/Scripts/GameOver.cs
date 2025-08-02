using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : IDisposable
{
    private GameManager _gameManager;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    private UIManager _uiManager;

    private GameObject _windowGameOver;
    private TextMeshProUGUI _scoreNumbersText;
    private TextMeshProUGUI _timeNumbersText;
    private TextMeshProUGUI _assignmentNumbersTimeText;
    private Button _continueButton;
    private bool _isInit;
    
    public bool IsInit => _isInit;
    
    private TimeGame TimeGame => StaticManagerWithoutZenject.GameManager.TimeGame;
    private Score Score => StaticManagerWithoutZenject.GameManager.Score;

    public GameOver(CoroutineMonoBehaviour coroutineMonoBehaviour)
    {

        _coroutineMonoBehaviour = coroutineMonoBehaviour;

        _coroutineMonoBehaviour.StartCoroutine(Init());
        Init();
    }

    public void Dispose()
    {
        EventBus.GameOver -= GameOverMethod;
        Debug.Log("У объекта вызван Dispose : GameOver");
    }
    
    private IEnumerator Init()
    {
        EventBus.GameOver += GameOverMethod;
        
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        
        while (_uiManager == null)
        {
            _uiManager = _gameManager.UIManager;
            yield return null;
        }
        
        _windowGameOver = _uiManager.WindowGameOver;
        _scoreNumbersText = _uiManager.ScoreNumbersText;
        _timeNumbersText = _uiManager.TimeNumbersText;
        _assignmentNumbersTimeText = _uiManager.AssignmentNumbersTimeText;
        _continueButton = _uiManager.ContinueButton;
        
        _windowGameOver.SetActive(false);
        
        Debug.Log("Создать объект: GameOver");
        _isInit = true;
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
        _windowGameOver.SetActive(true);
        _scoreNumbersText.text = $"{Mathf.Round(Score.ScorePlayer)}";
        _timeNumbersText.text = $"{TimeGame.CurrentMinutes:00}:{TimeGame.CurrentSeconds:00}";
        _assignmentNumbersTimeText.text = $"{TimeGame.TimeLevel[1]:00}:{TimeGame.TimeLevel[0]:00}";
        // дописать кнопку
    }
    
}
