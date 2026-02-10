using DG.Tweening;
using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class EndDialogueUI : MonoBehaviour
{
    [SerializeField] private ButtonManager buttonMenu;
    [SerializeField] private ButtonManager buttonGameplayLevel;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform panel;
    private SoundsServiceGameplay _soundsService;

    [Inject]
    private void ConstructZenject(SoundsServiceGameplay serviceGameplay)
    {
        _soundsService = serviceGameplay;
    }
    
    private void Awake()
    {
        // Перед анимацией прячем объект
        canvasGroup.alpha = 0;
        panel.localScale = Vector3.zero;
        buttonMenu.onClick.AddListener(TransitionToMenu);
        buttonGameplayLevel.onClick.AddListener(TransitionToGameplayLevel);
    }
    private void Start()
    {
        buttonMenu.soundSource = _soundsService.SourceSfx;
        buttonMenu.hoverSound = _soundsService.AudioDictionary[AudioNameGamePlay.HoverButton];
        buttonMenu.clickSound = _soundsService.AudioDictionary[AudioNameGamePlay.ClickButton];
        
        buttonGameplayLevel.soundSource = _soundsService.SourceSfx;
        buttonGameplayLevel.hoverSound = _soundsService.AudioDictionary[AudioNameGamePlay.HoverButton];
        buttonGameplayLevel.clickSound = _soundsService.AudioDictionary[AudioNameGamePlay.ClickButton];
    }
    
    private void OnDisable()
    {
        buttonMenu.onClick.RemoveListener(TransitionToMenu);
        buttonGameplayLevel.onClick.RemoveListener(TransitionToGameplayLevel);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
        PlayShowAnimation();
    }
    
    public void Hide()
    {
        PlayHideAnimation();
    }
    
    private void TransitionToMenu()
    {
        
    }
    
    private void TransitionToGameplayLevel()
    {
        
    }

    private void PlayShowAnimation()
    {
        Sequence seq = DOTween.Sequence();

        // 1. Плавное появление + увеличение
        seq.Append(canvasGroup.DOFade(1f, 0.7f).SetEase(Ease.OutQuad));
        seq.Join(panel.DOScale(1f, 0.7f).SetEase(Ease.OutBack));

        seq.Play();
    }
    
    private void PlayHideAnimation()
    {
        Sequence seq = DOTween.Sequence();

        // 1. Мини-вибрация перед исчезновением (приятный акцент)
        //seq.Append(panel.DOShakeAnchorPos(0.25f, new Vector2(6f, 4f), 10, 90, false, true));

        // 2. Масштабирование вниз + затем исчезновение
        seq.Append(panel.DOScale(0f, 0.65f).SetEase(Ease.InBack));
        seq.Join(canvasGroup.DOFade(0f, 0.6f).SetEase(Ease.InQuad));

        seq.OnComplete(() =>
        {
            gameObject.SetActive(false);
            //OnHidden?.Invoke();   // 🔥 уведомляем, что закрыли
        });
    }
}
