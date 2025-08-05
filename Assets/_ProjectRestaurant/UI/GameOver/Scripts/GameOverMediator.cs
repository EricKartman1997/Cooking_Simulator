using System;

public class GameOverMediator: IDisposable
{
    private GameOver _gameOver;
    private GameOverUI _gameOverUI;

    public GameOverMediator(GameOver gameOver, GameOverUI gameOverUI)
    {
        _gameOverUI = gameOverUI;
        _gameOver = gameOver;
        _gameOver.HideAction += Hide;
        _gameOver.ShowAction += Show;
    }

    public void Dispose()
    {
        _gameOver.HideAction -= Hide;
        _gameOver.ShowAction -= Show;
    }
    
    public void Show(Score score,TimeGame timeGame) => _gameOverUI.Show(score,timeGame);

    public void Hide() => _gameOverUI.Hide();

}