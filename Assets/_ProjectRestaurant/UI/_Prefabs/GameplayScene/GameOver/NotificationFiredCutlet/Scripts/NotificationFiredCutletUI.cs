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
        // Перед анимацией прячем объект
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

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.F))
    //     {
    //         Show();
    //     }
    //     // if (Input.GetKeyDown(KeyCode.C))
    //     // {
    //     //     Hide();
    //     // }
    // }

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

        // 1. Плавное появление + увеличение
        seq.Append(canvasGroup.DOFade(1f, 0.7f).SetEase(Ease.OutQuad));
        seq.Join(panel.DOScale(1f, 0.7f).SetEase(Ease.OutBack));

        // 2. Лёгкий "ударный" эффект
        //seq.Append(panel.DOPunchScale(new Vector3(0.15f, 0.15f, 0f), 0.5f, 10, 1));

        // 3. Лёгкое колебание позиции (живой эффект)
        //seq.Join(panel.DOShakeAnchorPos(0.4f, new Vector2(10f, 8f), 10, 90, false, true));

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
            OnHidden?.Invoke();   // 🔥 уведомляем, что закрыли
        });
    }
}
