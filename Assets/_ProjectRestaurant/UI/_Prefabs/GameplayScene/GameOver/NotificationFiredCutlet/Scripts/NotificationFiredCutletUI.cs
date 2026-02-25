using System;
using DG.Tweening;
using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class NotificationFiredCutletUI : MonoBehaviour
{
    [SerializeField] private ButtonManager continueButtton;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform panel;
    
    public event Action OnHidden;
    
    private SoundsServiceGameplay _soundsService;

    [Inject]
    private void ConstructZenject(SoundsServiceGameplay serviceGameplay)
    {
        _soundsService = serviceGameplay;
    }
    
    private void Awake()
    {
        canvasGroup.alpha = 0;
        panel.localScale = Vector3.zero;
        continueButtton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        continueButtton.soundSource = _soundsService.SourceSfx;
        continueButtton.hoverSound = _soundsService.AudioDictionary[AudioNameGamePlay.HoverButton];
        continueButtton.clickSound = _soundsService.AudioDictionary[AudioNameGamePlay.ClickButton];
    }
    
    private void OnDisable()
    {
        continueButtton.onClick.RemoveListener(Hide);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        PlayAnimation();
    }
    
    private void Hide()
    {
        PlayHideAnimation();
    }
    
    private void PlayAnimation()
    {
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

        seq.OnComplete(() =>
        {
            gameObject.SetActive(false);
            OnHidden?.Invoke();
        });
    }
}
