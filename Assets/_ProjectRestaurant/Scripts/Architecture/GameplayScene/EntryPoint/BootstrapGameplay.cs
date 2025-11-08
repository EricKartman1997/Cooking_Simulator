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
    
    private GameObject _loadingPanel;
    
    [Inject]
    private void ConstructZenject(LoadReleaseGameplay loadReleaseGameplay, FactoryUIGameplay factoryUIGameplay,FactoryEnvironment factoryEnvironment, FactoryPlayerGameplay factoryPlayerGameplay,LoadReleaseGlobalScene loadReleaseGlobalScene,SoundsServiceGameplay soundsServiceGameplay,FactoryCamerasGameplay factoryCamerasGameplay)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _factoryUIGameplay = factoryUIGameplay;
        _factoryPlayerGameplay = factoryPlayerGameplay;
        _soundsServiceGameplay = soundsServiceGameplay;
        _factoryEnvironment = factoryEnvironment;
        _factoryCamerasGameplay = factoryCamerasGameplay;
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
        // создать окружения (furniture, other environment,)
        await CreateEnvironmentAsync();
        // создать игрока
        await CreatePlayerAsync();
        // создание сервисов
        // включить музыку
        // выключить экран загрузки (удаляется)
        HideLoadingPanel();


    }
    
    public async UniTask ExitLevel()
    {
        // остановить музыку
        // сохранить настройки
        // переход на сцену меню
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
    
    private async UniTask WaitForResourcesLoaded()
    {
        // если загрузчик не даёт Task напрямую — просто ждём, пока он готов
        await UniTask.WaitUntil(() => _loadReleaseGameplay.IsLoaded);
        Debug.Log("Загружены все ресурсы для Gameplay");
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
    
}
