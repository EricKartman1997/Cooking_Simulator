using System;
using UnityEngine;
using Zenject;

public class UpdateChecks : IDisposable, ITickable,IPause
{
    private IAddCheck _checksManager;
    private float _timeAddNewCheck = 3f;
    private float _timeUpdateCheck;

    public bool Work;
    
    private bool _isPause;
    private IHandlerPause _pauseHandler;

    public UpdateChecks(IAddCheck checksManager,IHandlerPause pauseHandler)
    {
        _checksManager = checksManager;
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
        //Debug.Log("Создать объект: UpdateChecks");
    }

    public void Dispose()
    {
        _pauseHandler.Remove(this);
    }

    public void Tick()
    {
        if(Work == false)
            return;
        
        if (_isPause == true)
            return;
        
        _timeUpdateCheck += Time.deltaTime;
        if (_checksManager.Check1 == null && _timeUpdateCheck >= _timeAddNewCheck) 
        {
            _checksManager.AddCheck();
            _timeAddNewCheck = 10f;
            _timeUpdateCheck = 0f;
            return;
            //Debug.Log("добавил 1 чек");
        }
        
        if (_checksManager.Check2 == null && _timeUpdateCheck >= _timeAddNewCheck)
        {
            _checksManager.AddCheck();
            _timeAddNewCheck = 15f;
            _timeUpdateCheck = 0f;
            return;
            //Debug.Log("добавил 2 чек");
        }
        
        if (_checksManager.Check3 == null && _timeUpdateCheck >= _timeAddNewCheck)
        {
            _checksManager.AddCheck();
            _timeUpdateCheck = 0f;
            return;
            //Debug.Log("добавил 3 чек");
        }
        
        if(_checksManager.Check1 != null && _checksManager.Check2 != null && _checksManager.Check3 != null)
        {
            _timeUpdateCheck = 0f;
            _timeAddNewCheck = 5f;
            return;
        }
    }

    public void SetPause(bool isPaused) => _isPause = isPaused;
}
