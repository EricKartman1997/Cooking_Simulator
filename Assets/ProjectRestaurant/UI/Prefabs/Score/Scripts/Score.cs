using System;
using UnityEngine;

public class Score : IDisposable
{
    private TimeGame _timeGame;
    private float _score;

    public Score(TimeGame timeGame)
    {
        _timeGame = timeGame;
        
        EventBus.AddScore += AddScore;
        Debug.Log("Создать объект: Score");
    }

    public void Dispose()
    {
        EventBus.AddScore -= AddScore;
        Debug.Log("У объекта вызван Dispose : Score");
    }
    // private void Start()
    // {
    //     _timeGame = GetComponent<TimeGame>();
    // }

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
        var remSeconds = TimeGame.TimeLevel[0] - _timeGame.CurrentSeconds;
        var remMinutes = TimeGame.TimeLevel[1] - _timeGame.CurrentMinutes;
        var multiplyMinutes = remMinutes * 60;
        var result = multiplyMinutes + remSeconds;
        return result;
    }

    // private void OnEnable()
    // {
    //     EventBus.AddScore += AddScore;
    // }

    // private void OnDisable()
    // {
    //     EventBus.AddScore -= AddScore;
    // }
}
