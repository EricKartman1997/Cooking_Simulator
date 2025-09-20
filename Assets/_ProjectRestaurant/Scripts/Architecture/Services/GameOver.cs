using System;
using UnityEngine;
using System.Collections;

public class GameOver : IDisposable
{
    public event Action<Score,TimeGame> ShowAction;
    public event Action HideAction;
    
    private GameManager _gameManager;
    private MonoBehaviour _coroutineMonoBehaviour;
    
    private bool _isInit;
    
    public bool IsInit => _isInit;
    
    private TimeGame TimeGame => StaticManagerWithoutZenject.GameManager.TimeGame;
    private Score Score => StaticManagerWithoutZenject.GameManager.Score;

    public GameOver(MonoBehaviour coroutineMonoBehaviour)
    {
        _coroutineMonoBehaviour = coroutineMonoBehaviour;

        _coroutineMonoBehaviour.StartCoroutine(Init());
        Init();
    }

    public void Dispose()
    {
        EventBus.GameOver -= OnGameOverMethod;
        Debug.Log("У объекта вызван Dispose : GameOver");
    }
    
    private IEnumerator Init()
    {
        EventBus.GameOver += OnGameOverMethod;
        
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        
        HideAction?.Invoke();
        
        Debug.Log("Создать объект: GameOver");
        _isInit = true;
    }
    
    private void OnGameOverMethod()
    {
        Debug.Log("Игра закончена, время больше не идет");
        ShowAction?.Invoke(Score,TimeGame);
            
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
    
}
