using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TimeGame timeGame;
    private float _score;
    public void AddScore(int score, float scoreCheck)
    {
        _score += scoreCheck + score + AdditionalScore();
    }
    public void AddScore(int score)
    {
        _score += score + AdditionalScore();
    }

    private float AdditionalScore()
    {
        var remSeconds = TimeGame.TimeLevel[0] - timeGame.GetSeconds();
        var remMinutes = TimeGame.TimeLevel[1] - timeGame.GetMinutes();
        var multiplyMinutes = remMinutes * 60;
        var result = multiplyMinutes + remSeconds;
        return result;
    }

    public float GetScore()
    {
        return _score;
    }

    private void OnEnable()
    {
        EventBus.AddScore += AddScore;
    }

    private void OnDisable()
    {
        EventBus.AddScore -= AddScore;
    }
}
