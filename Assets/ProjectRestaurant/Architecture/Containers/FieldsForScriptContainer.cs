using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FieldsForScriptContainer : MonoBehaviour
{
    //Checks
    [SerializeField] private GameObject content;
    
    //OrdersUI
    [SerializeField] private TextMeshProUGUI scoreText; //OrderText

    //TimeGame
    [SerializeField] private TextMeshProUGUI timeText;
    
    // GameOver
    [SerializeField] private TextMeshProUGUI scoreNumbersText;
    [SerializeField] private TextMeshProUGUI timeNumbersText;
    [SerializeField] private TextMeshProUGUI assignmentNumbersTimeText;
    [SerializeField] private Button continueButton;
    
    // UIManager
    [SerializeField] private GameObject windowGameOver;
    [SerializeField] private GameObject windowGame;
    
    
    public GameObject Content => content;
    public TextMeshProUGUI ScoreText => scoreText;
    public TextMeshProUGUI TimeText => timeText;
    public TextMeshProUGUI ScoreNumbersText => scoreNumbersText;
    public TextMeshProUGUI TimeNumbersText => timeNumbersText;
    public TextMeshProUGUI AssignmentNumbersTimeText => assignmentNumbersTimeText;
    public Button ContinueButton => continueButton;
    public GameObject WindowGameOver => windowGameOver;
    public GameObject WindowGame => windowGame;
}
