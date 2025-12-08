using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class SettingsUI : MonoBehaviour
{
    private const string OPEN = "Open";
    private const string CLOSE = "Close";
    
    [SerializeField] private ButtonManager buttonBack;
    [SerializeField] private ButtonManager buttonAudio;
    [SerializeField] private ButtonManager buttonGraphic;
    [SerializeField] private ButtonManager buttonControls;
    [SerializeField] private Animator animator;
    
    private bool _isPlayAnim;

    private SoundsServiceGameplay _soundsService;

    [Inject]
    private void ConstructZenject(SoundsServiceGameplay soundsService)
    {
        _soundsService = soundsService;
    }
    private void OnEnable()
    {
        buttonBack.onClick.AddListener(ClosePanel);
        
        buttonBack.soundSource = _soundsService.SourceSfx;
        buttonAudio.soundSource = _soundsService.SourceSfx;
        buttonGraphic.soundSource = _soundsService.SourceSfx;
        buttonControls.soundSource = _soundsService.SourceSfx;
    }

    private void OnDisable()
    {
        buttonBack.onClick.RemoveListener(ClosePanel);
    }

    public void OpenPanel()
    {
        if(_isPlayAnim == true)
            return;
        
        animator.SetTrigger(OPEN);
        _isPlayAnim = true;
    }
    
    public void ClosePanel()
    {
        if(_isPlayAnim == true)
            return;
        
        animator.SetTrigger(CLOSE);
        _isPlayAnim = true;
    }
    
    public void FinishAnim()
    {
        _isPlayAnim = false;
    }
    
}