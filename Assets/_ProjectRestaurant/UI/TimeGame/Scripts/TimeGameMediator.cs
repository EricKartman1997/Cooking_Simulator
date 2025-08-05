using System;
using UnityEngine;

public class TimeGameMediator : IDisposable
{
    private TimeGame _timeGame;
    private TimeGameUI _timeGameUI;

    public TimeGameMediator(TimeGame timeGame, TimeGameUI timeGameUI)
    {
        _timeGame = timeGame;
        _timeGame.UpdateTime += OnUpdateTime;
        _timeGame.ShowTime += OnShow;
        _timeGameUI = timeGameUI;
        Debug.Log("Создать объект: TimeGameMediator");
    }

    public void Dispose()
    {
        _timeGame.UpdateTime -= OnUpdateTime;
        _timeGame.ShowTime -= OnShow;
        Debug.Log("У объекта вызван Dispose : TimeGameMediator");
    }

    private void OnShow()
    {
        _timeGameUI.Show();
    }
    
    private void OnHide()
    {
        _timeGameUI.Hide();
    }

    private void OnUpdateTime(float minutes, float seconds)
    {
        _timeGameUI.UpdateTime(minutes,seconds);
    }
}
