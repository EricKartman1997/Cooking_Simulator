using System;
using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class SettingsViewController : MonoBehaviour, ICloseOpenSettingsPanel
{
    private const string OPEN = "Open";
    private const string CLOSE = "Close";

    //public Action AnimFinishAction;
    
    [SerializeField] private ButtonManager buttonBack;
    [SerializeField] private ButtonManager buttonAudio;
    [SerializeField] private ButtonManager buttonGraphic;
    [SerializeField] private ButtonManager buttonControls;
    [SerializeField] private Animator animator;

    private bool _isOpen;
    private bool _isPlayAnim;

    private ISoundsService _soundsService;
        
    public bool IsOpen => _isOpen;


    [Inject]
    private void ConstructZenject(ISoundsService soundsService)
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
        _isOpen = true;
    }
    
    public void ClosePanel()
    {
        if(_isPlayAnim == true)
            return;
        
        animator.SetTrigger(CLOSE);
        _isPlayAnim = true;
        _isOpen = false;
    }
    
    public void FinishAnim()
    {
        _isPlayAnim = false;
    }

    public void FinishCloseAnim()
    {
        animator.Play(CLOSE, 0, 1f);
        animator.Update(0f);
    }
    
    
    
}
