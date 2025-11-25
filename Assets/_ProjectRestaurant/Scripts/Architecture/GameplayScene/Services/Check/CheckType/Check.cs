using System;
using UnityEngine;
using Zenject;

public abstract class Check: IDisposable, ITickable
{
    private GameObject _prefab;
    private float _startTime;
    private float _score;
    private GameObject _dish;
    private IDeleteCheck _deleteCheck;

    public bool IsStop;
    
    
    public GameObject Prefab => _prefab;
    public float StartTime => _startTime;
    public float Score => _score;
    public GameObject Dish => _dish;

    protected Check(GameObject prefab, float startTime, float score, GameObject dish, IDeleteCheck deleteCheck)
    {
        _prefab = prefab;
        _startTime = startTime;
        _score = score;
        _dish = dish;
        _deleteCheck = deleteCheck;
    }
    
    public void Dispose()
    {
        
    }
    
    public void Tick()
    {
        if (IsStop == true)
            return;
        UpdateTime();
    }

    public abstract void Accept(ICheckVisitor checkVisitor);
    
    private void UpdateTime()
    {
        if (_startTime > 0)
        {
            _startTime -= Time.deltaTime;
            return;
        }
        _deleteCheck.DeleteCheck(this);
        Debug.Log("Чек удален");
    }
}
