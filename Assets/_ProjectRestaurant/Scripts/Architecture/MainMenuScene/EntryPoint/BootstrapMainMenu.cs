using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BootstrapMainMenu : MonoBehaviour
{
    public Action ThereIsInternetAction;
    public Action ShowPanelWaitTheInternetAction;
    public Action HidePanelWaitTheInternetAction;

    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private FactoryUIMainMenuScene _factoryUIMainMenuScene;
    private FactoryCamerasMenuScene _factoryCamerasMainMenu;
    private SoundsServiceMainMenu _soundsServiceMainMenu;
    private StorageData _storageData;
    private InternetUpdateService _internetUpdateService;

    private GameObject _mainUIPanel;
    private GameObject _loadingPanel;
    private MenuViewController _menuViewController;

    [Inject]
    private void ConstructZenject(
        LoadReleaseMainMenuScene loadReleaseMainMenuScene,
        FactoryUIMainMenuScene factoryUIMainMenuScene,
        LoadReleaseGlobalScene loadReleaseGlobalScene,
        SoundsServiceMainMenu soundsServiceMainMenu,
        StorageData storageData,
        FactoryCamerasMenuScene factoryCamerasMainMenu,
        InternetUpdateService internetUpdateService)
    {
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
        _factoryUIMainMenuScene = factoryUIMainMenuScene;
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _soundsServiceMainMenu = soundsServiceMainMenu;
        _storageData = storageData;
        _factoryCamerasMainMenu = factoryCamerasMainMenu;
        _internetUpdateService = internetUpdateService;
    }

    private void Start()
    {
        InitializeAsync().Forget();
    }
    
    public async UniTask ExitLevel()
    {
        _soundsServiceMainMenu.StopSounds();
        ShowLoadingPanel();
        await _loadReleaseGlobalScene.LoadSceneAsync("SceneGameplay");
    }

    private void HideLoadingPanel(bool isPlaySounds = true)
    {
        if (_loadingPanel != null)
            _loadingPanel.SetActive(false);
        
        if (isPlaySounds)
            _soundsServiceMainMenu.PlaySounds();
    }

    private void ShowLoadingPanel(bool isStopSounds = true)
    {
        if (_loadingPanel != null)
            _loadingPanel.SetActive(true);
        
        if (isStopSounds)
            _soundsServiceMainMenu.StopSounds();
    }

    private async UniTask InitializeAsync()
    {
        await WaitForResourcesLoaded();
        await CreateCameras();
        CreateLoadingPanel();
        await InitAudioAsync();
        await CreateUI();
        
        // небольшая пауза
        await UniTask.Delay(1200);
        //await UniTask.Delay(1);

        await EnableMusic();
        await StartLevel();
    }
    
    private async UniTask WaitForResourcesLoaded()
    {
        await UniTask.WaitUntil(() => _loadReleaseMainMenuScene.IsLoaded);
    }
    
    private async UniTask CreateCameras()
    {
        _factoryCamerasMainMenu.CreateMainCamera();
        
        await UniTask.Yield();
    }
    
    private void CreateLoadingPanel()
    {
        // Canvas canvas = _factoryUIMainMenuScene.CreateShowLoading();
        // _loadingPanel = canvas.gameObject;
        // Instantiate(_loadReleaseMainMenuScene.GlobalPrefDic[GlobalPref.LoadingPanel], canvas.transform);
        // ShowLoadingPanel();
        
        _loadingPanel = _factoryUIMainMenuScene.CreateShowLoading();
        ShowLoadingPanel();
    }
    
    private async UniTask InitAudioAsync()
    {
        _soundsServiceMainMenu.CreateSounds();
        await UniTask.Yield();

    }
    
    private async UniTask CreateUI()
    {
        _mainUIPanel = _factoryUIMainMenuScene.CreateUI();
        
        await UniTask.Yield();
        
        DescribeTheInternet();
        
        await UniTask.Yield();
    }

    private async UniTask EnableMusic()
    {
        _soundsServiceMainMenu.SetMusic();
        await UniTask.Yield();
    }

    private async UniTask StartLevel()
    {
        _mainUIPanel.SetActive(true);

        if (_storageData.OperatingModeMainMenu ==
            OperatingModeMainMenu.WithoutAnInternetConnection)
        {
            _menuViewController.WarringWindowsViewController.ShowConnectionTheInternet();
            _menuViewController.WarringWindowsViewController.NoConnectionTextButton();
            _menuViewController.TurnOffButtonsGame();
            _internetUpdateService.StartChecking();
        }

        if (_storageData.OperatingModeMainMenu ==
            OperatingModeMainMenu.WithoutAnInternetConnectionButOutdatedData)
        {
            _menuViewController.WarringWindowsViewController.ShowConnectionTheInternet();
            _menuViewController.WarringWindowsViewController.UpdateDateTextButton();
            _internetUpdateService.StartChecking();
        }
        
        await UniTask.Yield();

        HideLoadingPanel();
    }

    private void DescribeTheInternet()
    {
        _menuViewController = _mainUIPanel.GetComponentInChildren<MenuViewController>();

        if (_storageData.OperatingModeMainMenu == OperatingModeMainMenu.WithoutAnInternetConnection || _storageData.OperatingModeMainMenu == OperatingModeMainMenu.WithoutAnInternetConnectionButOutdatedData)
        {
            Debug.Log("Подписался на событие ThereIsInternetAction");
            
            HidePanelWaitTheInternetAction += _menuViewController.WarringWindowsViewController.HideWaitTheInternetConnection;
            HidePanelWaitTheInternetAction += _menuViewController.WarringWindowsViewController.ShowConnectionTheInternet;
            
            ShowPanelWaitTheInternetAction += _menuViewController.WarringWindowsViewController.HideConnectionTheInternet;
            ShowPanelWaitTheInternetAction += _menuViewController.WarringWindowsViewController.ShowWaitTheInternetConnection;
            
            ThereIsInternetAction += _menuViewController.WarringWindowsViewController.HideConnectionTheInternet;
            ThereIsInternetAction += _menuViewController.WarringWindowsViewController.HideWaitTheInternetConnection;
            ThereIsInternetAction += _menuViewController.TurnOnButtonsGame;
            
            //ThereIsInternetAction += _menuViewController.WarringWindowsViewController.HideConnectionTheInternet;
        }
    }
    
}
