using System;
using UnityEngine;
using System.Collections;

public class GameOver : IDisposable
{
    public event Action<Score,TimeGame> ShowAction;
    public event Action HideAction;

    private Score _score;
    private TimeGame _timeGame;

    public GameOver(Score score, TimeGame timeGame)
    {
        _score = score;
        _timeGame = timeGame;
        
        EventBus.GameOver += OnGameOverMethod;
        //HideAction?.Invoke();
        //Debug.Log("Создать объект: GameOver");
    }

    public void Dispose()
    {
        EventBus.GameOver -= OnGameOverMethod;
        //Debug.Log("У объекта вызван Dispose : GameOver");
    }
    
    private void OnGameOverMethod()
    {
        Debug.Log("Игра закончена, время больше не идет");
        ShowAction?.Invoke(_score,_timeGame);
            
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
    
}
