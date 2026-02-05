using System;
using UnityEngine;

public class GameOverService : IDisposable
{
    private readonly ScoreService _scoreService;
    private readonly TimeGameService _timeGameService;
    private readonly OrdersService _ordersService;
    private readonly PauseHandler _pauseHandler;
    private readonly FactoryUIGameplay _factoryUIGameplay;
    
    private StatisticWindowUI _statisticWindowUI;


    public GameOverService(ScoreService scoreService, TimeGameService timeGameService,OrdersService ordersService,PauseHandler pauseHandler,
        FactoryUIGameplay factoryUIGameplay)
    {
        _pauseHandler = pauseHandler;
        _scoreService = scoreService;
        _timeGameService = timeGameService;
        _ordersService = ordersService;
        _factoryUIGameplay = factoryUIGameplay;
        //_gameOverUI = factoryUIGameplay.GameOverUI;
    }

    public void Init()
    {
        _timeGameService.GameOver += GameOver;
        _ordersService.GameOver += GameOver;
        _statisticWindowUI = _factoryUIGameplay.StatisticWindowUI;
    }

    public void Dispose()
    {
        _timeGameService.GameOver -= GameOver;
        _ordersService.GameOver -= GameOver;
    }
    
    public void GameOver()
    {
        Debug.Log("Игра закончена, время больше не идет");
        _pauseHandler.SetPause(true, InputBlockType.All);
        _statisticWindowUI.Show(_scoreService,_timeGameService);
    }
    
}
