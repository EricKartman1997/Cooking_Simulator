using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TimeGame timeGame;
    private float _score;
    void Start()
    {
        
    }
    public void AddScore(int score)
    {
        _score += score + AdditionalScore();
    }

    private float AdditionalScore()
    {
        var remSeconds = Level.TimeLevel[0] - timeGame.GetSeconds();
        var remMinutes = Level.TimeLevel[1] - timeGame.GetMinutes();
        var multiplyMinutes = remMinutes * 60;
        var result = multiplyMinutes + remSeconds;
        return result;
    }

    public float GetScore()
    {
        return _score;
    }
}
