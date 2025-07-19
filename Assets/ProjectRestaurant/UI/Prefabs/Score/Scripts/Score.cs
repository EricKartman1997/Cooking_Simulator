using System;
using System.Collections;
using UnityEngine;

public class Score : IDisposable
{
    private GameManager _gameManager;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    private TimeGame _timeGame;
    private float _score;
    private bool _isInit;
    
    public bool IsInit => _isInit;
    public float ScorePlayer => _score;

    public Score(CoroutineMonoBehaviour coroutineMonoBehaviour)
    {
        _coroutineMonoBehaviour = coroutineMonoBehaviour;
        
        
        _coroutineMonoBehaviour.StartCoroutine(Init());
    }

    public void Dispose()
    {
        EventBus.AddScore -= AddScore;
        Debug.Log("У объекта вызван Dispose : Score");
    }

    private IEnumerator Init()
    {
        EventBus.AddScore += AddScore;
        
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        
        while (_timeGame == null)
        {
            _timeGame = _gameManager.TimeGame;
            yield return null;
        }
        
        Debug.Log("Создать объект: Score");
        _isInit = true;
    }

    public void AddScore(int score)
    {
        _score += score + AdditionalScore();
    }
    
    public float GetScore()
    {
        return _score;
    }
    private void AddScore(int score, float scoreCheck)
    {
        _score += scoreCheck + score + AdditionalScore();
    }
    private float AdditionalScore()
    {
        var remSeconds = _timeGame.TimeLevel[0] - _timeGame.CurrentSeconds;
        var remMinutes = _timeGame.TimeLevel[1] - _timeGame.CurrentMinutes;
        var multiplyMinutes = remMinutes * 60;
        var result = multiplyMinutes + remSeconds;
        return result;
    }

}
