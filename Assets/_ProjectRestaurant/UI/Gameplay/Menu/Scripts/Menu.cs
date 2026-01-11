using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Menu : IDisposable
{
    public event Action ShowMenuAction;
    public event Action HideMenuAction;
    public event Action HideSettingsAction;
    public event Func<bool> IsOpenSettingsAction;
    
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private PauseHandler _pauseHandler;
    private BootstrapGameplay _bootstrapGameplay;
    private readonly IInputBlocker _inputBlocker;
    
    private GameObject _panelSettings;
    
    public Menu(LoadReleaseGlobalScene loadReleaseGlobalScene,PauseHandler pauseHandler,
                InputBlocker inputBlocker,BootstrapGameplay bootstrapGameplay)
    {
        _bootstrapGameplay = bootstrapGameplay;
        _pauseHandler = pauseHandler;
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _inputBlocker = inputBlocker;
    }

    public void Dispose()
    {
        
    }
    
    public void ContinueButton()
    {
        // выкл паузу
        // Time.timeScale = 1f;
        // AudioListener.pause = false;
        Hide();
    }
    
    public void SettingsButton()
    {
        // открыть настройки
        if (_panelSettings == null)
        {
            Debug.Log("пока не работает");
            return;
        }
        _panelSettings.SetActive(true);
    }
    
    public async UniTask ExitButton()
    {
        await _bootstrapGameplay.ExitLevel();
    }

    public void Show()
    {
        _pauseHandler.SetPause(true);
        _inputBlocker.Block(this);
        ShowMenuAction?.Invoke();
    }
    
    public void Hide()
    {
        if (IsOpenSettingsAction != null && IsOpenSettingsAction.Invoke())
        {
            HideSettingsAction?.Invoke();
            return;
        }
        _pauseHandler.SetPause(false);
        _inputBlocker.Unblock(this);
        HideMenuAction?.Invoke();
        EventBus.PauseOff.Invoke();
    }
}
