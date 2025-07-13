using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FieldsForScriptContainer : MonoBehaviour
{
    //Checks
    [SerializeField] private GameObject content;
    [SerializeField] private CheckContainer checkContainer;
    
    //OrdersUI
    [SerializeField] private TextMeshProUGUI scoretext;

    //TimeGame
    [SerializeField] private TextMeshProUGUI timeText;
    
    // GameOver
    [SerializeField] private GameObject windowScore;
    [SerializeField] private TextMeshProUGUI scoreNumbersText;
    [SerializeField] private TextMeshProUGUI timeNumbersText;
    [SerializeField] private TextMeshProUGUI assignmentNumbersTimeText;
    [SerializeField] private Button continueButton;
    public GameObject Content => content;

    public TextMeshProUGUI Scoretext => scoretext;
    
    public CheckContainer CheckContainer => checkContainer;
    
    public TextMeshProUGUI TimeText => timeText;

    // GameOver
    public GameObject WindowScore => windowScore;

    public TextMeshProUGUI ScoreNumbersText => scoreNumbersText;

    public TextMeshProUGUI TimeNumbersText => timeNumbersText;

    public TextMeshProUGUI AssignmentNumbersTimeText => assignmentNumbersTimeText;

    public Button ContinueButton => continueButton;
}
