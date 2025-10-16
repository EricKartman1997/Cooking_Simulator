using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BootstrapMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private FactoryUIMainMenuScene _factoryUIMainMenuScene;
    private SoundsServiceMainMenu _soundsServiceMainMenu;

    private GameObject _mainUIPanel;
    
    private GameObject _loadingPanel;

    [Inject]
    private void ConstructZenject(LoadReleaseMainMenuScene loadReleaseMainMenuScene, FactoryUIMainMenuScene factoryUIMainMenuScene,LoadReleaseGlobalScene loadReleaseGlobalScene,SoundsServiceMainMenu soundsServiceMainMenu)
    {
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _factoryUIMainMenuScene = factoryUIMainMenuScene;
        _soundsServiceMainMenu = soundsServiceMainMenu;
    }

    private async void Start()
    {
        ShowLoadingPanel();
        await WaitForResourcesLoaded();
        await CreateUI();
        await Task.Delay(1200);
        await EnableMusic();
        StartLevel();
    }

    public async void ExitLevel()
    {
        // сохранить настройки
        // переход на сцену гемплея
        await _loadReleaseGlobalScene.LoadSceneAsync("SceneGameplay"); 
        // остановить музыку
        _soundsServiceMainMenu.StopSounds();
        ShowLoadingPanel();
    }

    private void ShowLoadingPanel()
    {
        _loadingPanel = Instantiate(_loadReleaseMainMenuScene.GlobalPrefDic[GlobalPref.LoadingPanel], canvas.transform);
        //Debug.Log(" панель Загрузки");
    }
    
    private async Task WaitForResourcesLoaded()
    {
        // Ожидаем, пока ресурсы загрузятся
        while (!_loadReleaseMainMenuScene.IsLoaded == true) 
        {
            await Task.Yield(); // Ждем один кадр
        }
        while (!_loadReleaseMainMenuScene.IsLoaded == true) 
        {
            await Task.Yield(); // Ждем один кадр
        }
        //Debug.Log("Подгрузка завершена");
    }
    
    private async Task CreateUI()
    {
        _mainUIPanel = _factoryUIMainMenuScene.Get(PrefUINameMainMenu.UIPanel, canvas.transform);
        await Task.Yield();
    }

    private async Task EnableMusic()
    {
        _soundsServiceMainMenu.SetMusic();
        await Task.Yield();
    }

    private void StartLevel()
    {
        _mainUIPanel.SetActive(true);
        HideLoadingPanel();
    }
    
    private void HideLoadingPanel()
    {
        Destroy(_loadingPanel);
        //Debug.Log(" панель Загрузки удалена");
    }
    
}
