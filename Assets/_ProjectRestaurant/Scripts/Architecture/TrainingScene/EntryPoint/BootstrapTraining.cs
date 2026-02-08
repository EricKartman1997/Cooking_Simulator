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
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private FactoryUIGameplay _factoryUIGameplay;
    private FactoryPlayerGameplay _factoryPlayerGameplay;
    private FactoryEnvironment _factoryEnvironment;
    private FactoryCamerasGameplay _factoryCamerasGameplay;
    private SoundsServiceGameplay _soundsServiceGameplay;
    private StorageData _storageData;
    private NotificationManager _notificationManager;
    private OrdersService _ordersService;
    private ChecksManager _checksManager;
    private GameOverService _gameOverService;
    
    private DiContainer _container;
    
    //ВКЛ игры
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
        OrdersService ordersService,ChecksManager checksManager,GameOverService gameOverService)
    {
        _loadReleaseGameplay = loadRelease;
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
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
        //_menu = menu;
    }
    
    private void Start()
    {
        InitializeAsync().Forget();
        _storageData.ThereIsInternetConnection();
    }
    
    private async UniTaskVoid InitializeAsync()
    {
        // ждем загрузки всех ресурсов
        await WaitForResourcesLoaded();
        // инициализация музыки
        await InitAudioAsync();
        // создать камеры
        CreateCameras();
        // создать экран загрузки
        await CreateLoadingPanel();
        // вкл загрузку
        ShowLoadingPanel();
        //создать UI
        await CreateUI();
        // создание сервисов
        //await CreateServiceAsync();
        // создать окружения (furniture, other environment,)
        await CreateEnvironmentAsync();
        // создать игрока
        await CreatePlayerAsync();
        // выключить экран загрузки (удаляется)
        HideLoadingPanel();
        // вкл музыку
        await EnableMusic();
        // запускаем игру
        await StartGame();

        Debug.Log($"{_storageData.OperatingModeMainMenu} BootstrapGameplay");
    }
    
    public async UniTask ExitLevel()
    {   
        // выкл UI игрока
        _factoryUIGameplay.HideUI();
        await UniTask.Yield();
        // вкл загрузку
        ShowLoadingPanel();
        await UniTask.Yield();
        // переход на сцену меню
        await _loadReleaseGlobalScene.LoadSceneAsync(ScenesNames.SCENE_MAINMENU);
        await UniTask.Yield();
        HideLoadingPanel();
    }
    
    private async UniTask WaitForResourcesLoaded()
    {
        await UniTask.WaitUntil(() => _loadReleaseGameplay.IsLoaded);
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
    
    private async UniTask CreateUI() //меняем на UI с обучением
    {
        _factoryUIGameplay.CreateUI();
        await UniTask.Yield();
        _factoryUIGameplay.CreateStatisticsWindow();
        await UniTask.Yield();
        _factoryUIGameplay.CreateNotificationFiredCutlet();
        await UniTask.Yield();
    }
    
    private async UniTask CreateEnvironmentAsync() //создаем меньшее кол-во для обучения
    {
        await _factoryEnvironment.CreateFurnitureTrainingGamePlayAsync();
        await UniTask.Yield();
        await _factoryEnvironment.CreateEnvironmentGamePlayAsync();
        await UniTask.Yield();
        _factoryEnvironment.CreateLightsGamePlay();
        await _notificationManager.CreateNotifications(3,3);
    }
    
    private async UniTask CreatePlayerAsync()
    {
        _factoryPlayerGameplay.CreatePlayer(_virtualCamera);
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
        _ordersService.Init();
        InitMenuButtons?.Invoke();
        _timeGameService.Init();
        _gameOverService.Init();
        
        _updateChecks.Work = true;
        
        _ordersService.UpdateOrder();
        await UniTask.Yield();
    }
}
