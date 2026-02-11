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
    private NotificationFiredCutletUI _notificationFiredCutletUI;
    
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

    public void Init(bool isTutorialLevel = false)
    {
        if (isTutorialLevel)
        {
            _ordersService.GameOver += GameOverTutorial;
        }
        _timeGameService.GameOver += GameOver;
        _ordersService.GameOver += GameOver;
        _statisticWindowUI = _factoryUIGameplay.StatisticWindowUI;
        _notificationFiredCutletUI = _factoryUIGameplay.NotificationFiredCutletUI;
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
    
    public void GameOverTutorial()
    {
        Debug.Log("Игра закончена, tutorial");
        _pauseHandler.SetPause(true, InputBlockType.All);
        //_factoryUIGameplay.EndDialogueUI.Show();
        
        // вызвать DialogueManager.StartGoodbye()!!!!!!!!!!!!
    }
    
    public void GameOverFireCutlet()
    {
        Debug.Log("Игра закончена, котлета сгорела");

        _pauseHandler.SetPause(true, InputBlockType.All);

        // 1. Подписываемся на событие закрытия Notification
        _notificationFiredCutletUI.OnHidden += OnNotificationHidden;

        // 2. Показываем Notification
        _notificationFiredCutletUI.Show();
    }

    private void OnNotificationHidden()
    {
        // Отписываемся, чтобы не срабатывало повторно
        _notificationFiredCutletUI.OnHidden -= OnNotificationHidden;

        // 3. Показываем окно статистики
        _statisticWindowUI.Show(_scoreService, _timeGameService);
    }
    
}
