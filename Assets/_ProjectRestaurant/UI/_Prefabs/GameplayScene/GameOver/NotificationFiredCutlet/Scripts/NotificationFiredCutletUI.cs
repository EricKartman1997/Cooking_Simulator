using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class NotificationFiredCutletUI : MonoBehaviour
{
    [SerializeField] private ButtonManager continueButtton;
    private SoundsServiceGameplay _soundsService;

    [Inject]
    private void ConstructZenject(SoundsServiceGameplay serviceGameplay)
    {
        _soundsService = serviceGameplay;
    }

    private void Start()
    {
        continueButtton.soundSource = _soundsService.SourceSfx;
        continueButtton.hoverSound = _soundsService.AudioDictionary[AudioNameGamePlay.HoverButton];
        continueButtton.clickSound = _soundsService.AudioDictionary[AudioNameGamePlay.ClickButton];
    }

    public void Show()
    {
        
    }
    
    public void Hide()
    {
        
    }
}
