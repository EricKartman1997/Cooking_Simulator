using DG.Tweening;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NotificationTrainingUI : MonoBehaviour
{
    [SerializeField] private ButtonManager agreeButton;
    [SerializeField] private ButtonManager disagreeButton;
    [SerializeField] private Button anticlicker;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform panel;
    
    private SoundsServiceMainMenu _soundsService;
    private BootstrapMainMenu _bootstrapMainMenu;

    private bool _isAnimPlaying;
    
    [Inject]
    private void ConstructZenject(SoundsServiceMainMenu soundService,BootstrapMainMenu bootstrapMainMenu)
    {
        _soundsService = soundService;
        _bootstrapMainMenu = bootstrapMainMenu;
    }
    
    // private void Awake()
    // {
    //     agreeButton.onClick.AddListener(LoadTrainLevel);
    //     disagreeButton.onClick.AddListener(LoadDemoLevel);
    //     anticlicker.onClick.AddListener(Hide);
    // }

    private void OnDestroy()
    {
        agreeButton.onClick.RemoveListener(LoadTrainLevel);
        disagreeButton.onClick.RemoveListener(LoadDemoLevel);
        anticlicker.onClick.RemoveListener(Hide);
    }
    
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.F))
    //     {
    //         Debug.Log("F");
    //         Show();
    //     }
    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         Debug.Log("C");
    //         Hide();
    //     }
    // }

    private void Start()
    {
        agreeButton.onClick.AddListener(LoadTrainLevel);
        disagreeButton.onClick.AddListener(LoadDemoLevel);
        anticlicker.onClick.AddListener(Hide);
        agreeButton.enableButtonSounds = true;
        agreeButton.soundSource = _soundsService.SourceSfx;
        agreeButton.hoverSound = _soundsService.AudioDictionary[AudioNameMainMenu.HoverButton];
        agreeButton.clickSound = _soundsService.AudioDictionary[AudioNameMainMenu.ClickButton];
        
        disagreeButton.enableButtonSounds = true;
        disagreeButton.soundSource = _soundsService.SourceSfx;
        disagreeButton.hoverSound = _soundsService.AudioDictionary[AudioNameMainMenu.HoverButton];
        disagreeButton.clickSound = _soundsService.AudioDictionary[AudioNameMainMenu.ClickButton];
    }
    
    public void Show()
    {
        _isAnimPlaying = false;
        gameObject.SetActive(true);
        PlayAnimation();
        //Debug.Log("Show");
    }
    
    private void Hide()
    {
        if (_isAnimPlaying == false)
        {
            _isAnimPlaying = true;
            PlayHideAnimation();
        }
        
    }
    
    private void PlayAnimation()
    {
        // Центр canvas
        Vector2 center = Vector2.zero;

        // Начальная позиция - над экраном
        float startY = Screen.height * 0.7f;

        panel.anchoredPosition = new Vector2(0, startY);

        Sequence seq = DOTween.Sequence();
        
        seq.Append(panel.DOAnchorPos(center, 0.6f).SetEase(Ease.OutBack));
        seq.Join(canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.InQuad));
        
        seq.Play();
    }
    
    private void PlayHideAnimation()
    {
        // Конечная позиция — за экраном снизу
        float endY = -Screen.height * 0.7f;

        Sequence seq = DOTween.Sequence();
        seq.Append(panel.DOAnchorPos(new Vector2(0, endY), 0.55f).SetEase(Ease.InBack));
        seq.Join(canvasGroup.DOFade(0f, 0.6f).SetEase(Ease.OutQuad));
        
        seq.OnComplete(() =>
        {
            _isAnimPlaying = true;
            gameObject.SetActive(false);
        });
    }

    private async void LoadTrainLevel()
    {
        await _bootstrapMainMenu.LoadTrainingLevel();
        //gameObject.SetActive(false);
        Debug.Log("зашел LoadTrainLevel");
    }
    
    private async void LoadDemoLevel()
    {
        await _bootstrapMainMenu.LoadGameplayLevel();
        //gameObject.SetActive(false);
        Debug.Log("зашел LoadDemoLevel");
    }
}
