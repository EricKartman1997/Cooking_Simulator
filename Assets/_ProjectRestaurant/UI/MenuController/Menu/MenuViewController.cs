using Michsky.MUIP;
using UnityEngine;

public class MenuViewController : MonoBehaviour
{
    [SerializeField] private ButtonManager buttonStart;
    [SerializeField] private ButtonManager buttonChoiceLevel;
    [SerializeField] private ButtonManager buttonExit;
    [SerializeField] private ButtonManager buttonSettings;
    [SerializeField] private ButtonManager buttonSocialNetworks;
    
    [SerializeField] private ModalWindowManager warringWindow;

    [SerializeField] private SettingsViewController settingsViewController;
    [SerializeField] private SocialNetViewController socialNetViewController;

    private void OnEnable()
    {
        buttonSettings.onClick.AddListener(socialNetViewController.Close);
        buttonSettings.onClick.AddListener(settingsViewController.OpenPanel);
        
        buttonSocialNetworks.onClick.AddListener(socialNetViewController.Close);
        buttonSocialNetworks.onClick.AddListener(socialNetViewController.Open);
        
        buttonStart.onClick.AddListener(ShowOnClick);
    }

    private void OnDisable()
    {
        buttonSettings.onClick.RemoveListener(settingsViewController.OpenPanel);
        buttonSettings.onClick.AddListener(socialNetViewController.Close);
        
        buttonSocialNetworks.onClick.RemoveListener(socialNetViewController.Open);
        buttonSocialNetworks.onClick.RemoveListener(socialNetViewController.Close);
        
        buttonStart.onClick.RemoveListener(ShowOnClick);
    }

    private void Awake()
    {
        
    }
    
    private void ShowOnClick()
    {
        Debug.Log("onClick");
        warringWindow.Open();
    }
    
}
