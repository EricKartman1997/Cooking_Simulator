using System;
using System.Collections;
using UnityEngine;

public class UIManager : IDisposable
{
    private TimeGameMediator _timeGameMediator;
    private OrdersMediator _ordersMediator;
    private CheckPanelMediator _checkPanelMediator;
    private GameOverMediator _gameOverMediator;
    
    private TimeGame _timeGame;
    private Orders _orders;
    private Checks _checks;
    private GameOver _gameOver;

    
    private GameManager _gameManager;
    private UIContainer _uiContainer;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    private bool _isInit;

    public bool IsInit => _isInit;
    
    public UIManager(CoroutineMonoBehaviour coroutineMonoBehaviour)
    {
        _coroutineMonoBehaviour = coroutineMonoBehaviour;
        
        _coroutineMonoBehaviour.StartCoroutine(Init());
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : UIManager");
    }

    private IEnumerator Init()
    {
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }

        while (_uiContainer == null)
        {
            _uiContainer = _gameManager.UIContainer;
            yield return null;
        }
        
        while (_timeGame == null)
        {
            _timeGame = _gameManager.TimeGame;
            yield return null;
        }
        
        while (_timeGameMediator == null)
        {
            _timeGameMediator = new TimeGameMediator(_gameManager.TimeGame, _uiContainer.WindowGame.GetComponent<TimeGameUI>() );
            yield return null;
        }
        
        while (_orders == null)
        {
            _orders = _gameManager.Orders;
            yield return null;
        }

        while (_ordersMediator == null)
        {
            _ordersMediator = new OrdersMediator(_gameManager.Orders, _uiContainer.WindowGame.GetComponent<OrdersUI>());
            yield return null;
        }
        
        while (_checks == null)
        {
            _checks = _gameManager.Checks;
            yield return null;
        }

        while (_checkPanelMediator == null)
        {
            _checkPanelMediator = new CheckPanelMediator(_gameManager.Checks, _uiContainer.WindowGame.GetComponent<ChecksPanalUI>());
            yield return null;
        }
        
        while (_gameOver == null)
        {
            _gameOver = _gameManager.GameOver;
            yield return null;
        }
        
        while ( _gameOverMediator == null)
        {
            _gameOverMediator = new GameOverMediator(_gameManager.GameOver,_uiContainer.WindowGameOver.GetComponent<GameOverUI>());
            yield return null;
        }
        
        Debug.Log("Создать объект: UIManager");
        _isInit = true;
    }
}
