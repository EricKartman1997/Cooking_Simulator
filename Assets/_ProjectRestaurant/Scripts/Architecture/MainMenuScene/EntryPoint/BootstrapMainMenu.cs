using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BootstrapMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;
    private FactoryUIMainMenuScene _factoryUIMainMenuScene;

    private GameObject _mainUIPanel;
    
    private GameObject _loadingPanel;

    [Inject]
    private void ConstructZenject(LoadReleaseMainMenuScene loadReleaseMainMenuScene, FactoryUIMainMenuScene factoryUIMainMenuScene)
    {
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
        _factoryUIMainMenuScene = factoryUIMainMenuScene;
    }

    private async void Start()
    {
        ShowLoadingPanel();
        //await Task.Delay(3000);
        await WaitForResourcesLoaded();
        //await Task.Delay(3000);
        await CreateUI();
        await Task.Delay(1200);
        StartLevel();
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
