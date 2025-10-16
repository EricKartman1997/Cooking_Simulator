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
    
    [SerializeField] private ModalWindowManager _warringWindow;
    [SerializeField] private SettingsViewController _settingsViewController;
    [SerializeField] private SocialNetViewController _socialNetViewController;

    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private BootstrapMainMenu _bootstrapMainMenu;
    private SoundsServiceMainMenu _soundsService;

    [Inject]
    private void ConstructZenject(LoadReleaseGlobalScene loadReleaseGlobalScene,BootstrapMainMenu bootstrapMainMenu,SoundsServiceMainMenu soundsService)
    {
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _bootstrapMainMenu = bootstrapMainMenu;
        _soundsService = soundsService;
    }
    private void OnEnable()
    {
        buttonSettings.onClick.AddListener(_socialNetViewController.Close);
        buttonSettings.onClick.AddListener(_settingsViewController.OpenPanel);
        
        buttonSocialNetworks.onClick.AddListener(_socialNetViewController.Close);
        buttonSocialNetworks.onClick.AddListener(_socialNetViewController.Open);
        
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
        buttonSettings.onClick.RemoveListener(_settingsViewController.OpenPanel);
        buttonSettings.onClick.AddListener(_socialNetViewController.Close);
        
        buttonSocialNetworks.onClick.RemoveListener(_socialNetViewController.Open);
        buttonSocialNetworks.onClick.RemoveListener(_socialNetViewController.Close);
        
        buttonStart.onClick.RemoveListener(StartOnClick);
        buttonChoiceLevel.onClick.RemoveListener(ChoiceLevelOnClick);
        buttonExit.onClick.RemoveListener(ExitOnClick);
    }
    
    private void StartOnClick()
    {
        _bootstrapMainMenu.ExitLevel();
    }
    
    private void ChoiceLevelOnClick()
    {
        _warringWindow.Open();
    }
    
    private void ExitOnClick()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
    
}
