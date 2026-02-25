using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BootstrapTraining : MonoBehaviour, IExitLevel
{
    public event Action InitMenuButtons;
    
    private CinemachineVirtualCamera _virtualCamera;
    
    private LoadReleaseGameplay _loadReleaseGameplay;
    private LoadReleaseTraining _loadReleaseTraining;
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private FactoryUIGameplay _factoryUIGameplay;
    private FactoryUITraining _factoryUITraining;
    private FactoryPlayerGameplay _factoryPlayerGameplay;
    private FactoryEnvironment _factoryEnvironment;
    private FactoryEnvironmentTraining _factoryEnvironmentTraining;
    private FactoryCamerasGameplay _factoryCamerasGameplay;
    private SoundsServiceGameplay _soundsServiceGameplay;
    private StorageData _storageData;
    private NotificationManager _notificationManager;
    private OrdersService _ordersService;
    private ChecksManager _checksManager;
    private GameOverService _gameOverService;
    private DialogueManager _dialogueManager;
    
    private DiContainer _container;
    
    private UpdateChecks _updateChecks;
    private TimeGameService _timeGameService;
    
    private GameObject _loadingPanel;
    private GameObject _gameUI;
    
    [Inject]
    public void ConstructZenject(LoadReleaseGameplay loadRelease, LoadReleaseGlobalScene loadReleaseGlobalScene,
        FactoryUIGameplay factoryUIGameplay, FactoryPlayerGameplay factoryPlayerGameplay,
        FactoryEnvironment factoryEnvironment, FactoryCamerasGameplay factoryCamerasGameplay,
        SoundsServiceGameplay soundsServiceGameplay, TimeGameService timeGameService,
        UpdateChecks updateChecks,DiContainer container,StorageData storageData,NotificationManager notificationManager,
        OrdersService ordersService,ChecksManager checksManager,GameOverService gameOverService, DialogueManager dialogueManager,
        FactoryEnvironmentTraining factoryEnvironmentTraining,LoadReleaseTraining loadReleaseTraining,FactoryUITraining factoryUITraining)
    {
        _loadReleaseGameplay = loadRelease;
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _loadReleaseTraining = loadReleaseTraining;
        _factoryUIGameplay = factoryUIGameplay;
        _factoryPlayerGameplay = factoryPlayerGameplay;
        _factoryEnvironment = factoryEnvironment;
        _factoryCamerasGameplay = factoryCamerasGameplay;
        _soundsServiceGameplay = soundsServiceGameplay;
        _timeGameService = timeGameService;
        _updateChecks = updateChecks;
        _container = container;
        _storageData = storageData;
        _notificationManager = notificationManager;
        _ordersService = ordersService;
        _checksManager = checksManager;
        _gameOverService = gameOverService;
        _dialogueManager = dialogueManager;
        _factoryEnvironmentTraining = factoryEnvironmentTraining;
        _factoryUITraining = factoryUITraining;
    }
    
    private void Start()
    {
        InitializeAsync().Forget();
        _storageData.ThereIsInternetConnection();
    }
    
    private async UniTaskVoid InitializeAsync()
    {
        await WaitForResourcesLoaded();
        await InitAudioAsync();
        CreateCameras();
        await CreateLoadingPanel();
        ShowLoadingPanel();
        await CreateUI();
        await CreateEnvironmentAsync();
        await CreatePlayerAsync();
        HideLoadingPanel();
        await EnableMusic();
        await StartGame();

        Debug.Log($"{_storageData.OperatingModeMainMenu} BootstrapGameplay");
    }
    
    public async UniTask ExitInMenuLevel()
    {   
        _factoryUIGameplay.HideUI();
        await UniTask.Yield();
        ShowLoadingPanel();
        await UniTask.Yield();
        await _loadReleaseGlobalScene.LoadSceneAsync(ScenesNames.SCENE_MAINMENU);
        await UniTask.Yield();
        HideLoadingPanel();
    }
    
    public async UniTask ExitInGameplayLevel()
    {   
        _factoryUIGameplay.HideUI();
        await UniTask.Yield();
        ShowLoadingPanel();
        await UniTask.Yield();
        await _loadReleaseGlobalScene.LoadSceneAsync(ScenesNames.SCENE_GAMEPLAY);
        await UniTask.Yield();
        HideLoadingPanel();
    }
    
    private async UniTask WaitForResourcesLoaded()
    {
        await UniTask.WaitUntil(() => _loadReleaseGameplay.IsLoaded);
        await UniTask.WaitUntil(() => _loadReleaseTraining.IsLoaded);
    }
    
    private async UniTask InitAudioAsync()
    {
        _soundsServiceGameplay.CreateSounds();
        await UniTask.Yield();
    }
    
    private void CreateCameras()
    {
        _factoryCamerasGameplay.CreateMainCamera();
        _virtualCamera = _factoryCamerasGameplay.CreateTopDownCamera();
    }
    
    private async UniTask CreateLoadingPanel()
    {
        _loadingPanel = _factoryUIGameplay.CreateShowLoading();
        await UniTask.Yield();
    }
    
    private void ShowLoadingPanel()
    {
        if (_loadingPanel != null)
            _loadingPanel.SetActive(true);
    }
    
    private async UniTask CreateUI()
    {
        _factoryUIGameplay.CreateUI();
        await UniTask.Yield();
        _factoryUITraining.CreateStartDialogue();
        await UniTask.Yield();
        _factoryUITraining.CreateEndDialogue();
        await UniTask.Yield();
        _factoryUITraining.CreateTaskDialogue();
        await UniTask.Yield();
        _factoryUITraining.CreateMiniTaskDialogue();
        await UniTask.Yield();
        _factoryUIGameplay.HideChecks();
        _factoryUIGameplay.HideOrder();
        _factoryUIGameplay.HideTime();
    }
    
    private async UniTask CreateEnvironmentAsync()
    {
        await _factoryEnvironmentTraining.CreateFurnitureTrainingGamePlayAsync();
        await UniTask.Yield();
        await _factoryEnvironment.CreateEnvironmentGamePlayAsync();
        await UniTask.Yield();
        _factoryEnvironment.CreateLightsGamePlay();
        await _notificationManager.CreateNotifications(3,3);
    }
    
    private async UniTask CreatePlayerAsync()
    {
        _factoryPlayerGameplay.CreatePlayerTraining(_virtualCamera);
        await UniTask.Yield();
    }
    
    private void HideLoadingPanel()
    {
        if (_loadingPanel != null)
            _loadingPanel.SetActive(false);
    }
    
    private async UniTask EnableMusic()
    {
        _soundsServiceGameplay.SetMusic();
        await UniTask.Yield();
    }
    
    private async UniTask StartGame()
    {
        _checksManager.Init();
        _ordersService.Init(true);
        InitMenuButtons?.Invoke();
        _gameOverService.Init(true);
        
        _updateChecks.Work = false;
        
        _dialogueManager.StartWedding();
        await UniTask.Yield();
    }
}
