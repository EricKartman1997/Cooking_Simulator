using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreNumbersText;
    [SerializeField] private TextMeshProUGUI timeNumbersText;
    [SerializeField] private TextMeshProUGUI assignmentNumbersTimeText;
    [SerializeField] private Button continueButton;
    
    private BootstrapGameplay _bootstrapGameplay;
    

    [Inject]
    public void ConstructZenject(BootstrapGameplay bootstrapGameplay)
    {
        _bootstrapGameplay = bootstrapGameplay;
    }
    
    private void Start()
    {
        continueButton.onClick.AddListener(ButtonExit);
    }
    
    public void Show(ScoreService scoreService,TimeGameService timeGameService)
    {
        gameObject.SetActive(true);
        scoreNumbersText.text = $"{Mathf.Round(scoreService.ScorePlayer)}";
        timeNumbersText.text = $"{timeGameService.CurrentMinutes:00}:{timeGameService.CurrentSeconds:00}";
        assignmentNumbersTimeText.text = $"{timeGameService.TimeLevel[1]:00}:{timeGameService.TimeLevel[0]:00}";
    }
    
    private void ButtonExit()
    {
        _bootstrapGameplay.ExitLevel().Forget();
    }
}
