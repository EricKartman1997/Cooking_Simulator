using System;
using UnityEngine;

public class UpdateChecks : IDisposable
{
    private Checks _checks;
    private float _timeAddNewCheck = 3f;
    private float _timeUpdateCheck;

    public UpdateChecks(Checks checks, float timeAddNewCheck)
    {
        _checks = checks;
        _timeAddNewCheck = timeAddNewCheck;
        
        Debug.Log("Создать объект: UpdateChecks");
    }

    public void Dispose()
    {
        _checks?.Dispose();
    }
    // private void Start()
    // {
    //     _checks = GetComponent<Checks>();
    // }

    public void Update()
    {
        _timeUpdateCheck += Time.deltaTime;
        if (_checks.GetCheck1() == null && _timeUpdateCheck >= _timeAddNewCheck) 
        {
            _checks.AddCheck();
            _timeAddNewCheck = 10f;
            _timeUpdateCheck = 0f;
            //Debug.Log("добавил 1 чек");
        }
        else if (_checks.GetCheck2() == null && _timeUpdateCheck >= _timeAddNewCheck)
        {
            _checks.AddCheck();
            _timeAddNewCheck = 15f;
            _timeUpdateCheck = 0f;
            //Debug.Log("добавил 2 чек");
        }
        else if (_checks.GetCheck3() == null && _timeUpdateCheck >= _timeAddNewCheck)
        {
            _checks.AddCheck();
            _timeUpdateCheck = 0f;
            //Debug.Log("добавил 3 чек");
        }
        else if(_checks.GetCheck1() != null && _checks.GetCheck2() != null && _checks.GetCheck3() != null)
        {
            _timeUpdateCheck = 0f;
            _timeAddNewCheck = 5f;
        }
    }
    
}
