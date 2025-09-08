using System;
using Michsky.MUIP;
using UnityEngine;

public class SocialNetViewController : MonoBehaviour
{
    private const string OPEN = "Open";
    private const string CLOSE = "Close";
    
    private const string TELEGRAMURL = "https://t.me/E_R_I_C_CARTMAN";
    private const string DISCORDURL = "https://discord.gg/R56NSYPUaJ";
    private const string EMAILADDRESS = "your.erickartmangamedev@gmail.com";
    private const string GITHUBURL = "https://github.com/EricKartman1997";

    [SerializeField] private ButtonManager buttonGitHub;
    [SerializeField] private ButtonManager buttonEmail;
    [SerializeField] private ButtonManager buttonTelegram;
    [SerializeField] private ButtonManager buttonDiscord;
    
    [SerializeField] private Animator animator;

    private bool _isOpenPanel;
    private bool _isPlayAnim;
    
    private void OnEnable()
    {
        buttonGitHub.onClick.AddListener(FollowGitHub);
        buttonEmail.onClick.AddListener(FollowEmail);
        buttonTelegram.onClick.AddListener(FollowTelegram);
        buttonDiscord.onClick.AddListener(FollowDiscord);
    }

    private void OnDisable()
    {
        buttonGitHub.onClick.RemoveListener(FollowGitHub);
        buttonEmail.onClick.RemoveListener(FollowEmail);
        buttonTelegram.onClick.RemoveListener(FollowTelegram);
        buttonDiscord.onClick.RemoveListener(FollowDiscord);
    }

    private void Awake()
    {
        buttonGitHub.Interactable(true);
        buttonEmail.Interactable(false);
        buttonTelegram.Interactable(true);
        buttonDiscord.Interactable(true);
    }

    private void FollowTelegram()
    {
        Application.OpenURL(TELEGRAMURL);
    }
    
    private void FollowDiscord()
    {
        Application.OpenURL(DISCORDURL);
    }
    
    private void FollowEmail()
    {
        string subject = "Feedback about Your Game";
        
        Application.OpenURL($"mailto:{EMAILADDRESS}?subject={Uri.EscapeDataString(subject)}");
        //Application.OpenURL(EMAILADDRESS);
    }
    
    private void FollowGitHub()
    {
        Application.OpenURL(GITHUBURL);
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
