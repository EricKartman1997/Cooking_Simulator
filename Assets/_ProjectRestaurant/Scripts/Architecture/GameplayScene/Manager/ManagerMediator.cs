using System;
using UnityEngine;

public class ManagerMediator : IDisposable
{
    private TimeGameMediator _timeGameMediator;
    private OrdersMediator _ordersMediator;
    private CheckPanelMediator _checkPanelMediator;
    private GameOverMediator _gameOverMediator;
    
    private TimeGame _timeGame;
    private Orders _orders;
    private IActionCheck _checksManager;
    private GameOver _gameOver;
    private GameObject _windowGameOver;
    private GameObject _windowGame;
    
    public ManagerMediator(TimeGame timeGame, Orders orders, IActionCheck checksManager, GameOver gameOver,
        GameObject windowGameOver,
        GameObject windowGame)
    {
        _timeGame = timeGame;
        _orders = orders;
        _checksManager = checksManager;
        _gameOver = gameOver;
        _windowGameOver = windowGameOver;
        _windowGame = windowGame;
        
        CreateMediator();
        
        Debug.Log("Создан объект: ManagerMediator");
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : ManagerMediator");
    }

    private void CreateMediator()
    {
        _timeGameMediator = new TimeGameMediator(_timeGame, _windowGame.GetComponent<TimeGameUI>() );

        _ordersMediator = new OrdersMediator(_orders, _windowGame.GetComponent<OrdersUI>());

        _checkPanelMediator = new CheckPanelMediator(_checksManager, _windowGame.GetComponent<ChecksPanalUI>());
        
        _gameOverMediator = new GameOverMediator(_gameOver,_windowGameOver.GetComponent<GameOverUI>());
        
        Debug.Log("Создать объект: ManagerMediator");
    }
}
