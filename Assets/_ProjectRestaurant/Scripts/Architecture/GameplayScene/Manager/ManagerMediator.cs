using System;
using UnityEngine;

public class ManagerMediator : IDisposable
{
    private TimeGameMediator _timeGameMediator;
    private OrdersMediator _ordersMediator;
    private CheckPanelMediator _checkPanelMediator;
    private GameOverMediator _gameOverMediator;
    private MenuMediator _menuMediator;
    
    private TimeGame _timeGame;
    private Orders _orders;
    private IActionCheck _checksManager;
    private GameOver _gameOver;
    private Menu _menu;
    
    private TimeGameUI _timeGameUI;
    private OrdersUI _ordersUI;
    private ChecksPanalUI _checksPanalUI;
    private GameOverUI _gameOverUI;
    private MenuUI _menuUI;
    
    
    public ManagerMediator(TimeGame timeGame, Orders orders, IActionCheck checksManager, GameOver gameOver, Menu menu,
        TimeGameUI timeGameUI, OrdersUI ordersUI, ChecksPanalUI checksPanalUI, GameOverUI gameOverUI, MenuUI menuUI
        )
    {
        
        _timeGame = timeGame;
        _orders = orders;
        _checksManager = checksManager;
        _gameOver = gameOver;
        _menu = menu;
        
        _timeGameUI = timeGameUI;
        _ordersUI = ordersUI;
        _checksPanalUI = checksPanalUI;
        _gameOverUI = gameOverUI ;
        _menuUI = menuUI;;
        
        CreateMediator();
        
        Debug.Log("Создан объект: ManagerMediator");
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : ManagerMediator");
    }

    private void CreateMediator()
    {
        _timeGameMediator = new TimeGameMediator(_timeGame, _timeGameUI);

        _ordersMediator = new OrdersMediator(_orders, _ordersUI);

        _checkPanelMediator = new CheckPanelMediator(_checksManager, _checksPanalUI);
        
        _gameOverMediator = new GameOverMediator(_gameOver,_gameOverUI);
        
        _menuMediator = new MenuMediator(_menu,_menuUI);
        
        //Debug.Log("Создать объект: ManagerMediator");
    }
}
