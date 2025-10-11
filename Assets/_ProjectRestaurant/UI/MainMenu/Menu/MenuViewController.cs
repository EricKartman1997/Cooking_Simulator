using Michsky.MUIP;
using UnityEngine;

public class MenuViewController : MonoBehaviour
{
    [SerializeField] private ButtonManager buttonStart;
    [SerializeField] private ButtonManager buttonChoiceLevel;
    [SerializeField] private ButtonManager buttonExit;
    [SerializeField] private ButtonManager buttonSettings;
    [SerializeField] private ButtonManager buttonSocialNetworks;
    
    [SerializeField] private ModalWindowManager _warringWindow;
    [SerializeField] private SettingsViewController _settingsViewController;
    [SerializeField] private SocialNetViewController _socialNetViewController;
    
    private void OnEnable()
    {
        buttonSettings.onClick.AddListener(_socialNetViewController.Close);
        buttonSettings.onClick.AddListener(_settingsViewController.OpenPanel);
        
        buttonSocialNetworks.onClick.AddListener(_socialNetViewController.Close);
        buttonSocialNetworks.onClick.AddListener(_socialNetViewController.Open);
        
        buttonStart.onClick.AddListener(ShowOnClick);
        //buttonStart.clickSound;
        //buttonStart.hoverSound;
        //buttonStart.soundSource;
    }

    private void OnDisable()
    {
        buttonSettings.onClick.RemoveListener(_settingsViewController.OpenPanel);
        buttonSettings.onClick.AddListener(_socialNetViewController.Close);
        
        buttonSocialNetworks.onClick.RemoveListener(_socialNetViewController.Open);
        buttonSocialNetworks.onClick.RemoveListener(_socialNetViewController.Close);
        
        buttonStart.onClick.RemoveListener(ShowOnClick);
    }
    
    private void ShowOnClick()
    {
        //Debug.Log("onClick");
        _warringWindow.Open();
    }
    
}
