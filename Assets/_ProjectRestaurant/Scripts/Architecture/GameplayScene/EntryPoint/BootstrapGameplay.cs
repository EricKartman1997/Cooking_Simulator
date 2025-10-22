using UnityEngine;

public class BootstrapGameplay : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Transform poinPlayer;
    [SerializeField] private Transform parentPlayer;
    
    
    private LoadReleaseGameplay _loadReleaseGameplay;
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private FactoryUIGameplay _factoryUIGameplay;
    private FactoryPlayerGameplay _factoryPlayerGameplay;
    private SoundsServiceGameplay _soundsServiceGameplay;
    
    private GameObject _loadingPanel;
    
    private void ConstructZenject(LoadReleaseGameplay loadReleaseGameplay, FactoryUIGameplay factoryUIGameplay, FactoryPlayerGameplay factoryPlayerGameplay,LoadReleaseGlobalScene loadReleaseGlobalScene,SoundsServiceGameplay soundsServiceGameplay)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _factoryUIGameplay = factoryUIGameplay;
        _factoryPlayerGameplay = factoryPlayerGameplay;
        _soundsServiceGameplay = soundsServiceGameplay;
    }
    
    private void Start()
    {
        // экран зарузка
        ShowLoadingPanel();
        // создать игрока
        _factoryPlayerGameplay.CreatePlayer(poinPlayer,parentPlayer);
        // создать окружения
        // создание сервисов
        // создать UI
        // включить музыку
    }

    public void ExitLevel()
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
    
    


}
