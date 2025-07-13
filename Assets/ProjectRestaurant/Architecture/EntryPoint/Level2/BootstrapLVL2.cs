using System;
using UnityEngine;

public class BootstrapLVL2 : IDisposable
{
    private FieldsForScriptContainer _FC;

    private Checks _checks;
    private Score _score;
    private UpdateChecks _updateChecks;
    private Orders _orders;
    private OrdersUI _ordersUI;
    private EventBus _eventBus;
    private TimeGame _timeGame;
    private GameOver _gameOver;
    
    public Checks Checks => _checks;

    public Score Score => _score;

    public UpdateChecks UpdateChecks => _updateChecks;

    public Orders Orders => _orders;

    public OrdersUI OrdersUI => _ordersUI;

    public EventBus EventBus => _eventBus;
    
    public TimeGame TimeGame => _timeGame;

    public BootstrapLVL2(FieldsForScriptContainer fc)
    {
        _FC = fc;
        
        _eventBus = new EventBus();
        _checks = new Checks(_FC.CheckContainer, _FC.Content);
        _updateChecks = new UpdateChecks(_checks,3f);
        _orders = new Orders();
        _ordersUI = new OrdersUI(_orders,_FC.Scoretext);
        _timeGame = new TimeGame(_FC.TimeText);
        _score = new Score(_timeGame);
        _gameOver = new GameOver(_FC.WindowScore,_FC.ScoreNumbersText,_FC.TimeNumbersText,_FC.AssignmentNumbersTimeText,_FC.ContinueButton);
        Debug.Log("Создать объект: BootstrapLVL2");
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : BootstrapLVL2");
    }
    
}
