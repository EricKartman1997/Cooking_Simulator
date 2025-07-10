using UnityEngine;

public class Score : MonoBehaviour
{
    private TimeGame _timeGame;
    private float _score;

    private void Start()
    {
        _timeGame = GetComponent<TimeGame>();
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
        var remSeconds = TimeGame.TimeLevel[0] - _timeGame.GetSeconds();
        var remMinutes = TimeGame.TimeLevel[1] - _timeGame.GetMinutes();
        var multiplyMinutes = remMinutes * 60;
        var result = multiplyMinutes + remSeconds;
        return result;
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
