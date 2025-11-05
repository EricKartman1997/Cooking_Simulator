using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Cinemachine;

public class BootstrapGameplay : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Transform pointPlayer;
    [SerializeField] private Transform parentPlayer;
    [SerializeField] private Transform parentFurniture;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    
    private LoadReleaseGameplay _loadReleaseGameplay;
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private FactoryUIGameplay _factoryUIGameplay;
    private FactoryPlayerGameplay _factoryPlayerGameplay;
    private FactoryEnvironment _factoryEnvironment;
    private SoundsServiceGameplay _soundsServiceGameplay;
    
    private GameObject _loadingPanel;
    
    [Inject]
    private void ConstructZenject(LoadReleaseGameplay loadReleaseGameplay, FactoryUIGameplay factoryUIGameplay,FactoryEnvironment factoryEnvironment, FactoryPlayerGameplay factoryPlayerGameplay,LoadReleaseGlobalScene loadReleaseGlobalScene,SoundsServiceGameplay soundsServiceGameplay)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _factoryUIGameplay = factoryUIGameplay;
        _factoryPlayerGameplay = factoryPlayerGameplay;
        _soundsServiceGameplay = soundsServiceGameplay;
        _factoryEnvironment = factoryEnvironment;
    }
    
    private void Start()
    {
        InitializeAsync().Forget();
    }
    
    private async UniTaskVoid InitializeAsync()
    {
        ShowLoadingPanel();
        
        // загрузить все ресурсы
        await WaitForResourcesLoaded();
        // создать игрока
        await CreatePlayerAsync();
        // создать окружения (furniture, other environment,)
        await CreateEnvironmentAsync();
        // создание сервисов
        // создать UI
        // включить музыку

        // выключить экран зарузка (удаляется)
        HideLoadingPanel();
        // подключить камеру к игроку
        //_factoryPlayerGameplay.EnableCamera(virtualCamera);
    }
    
    public async UniTask ExitLevel()
    {
        // остановить музыку
        // сохранить настройки
        // переход на сцену меню
    }
    
    private void ShowLoadingPanel()
    {
        _loadingPanel = Instantiate(_loadReleaseGameplay.GlobalPrefDic[GlobalPref.LoadingPanel], canvas.transform);
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
    }
    
    private async UniTask CreatePlayerAsync()
    {
       _factoryPlayerGameplay.CreatePlayer(pointPlayer, parentPlayer, virtualCamera);
        await UniTask.Yield(); // отдаём управление кадру
    }
    
    private async UniTask CreateEnvironmentAsync()
    {
        await _factoryEnvironment.CreateFurnitureGamePlayAsync(parentFurniture);
        await UniTask.Yield();
    }
 
    
}
