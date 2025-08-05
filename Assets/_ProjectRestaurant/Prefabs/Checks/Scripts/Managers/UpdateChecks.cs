using System;
using UnityEngine;
using System.Collections;

public class UpdateChecks : IDisposable
{
    public bool IsInit;// переделать
    
    private GameManager _gameManager;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    private Checks _checks;
    private float _timeAddNewCheck = 3f;
    private float _timeUpdateCheck;

    public UpdateChecks(Checks checks, float timeAddNewCheck,CoroutineMonoBehaviour coroutineMonoBehaviour)
    {
        _checks = checks;
        _timeAddNewCheck = timeAddNewCheck;
        _coroutineMonoBehaviour = coroutineMonoBehaviour;

        _coroutineMonoBehaviour.StartCoroutine(Init());
        
        //Debug.Log("Создать объект: UpdateChecks");
    }

    public void Dispose()
    {
        _checks?.Dispose();
    }
    
    private IEnumerator Init()
    {
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        
        // while (_timeGame == null)
        // {
        //     _timeGame = _gameManager.TimeGame;
        //     yield return null;
        // }
        IsInit = true;
        Debug.Log("Создать объект: UpdateChecks");
    }

    public void Update()
    {
        _timeUpdateCheck += Time.deltaTime;
        if (_checks.Check1 == null && _timeUpdateCheck >= _timeAddNewCheck) 
        {
            _checks.AddCheck();
            _timeAddNewCheck = 10f;
            _timeUpdateCheck = 0f;
            return;
            //Debug.Log("добавил 1 чек");
        }
        
        if (_checks.Check2 == null && _timeUpdateCheck >= _timeAddNewCheck)
        {
            _checks.AddCheck();
            _timeAddNewCheck = 15f;
            _timeUpdateCheck = 0f;
            return;
            //Debug.Log("добавил 2 чек");
        }
        
        if (_checks.Check3 == null && _timeUpdateCheck >= _timeAddNewCheck)
        {
            _checks.AddCheck();
            _timeUpdateCheck = 0f;
            return;
            //Debug.Log("добавил 3 чек");
        }
        
        if(_checks.Check1 != null && _checks.Check2 != null && _checks.Check3 != null)
        {
            _timeUpdateCheck = 0f;
            _timeAddNewCheck = 5f;
            return;
        }
    }
    
}
