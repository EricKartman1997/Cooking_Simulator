using System;
using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;

public class GameOver : IDisposable
{
    public event Action<Score,TimeGame> ShowAction;

    private readonly Score _score;
    private readonly TimeGame _timeGame;
    private readonly BootstrapGameplay _bootstrapGameplay;
    private readonly PauseHandler _pauseHandler;
    private readonly IInputBlocker _inputBlocker;


    public GameOver(Score score, TimeGame timeGame, BootstrapGameplay bootstrapGameplay,PauseHandler pauseHandler,IInputBlocker inputBlocker)
    {
        _pauseHandler = pauseHandler;
        _score = score;
        _timeGame = timeGame;
        _bootstrapGameplay = bootstrapGameplay;
        _inputBlocker = inputBlocker;
        
        EventBus.GameOver += OnGameOverMethod;
        //HideAction?.Invoke();
        //Debug.Log("Создать объект: GameOver");
    }

    public void Dispose()
    {
        EventBus.GameOver -= OnGameOverMethod;
        _inputBlocker.Unblock(this);
    }
    
    private void OnGameOverMethod()
    {
        Debug.Log("Игра закончена, время больше не идет");
        _pauseHandler.SetPause(true);
        _inputBlocker.Block(this);
        ShowAction?.Invoke(_score,_timeGame);


    }

    public async UniTask ExitButton()
    {
        await _bootstrapGameplay.ExitLevel();
    }
    
}
