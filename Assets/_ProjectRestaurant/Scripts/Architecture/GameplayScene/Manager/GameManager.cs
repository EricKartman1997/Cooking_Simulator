using System;

public class GameManager : IDisposable
{
    private PauseHandler _pauseHandler;
    private Menu _menu;
    
    private bool _isPause;
    
    public bool IsPause => _isPause;
    
    public void Dispose()
    {
        // TODO release managed resources here
    }
    
    public void ShowMenu()
    {
        OnPause();
        _menu.Show();
    }
    
    public void HideMenu()
    {
        OffPause();
        _menu.Hide();
    }

    private void OnPause()
    {
        _isPause = true;
        _pauseHandler.SetPause(true);
    }
    
    private void OffPause()
    {
        _isPause = false;
        _pauseHandler.SetPause(false);
    }
    
}
