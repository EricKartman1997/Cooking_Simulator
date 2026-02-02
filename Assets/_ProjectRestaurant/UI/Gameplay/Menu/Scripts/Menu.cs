using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Menu : IDisposable
{
    // public event Action ShowMenuAction;
    // public event Action HideMenuAction;
    // public event Action HideSettingsAction;
    // public event Func<bool> IsOpenSettingsAction;
    //
    private PauseHandler _pauseHandler;
    private BootstrapGameplay _bootstrapGameplay;
    private MenuUI _menuUI;
    private FactoryUIGameplay _factoryUIGameplay;
    
    private GameObject _panelSettings;
    
    public Menu(PauseHandler pauseHandler, BootstrapGameplay bootstrapGameplay,FactoryUIGameplay factoryUIGameplay)
    {
        _bootstrapGameplay = bootstrapGameplay;
        _pauseHandler = pauseHandler;
        _factoryUIGameplay = factoryUIGameplay;
        //_menuUI = factoryUIGameplay.MenuUI;

        _bootstrapGameplay.InitMenuButtons += Init;
    }
    
    public void Dispose()
    {
        _menuUI.ContinueAction -= ContinueButton;
        _menuUI.ExitAction -= ExitButton;
        _bootstrapGameplay.InitMenuButtons -= Init;
    }
    
    private void Init()
    {
        _menuUI = _factoryUIGameplay.MenuUI;
        _menuUI.ContinueAction += ContinueButton;
        _menuUI.ExitAction += ExitButton;
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
        _pauseHandler.SetPause(true,InputBlockType.Movement | InputBlockType.OnPressE);
        _menuUI.ShowMenu();
    }
    
    public void Hide()
    {
        if ( _menuUI.IsOpen)
        {
            _menuUI.HideSettingsPanel();
            return;
        }
        _pauseHandler.SetPause(false,InputBlockType.Movement | InputBlockType.OnPressE);
        _menuUI.HideMenu();
    }
}
