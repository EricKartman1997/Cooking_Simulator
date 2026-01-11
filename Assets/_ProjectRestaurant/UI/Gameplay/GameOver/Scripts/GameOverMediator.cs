using System;

public class GameOverMediator: IDisposable
{
    private GameOver _gameOver;
    private GameOverUI _gameOverUI;

    public GameOverMediator(GameOver gameOver, GameOverUI gameOverUI)
    {
        _gameOverUI = gameOverUI;
        _gameOver = gameOver;
        _gameOver.ShowAction += Show;
        _gameOverUI.ExitAction += _gameOver.ExitButton;

    }

    public void Dispose()
    {
        _gameOver.ShowAction -= Show;
        _gameOverUI.ExitAction -= _gameOver.ExitButton;
    }

    private void Show(Score score,TimeGame timeGame) => _gameOverUI.Show(score,timeGame);
    


}