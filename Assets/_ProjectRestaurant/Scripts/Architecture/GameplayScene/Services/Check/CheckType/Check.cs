using System;
using UnityEngine;
using Zenject;

public abstract class Check: IDisposable, ITickable, IPause
{
    private GameObject _prefab;
    private float _startTime;
    private float _score;
    private GameObject _dish;
    private IDeleteOverdueCheck _deleteCheck;

    public bool IsStop;
    
    private bool _isPause;
    private IHandlerPause _pauseHandler;
    
    public GameObject Prefab => _prefab;
    public float StartTime => _startTime;
    public float Score => _score;
    public GameObject Dish => _dish;

    protected Check(GameObject prefab, float startTime, float score, GameObject dish,
        IDeleteOverdueCheck deleteCheck,IHandlerPause pauseHandler)
    {
        _prefab = prefab;
        _startTime = startTime;
        _score = score;
        _dish = dish;
        _deleteCheck = deleteCheck;
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
    }
    
    public void Dispose()
    {
        _pauseHandler.Remove(this);
    }
    
    public void Tick()
    {
        if (IsStop == true)
            return;
        
        if (_isPause == true)
            return;
        
        UpdateTime();
    }

    public void ChangeTimeForTutorial()
    {
        _startTime = 0f;
    }

    private void UpdateTime()
    {
        if (_startTime > 0)
        {
            _startTime -= Time.deltaTime;
            return;
        }
        _deleteCheck.DeleteOverdueCheck(this);
        Debug.Log("Чек удален");
    }
    public abstract void Accept(ICheckVisitor checkVisitor);

    public void SetPause(bool isPaused) => _isPause = isPaused;

}
