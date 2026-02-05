using Cysharp.Threading.Tasks;
using DG.Tweening;
using Michsky.MUIP;
using TMPro;
using UnityEngine;
using Zenject;

public class StatisticWindowUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreNumbersText;
    [SerializeField] private TextMeshProUGUI timeNumbersText;
    [SerializeField] private TextMeshProUGUI assignmentNumbersTimeText;
    [SerializeField] private ButtonManager continueButton;
    
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform panel;
    
    private BootstrapGameplay _bootstrapGameplay;
    

    [Inject]
    public void ConstructZenject(BootstrapGameplay bootstrapGameplay)
    {
        _bootstrapGameplay = bootstrapGameplay;
    }
    
    private void Start()
    {
        canvasGroup.alpha = 0;
        panel.localScale = Vector3.zero;
        continueButton.onClick.AddListener(ButtonExit);
    }
    
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.F))
    //     {
    //         Show();
    //     }
    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         Hide();
    //     }
    // }
    
    public void Show(ScoreService scoreService,TimeGameService timeGameService)
    {
        gameObject.SetActive(true);
        UpdateUI(scoreService, timeGameService);
        PlayAnimation();
    }
    
    // public void Show()
    // {
    //     gameObject.SetActive(true);
    //     PlayAnimation();
    // }
    
    public void Hide()
    {
        PlayHideAnimation();
    }
    
    private void PlayAnimation()
    {
        gameObject.SetActive(true);
        Sequence seq = DOTween.Sequence();
        
        seq.Append(canvasGroup.DOFade(1f, 0.7f).SetEase(Ease.OutQuad));
        seq.Join(panel.DOScale(1f, 0.7f).SetEase(Ease.OutBack));
        seq.Play();
    }
    
    private void PlayHideAnimation()
    {
        Sequence seq = DOTween.Sequence();
        
        seq.Append(panel.DOScale(0f, 0.65f).SetEase(Ease.InBack));
        seq.Join(canvasGroup.DOFade(0f, 0.6f).SetEase(Ease.InQuad));
    }
    
    private void UpdateUI(ScoreService scoreService,TimeGameService timeGameService)
    {
        scoreNumbersText.text = $"{Mathf.Round(scoreService.ScorePlayer)}";
        timeNumbersText.text = $"{timeGameService.CurrentMinutes:00}:{timeGameService.CurrentSeconds:00}";
        assignmentNumbersTimeText.text = $"{timeGameService.TimeLevel[1]:00}:{timeGameService.TimeLevel[0]:00}";
    }
    
    private void ButtonExit()
    {
        _bootstrapGameplay.ExitLevel().Forget();
    }
}
