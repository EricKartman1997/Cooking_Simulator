using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : IDisposable
{
    private GameManager _gameManager;
    private FieldsForScriptContainer _fieldsContainer;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    private bool _isInit;
    
    // ...
    private GameObject _windowGame;
    
    //Checks
    private GameObject _content;
    
    //OrdersUI
    private TextMeshProUGUI _scoreText;

    //TimeGame
    private TextMeshProUGUI _timeText;
    
    // GameOver
    private GameObject _windowGameOver;
    private TextMeshProUGUI _scoreNumbersText;
    private TextMeshProUGUI _timeNumbersText;
    private TextMeshProUGUI _assignmentNumbersTimeText;
    private Button _continueButton;

    public bool IsInit => _isInit;
    public GameObject WindowGame => _windowGame;

    public GameObject WindowGameOver => _windowGameOver;

    public GameObject Content => _content;

    public TextMeshProUGUI ScoreText => _scoreText;

    public TextMeshProUGUI TimeText => _timeText;

    public TextMeshProUGUI ScoreNumbersText => _scoreNumbersText;

    public TextMeshProUGUI TimeNumbersText => _timeNumbersText;

    public TextMeshProUGUI AssignmentNumbersTimeText => _assignmentNumbersTimeText;

    public Button ContinueButton => _continueButton;

    public UIManager(CoroutineMonoBehaviour coroutineMonoBehaviour)
    {
        _coroutineMonoBehaviour = coroutineMonoBehaviour;
        
        _coroutineMonoBehaviour.StartCoroutine(Init());
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : UIManager");
    }

    private IEnumerator Init()
    {
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }

        while (_fieldsContainer == null)
        {
            _fieldsContainer = _gameManager.FieldsContainer;
            yield return null;
        }
        
        _windowGame = _fieldsContainer.WindowGame;
        _windowGameOver = _fieldsContainer.WindowGameOver;
        _content = _fieldsContainer.Content;
        _scoreText = _fieldsContainer.ScoreText;
        _timeText = _fieldsContainer.TimeText;
        _scoreNumbersText = _fieldsContainer.ScoreNumbersText;
        _timeNumbersText = _fieldsContainer.TimeNumbersText;
        _assignmentNumbersTimeText = _fieldsContainer.AssignmentNumbersTimeText;
        _continueButton = _fieldsContainer.ContinueButton;
        
        Debug.Log("Создать объект: UIManager");
        _isInit = true;
    }
}
