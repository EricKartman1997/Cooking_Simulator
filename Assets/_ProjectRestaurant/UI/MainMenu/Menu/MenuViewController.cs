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

    [Inject]
    private void ConstructZenject(LoadReleaseGlobalScene loadReleaseGlobalScene)
    {
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
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
        // остановить музыку
        // сохранить настройки
        // переход на сцену гемплея
        _loadReleaseGlobalScene.LoadSceneAsync("SceneGameplay");
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
