using System;
using UnityEngine;
using Zenject;

public abstract class Check: IDisposable, ITickable
{
    private GameObject _prefab;
    private float _startTime;
    private float _score;
    private GameObject _dish;
    private IDeleteOverdueCheck _deleteCheck;

    public bool IsStop;
    
    public GameObject Prefab => _prefab;
    public float StartTime => _startTime;
    public float Score => _score;
    public GameObject Dish => _dish;

    protected Check(GameObject prefab, float startTime, float score, GameObject dish, IDeleteOverdueCheck deleteCheck)
    {
        _prefab = prefab;
        _startTime = startTime;
        _score = score;
        _dish = dish;
        _deleteCheck = deleteCheck;
        //Debug.Log($"Check created: {GetType().Name}, startTime={_startTime}");
    }
    
    public void Dispose()
    {
        
    }
    
    public void Tick()
    {
        //Debug.Log("я работаю 3");
        if (IsStop == true)
            return;
        UpdateTime();
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
    
}
