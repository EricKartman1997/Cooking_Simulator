using System;
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
    
    public event Action OnShown;
    
    private IExitLevel _bootstrapGameplay;
    private SoundsServiceGameplay _soundsService;
    

    [Inject]
    public void ConstructZenject(IExitLevel bootstrapGameplay,SoundsServiceGameplay soundsServiceGameplay)
    {
        _bootstrapGameplay = bootstrapGameplay;
        _soundsService = soundsServiceGameplay;
    }

    private void Awake()
    {
        canvasGroup.alpha = 0;
        panel.localScale = Vector3.zero;
        continueButton.onClick.AddListener(ButtonExit);
    }

    private void Start()
    {
        continueButton.enableButtonSounds = true;
        continueButton.soundSource = _soundsService.SourceSfx;
        continueButton.hoverSound = _soundsService.AudioDictionary[AudioNameGamePlay.HoverButton];
        continueButton.clickSound = _soundsService.AudioDictionary[AudioNameGamePlay.ClickButton];
    }
    
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
        
        seq.OnComplete(() => OnShown?.Invoke());
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
        _bootstrapGameplay.ExitInMenuLevel().Forget();
    }
}
