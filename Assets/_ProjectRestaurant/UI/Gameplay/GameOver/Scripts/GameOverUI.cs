using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreNumbersText;
    [SerializeField] private TextMeshProUGUI timeNumbersText;
    [SerializeField] private TextMeshProUGUI assignmentNumbersTimeText;
    [SerializeField] private Button continueButton;
    
    public void Show(Score score,TimeGame timeGame)
    {
        gameObject.SetActive(true);
        scoreNumbersText.text = $"{Mathf.Round(score.ScorePlayer)}";
        timeNumbersText.text = $"{timeGame.CurrentMinutes:00}:{timeGame.CurrentSeconds:00}";
        assignmentNumbersTimeText.text = $"{timeGame.TimeLevel[1]:00}:{timeGame.TimeLevel[0]:00}";
        // дописать кнопку
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
