using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class MenuViewController : MonoBehaviour
{
    [SerializeField] private ButtonManager buttonStart;
    [SerializeField] private ButtonManager buttonChoiceLevel;
    [SerializeField] private ButtonManager buttonExit;
    [SerializeField] private ButtonManager buttonSettings;
    [SerializeField] private ButtonManager buttonSocialNetworks;

    [SerializeField] private WarringWindowsViewController warringWindowsViewController;
    [SerializeField] private SettingsViewController settingsViewController;
    [SerializeField] private SocialNetViewController socialNetViewController;
    
    private BootstrapMainMenu _bootstrapMainMenu;
    private SoundsServiceMainMenu _soundsService;
    private FactoryUIMainMenuScene _factoryUIMainMenuScene;

    private NotificationTrainingUI _notificationTraining;

    public WarringWindowsViewController WarringWindowsViewController => warringWindowsViewController;

    [Inject]
    private void ConstructZenject(BootstrapMainMenu bootstrapMainMenu,
        SoundsServiceMainMenu soundsService,FactoryUIMainMenuScene factoryUIMainMenuScene)
    {
        _bootstrapMainMenu = bootstrapMainMenu;
        _soundsService = soundsService;
        _factoryUIMainMenuScene = factoryUIMainMenuScene;
    }
    private void OnEnable()
    {
        buttonSettings.onClick.AddListener(OnClickButtonSettings);
        
        buttonSocialNetworks.onClick.AddListener(OnClickButtonSocialNetworks);
        
        buttonStart.onClick.AddListener(StartOnClick);
        buttonChoiceLevel.onClick.AddListener(ChoiceLevelOnClick);
        buttonExit.onClick.AddListener(ExitOnClick);
        
        buttonSettings.soundSource = _soundsService.SourceSfx;
        buttonSocialNetworks.soundSource = _soundsService.SourceSfx;
        buttonStart.soundSource = _soundsService.SourceSfx;
        buttonChoiceLevel.soundSource = _soundsService.SourceSfx;
        buttonExit.soundSource = _soundsService.SourceSfx;
        
        //buttonStart.clickSound;
        //buttonStart.hoverSound;
        //buttonStart.soundSource;
    }

    private void OnDisable()
    {
        buttonSettings.onClick.RemoveListener(OnClickButtonSettings);
        
        buttonSocialNetworks.onClick.RemoveListener(OnClickButtonSocialNetworks);
        
        buttonStart.onClick.RemoveListener(StartOnClick);
        buttonChoiceLevel.onClick.RemoveListener(ChoiceLevelOnClick);
        buttonExit.onClick.RemoveListener(ExitOnClick);
    }
    
    public void Init()
    {
        _notificationTraining = _factoryUIMainMenuScene.NotificationTraining;
    }

    public void TurnOffButtonsGame()
    {
        buttonStart.isInteractable = false;
        buttonChoiceLevel.isInteractable = false;
    }
    
    public void TurnOnButtonsGame()
    {
        buttonStart.isInteractable = true;
        buttonChoiceLevel.isInteractable = true;
    }
    
    private void StartOnClick()
    {
        _notificationTraining.Show();
    }
    
    private void ChoiceLevelOnClick()
    {
        warringWindowsViewController.WarringWindow.Open();
        warringWindowsViewController.WarringWindow.windowTitle.fontSize = 30;
    }
    
    private void ExitOnClick()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }

    private void OnClickButtonSettings()
    {
        socialNetViewController.Close();
        settingsViewController.OpenPanel();
        //warringWindowsViewController.HideConnectionTheInternet();
    }
    
    private void OnClickButtonSocialNetworks()
    {
        socialNetViewController.Close();
        socialNetViewController.Open();
    }
    
}
