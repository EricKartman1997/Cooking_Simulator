using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BootstrapGameplay : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Transform poinPlayer;
    [SerializeField] private Transform parentPlayer;
    [SerializeField] private Transform parentFurniture;
    
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
        _ = InitializeAsync();
    }
    
    private async Task InitializeAsync()
    {
        // экран зарузка
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
    }
    
    public async Task ExitLevel()
    {
        // остановить музыку
        // сохранить настройки
        // переход на сцену меню
    }
    
    private void ShowLoadingPanel()
    {
        _loadingPanel = Instantiate(_loadReleaseGameplay.GlobalPrefDic[GlobalPref.LoadingPanel], canvas.transform);
        //Debug.Log(" панель Загрузки");
    }
    
    private async Task WaitForResourcesLoaded()
    {
        // Рекомендуемый паттерн — если загрузчик может дать Task, использовать его.
        // Но если нет — опрашиваем:
        while (!_loadReleaseGameplay.IsLoaded)
        {
            //ct.ThrowIfCancellationRequested();
            await Task.Yield(); // ожидаем 1 кадр
        }
    }
    
    private async Task CreatePlayerAsync()
    {
        _factoryPlayerGameplay.CreatePlayer(poinPlayer,parentPlayer);
        await Task.Yield();
        //Debug.Log("Игрок создан");
    }
    
    private async Task CreateEnvironmentAsync()
    {
        _factoryEnvironment.CreateFurnitureGamePlay(parentFurniture);
        await Task.Yield();
        //Debug.Log("Игрок создан");
    }
 
    
}
