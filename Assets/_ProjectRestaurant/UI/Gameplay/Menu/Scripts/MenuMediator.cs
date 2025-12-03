using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMediator : IDisposable
{
    private Menu _menu;
    private MenuUI _menuUI;

    public MenuMediator(Menu menu, MenuUI menuUI)
    {
        _menu = menu;
        _menuUI = menuUI;
        
        _menuUI.ContinueAction += _menu.ContinueButton;
        _menuUI.SettingsAction += _menu.SettingsButton;
        _menuUI.ExitAction += _menu.ExitButton;
        _menuUI.ShowAction += _menu.Show;
    }

    public void Dispose()
    {
        _menuUI.ContinueAction -= _menu.ContinueButton;
        _menuUI.SettingsAction -= _menu.SettingsButton;
        _menuUI.ExitAction -= _menu.ExitButton;
    }
    
    // private void ContinueButton()
    // {
    //     //_menuUI.ContinueAction += _menu.;
    // }
    //
    // private void SettingsButton()
    // {
    //     //_menuUI.SettingsAction += _menu.;
    // }
    //
    // private void ExitButton()
    // {
    //     //_menuUI.ExitAction += _menu.;
    // }
}
