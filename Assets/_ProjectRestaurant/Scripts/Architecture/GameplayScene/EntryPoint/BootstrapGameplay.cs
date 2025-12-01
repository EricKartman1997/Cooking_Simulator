using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Cinemachine;

public class BootstrapGameplay : MonoBehaviour
{
    //[SerializeField] private GameObject canvas;
    //[SerializeField] private CinemachineVirtualCamera virtualCamera;
    private GameObject _canvas;
    private CinemachineVirtualCamera _virtualCamera;
    
    private LoadReleaseGameplay _loadReleaseGameplay;
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private FactoryUIGameplay _factoryUIGameplay;
    private FactoryPlayerGameplay _factoryPlayerGameplay;
    private FactoryEnvironment _factoryEnvironment;
    private FactoryCamerasGameplay _factoryCamerasGameplay;
    private SoundsServiceGameplay _soundsServiceGameplay;
    private TimeGame _timeGame;
    private Orders _orders;
    private GameOver _gameOver;
    private IActionCheck _checksManager;
    private UpdateChecks _updateChecks;
    
    private GameObject _loadingPanel;

    [Inject]
    public void ConstructZenject(LoadReleaseGameplay loadReleaseGameplay, LoadReleaseGlobalScene loadReleaseGlobalScene,
        FactoryUIGameplay factoryUIGameplay, FactoryPlayerGameplay factoryPlayerGameplay,
        FactoryEnvironment factoryEnvironment, FactoryCamerasGameplay factoryCamerasGameplay,
        SoundsServiceGameplay soundsServiceGameplay, TimeGame timeGame, Orders orders, GameOver gameOver,
        IActionCheck checksManager, UpdateChecks updateChecks)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _factoryUIGameplay = factoryUIGameplay;
        _factoryPlayerGameplay = factoryPlayerGameplay;
        _factoryEnvironment = factoryEnvironment;
        _factoryCamerasGameplay = factoryCamerasGameplay;
        _soundsServiceGameplay = soundsServiceGameplay;
        _timeGame = timeGame;
        _orders = orders;
        _gameOver = gameOver;
        _checksManager = checksManager;
        _updateChecks = updateChecks;
    }
    
    private void Start()
    {
        InitializeAsync().Forget();
    }
    
    private async UniTaskVoid InitializeAsync()
    {
        // ждем загрузки всех ресурсов
        await WaitForResourcesLoaded();
        // создать камеры
        CreateCameras();
        //создать UI
        CreateUI();
        // вкл загрузку
        ShowLoadingPanel();
        // инициализация музыки
        await InitAudioAsync();
        // создание сервисов
        await CreateServiceAsync();
        // создать окружения (furniture, other environment,)
        await CreateEnvironmentAsync();
        // создать игрока
        await CreatePlayerAsync();
        // выключить экран загрузки (удаляется)
        HideLoadingPanel();
        // вкл музыку
        //await EnableAudioAsync();
        StartGame();

    }
    
    public async UniTask ExitLevel()
    {
        // остановить музыку
        // сохранить настройки
        // переход на сцену меню
    }

    private async UniTask WaitForResourcesLoaded()
    {
        // если загрузчик не даёт Task напрямую — просто ждём, пока он готов
        await UniTask.WaitUntil(() => _loadReleaseGameplay.IsLoaded);
        Debug.Log("Загружены все ресурсы для Gameplay");
    }
    
    private void CreateCameras()
    {
        _factoryCamerasGameplay.CreateMainCamera();
        _virtualCamera = _factoryCamerasGameplay.CreateTopDownCamera();
    }
    private void CreateUI()
    {
        _canvas = _factoryUIGameplay.CreateUI();
    }
    
    private void ShowLoadingPanel()
    {
        _loadingPanel = Instantiate(_loadReleaseGameplay.GlobalPrefDic[GlobalPref.LoadingPanel], _canvas.transform);
    }

    private void HideLoadingPanel()
    {
        if (_loadingPanel != null)
            Destroy(_loadingPanel);
    }
    
    private async UniTask CreatePlayerAsync()
    {
       _factoryPlayerGameplay.CreatePlayer(_virtualCamera);
        await UniTask.Yield(); // отдаём управление кадру
    }
    
    private async UniTask CreateEnvironmentAsync()
    {
        await _factoryEnvironment.CreateFurnitureGamePlayAsync();
        await UniTask.Yield();
        _factoryEnvironment.CreateOtherEnvironmentGamePlay();
        await UniTask.Yield();
        _factoryEnvironment.CreateLightsGamePlay();
    }
    
    private async UniTask InitAudioAsync()
    {
        _soundsServiceGameplay.CreateSounds();
        await UniTask.Yield();

    }
    
    private async UniTask CreateServiceAsync()
    {
        //timeGame сделал
        //Order сделал
        //GameOver
        //UpdateCheck
        //ChecksManager сделал
        new ManagerMediator(_timeGame, _orders, _checksManager, _gameOver, _factoryUIGameplay.GameOverWindow, _factoryUIGameplay.GameWindow);
        await UniTask.Yield();
    }
    
    private async UniTask EnableAudioAsync()
    {
        _soundsServiceGameplay.SetMusic();
        await UniTask.Yield();

    }
    
    private void StartGame()
    {
        _timeGame.Work = true;
        _updateChecks.Work = true;
    }

    
}
