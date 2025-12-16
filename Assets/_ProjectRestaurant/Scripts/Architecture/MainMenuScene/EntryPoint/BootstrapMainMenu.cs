using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BootstrapMainMenu : MonoBehaviour
{
    //private GameObject _canvas;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private FactoryUIMainMenuScene _factoryUIMainMenuScene;
    private FactoryCamerasMenuScene _factoryCamerasMainMenu;
    private SoundsServiceMainMenu _soundsServiceMainMenu;
    private StorageData _storageData;
    private InternetUpdateService _internetUpdateService;

    private GameObject _mainUIPanel;
    private GameObject _loadingPanel;

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
    
    public void HideLoadingPanel()
    {
        if (_loadingPanel != null)
            _loadingPanel.SetActive(false);
        
        _soundsServiceMainMenu.PlaySounds();
    }
    
    public void ShowLoadingPanel()
    {
        if (_loadingPanel != null)
            _loadingPanel.SetActive(true);
        
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

        await EnableMusic();
        StartLevel();
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
        Canvas canvas = _factoryUIMainMenuScene.CreateShowLoading();
        _loadingPanel = canvas.gameObject;
        Instantiate(_loadReleaseMainMenuScene.GlobalPrefDic[GlobalPref.LoadingPanel], canvas.transform);
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
    }

    private async UniTask EnableMusic()
    {
        _soundsServiceMainMenu.SetMusic();
        await UniTask.Yield();
    }

    private async UniTask StartLevel()
    {
        var menuViewController = _mainUIPanel.GetComponentInChildren<MenuViewController>();

        _mainUIPanel.SetActive(true);

        if (_storageData.OperatingModeMainMenu ==
            OperatingModeMainMenu.WithoutAnInternetConnection)
        {
            menuViewController.WarringWindowsViewController.ShowConnectionTheInternet();
            menuViewController.WarringWindowsViewController.NoConnectionTextButton();
            menuViewController.TurnOffButtonsGame();
            _internetUpdateService.StartChecking();
        }

        if (_storageData.OperatingModeMainMenu ==
            OperatingModeMainMenu.WithoutAnInternetConnectionButOutdatedData)
        {
            menuViewController.WarringWindowsViewController.ShowConnectionTheInternet();
            menuViewController.WarringWindowsViewController.UpdateDateTextButton();
            _internetUpdateService.StartChecking();
        }

        HideLoadingPanel();
    }
    
}
