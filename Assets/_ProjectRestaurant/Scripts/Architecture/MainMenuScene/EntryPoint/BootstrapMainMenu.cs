using System;
using System.Threading;
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
    private CancellationTokenSource _cts;

    [Inject]
    private void ConstructZenject(
        LoadReleaseMainMenuScene loadReleaseMainMenuScene,
        FactoryUIMainMenuScene factoryUIMainMenuScene,
        LoadReleaseGlobalScene loadReleaseGlobalScene,
        SoundsServiceMainMenu soundsServiceMainMenu)
    {
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
        _factoryUIMainMenuScene = factoryUIMainMenuScene;
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _soundsServiceMainMenu = soundsServiceMainMenu;
    }

    private void Start()
    {
        _cts = new CancellationTokenSource();
        _ = InitializeAsync(_cts.Token); // запущено "fire-and-forget", но Task возвращаемый и ошибки ловим внутри
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private async Task InitializeAsync(CancellationToken ct)
    {
        //_soundsServiceMainMenu.MuteSources();
        try
        {
            ShowLoadingPanel();

            await WaitForResourcesLoaded(ct);
            ct.ThrowIfCancellationRequested();

            await CreateUI(ct);
            ct.ThrowIfCancellationRequested();

            // небольшая пауза (можно убрать или заменить на await Task.Delay(..., ct))
            await Task.Delay(1200, ct);

            await EnableMusic(ct);
            ct.ThrowIfCancellationRequested();

            StartLevel();
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Инициализация отменена");
            // корректно убрать loading panel, etc.
            SafeHideLoadingPanel();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка инициализации меню: {ex}");
            SafeHideLoadingPanel();
            // показать уведомление пользователю или fallback UI
        }
        //_soundsServiceMainMenu.UnMuteSources();
    }

    private void ShowLoadingPanel()
    {
        _loadingPanel = Instantiate(_loadReleaseMainMenuScene.GlobalPrefDic[GlobalPref.LoadingPanel], canvas.transform);
    }

    private void SafeHideLoadingPanel()
    {
        if (_loadingPanel != null) Destroy(_loadingPanel);
    }

    private async Task WaitForResourcesLoaded(CancellationToken ct)
    {
        // Рекомендуемый паттерн — если загрузчик может дать Task, использовать его.
        // Но если нет — опрашиваем:
        while (!_loadReleaseMainMenuScene.IsLoaded)
        {
            ct.ThrowIfCancellationRequested();
            await Task.Yield(); // ожидаем 1 кадр
        }
    }

    private async Task CreateUI(CancellationToken ct)
    {
        _mainUIPanel = _factoryUIMainMenuScene.Get(PrefUINameMainMenu.UIPanel, canvas.transform);
        // даём одному кадру выполниться (если нужно)
        await Task.Yield();
    }

    private async Task EnableMusic(CancellationToken ct)
    {
        _soundsServiceMainMenu.SetMusic();
        await Task.Yield();
    }

    private void StartLevel()
    {
        if (_mainUIPanel != null)
            _mainUIPanel.SetActive(true);

        SafeHideLoadingPanel();
    }

    public async Task ExitLevel()
    {
        // сохранить настройки...
        await _loadReleaseGlobalScene.LoadSceneAsync("SceneGameplay");
        _soundsServiceMainMenu.StopSounds();
        ShowLoadingPanel();
    }
}
