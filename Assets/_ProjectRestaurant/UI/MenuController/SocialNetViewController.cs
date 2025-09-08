using System;
using Michsky.MUIP;
using UnityEngine;

public class SocialNetViewController : MonoBehaviour
{
    private const string OPEN = "Open";
    private const string CLOSE = "Close";

    [SerializeField] private ButtonManager buttonGitHub;
    [SerializeField] private ButtonManager buttonEmail;
    [SerializeField] private ButtonManager buttonTelegram;
    [SerializeField] private ButtonManager buttonDiscord;
    
    [SerializeField] private Animator animator;

    private bool _isOpenPanel;
    private bool _isPlayAnim;
    private void OnEnable()
    {
        //buttonSocialNet.onClick.AddListener(OpenClosePanel);

    }

    private void OnDisable()
    {
        //buttonSocialNet.onClick.RemoveListener(OpenClosePanel);
    }

    private void Awake()
    {
        buttonGitHub.Interactable(true);
        buttonEmail.Interactable(true);
        buttonTelegram.Interactable(true);
        buttonDiscord.Interactable(false);
    }

    public void Open()
    {
        if (_isOpenPanel == false)
        {
            if(_isPlayAnim == true)
                return;
            
            animator.SetTrigger(OPEN);
            _isOpenPanel = true;
            _isPlayAnim = true;
        }
    }
    
    public void Close()
    {
        if(_isOpenPanel == true)
        {
            if(_isPlayAnim == true)
                return;
                
            animator.SetTrigger(CLOSE);
            _isOpenPanel = false;
            _isPlayAnim = true;
        }
    }

    public void FinishAnim()
    {
        _isPlayAnim = false;
    }
    
}
