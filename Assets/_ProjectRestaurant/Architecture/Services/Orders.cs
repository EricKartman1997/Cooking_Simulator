using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class Orders : IDisposable
{
    public event Action<byte,byte> UpdateOrders;
    public event Action ShowOrders;
    
    private GameManager _gameManager;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    private byte _totalOrder; // всего заказов в игре
    private byte _makeOrders; // сколько сделано заказов
    private byte _stayedOrders; // осталось сделать заказов
    private bool _isInit;

    public bool IsInit => _isInit;

    public Orders(CoroutineMonoBehaviour coroutineMonoBehaviour)
    {
        _coroutineMonoBehaviour = coroutineMonoBehaviour;
        EventBus.AddOrder += OnAddMakeOrder;
        EventBus.UpdateOrder += OnUpdateOrder;
        
        _coroutineMonoBehaviour.StartCoroutine(Init());
        CreateOrders();
        //Debug.Log("Создать объект: Orders");
    }

    public void Dispose()
    {
        EventBus.AddOrder -= OnAddMakeOrder;
        EventBus.UpdateOrder -= OnUpdateOrder;
        Debug.Log("У объекта вызван Dispose : Orders");
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
        
        Debug.Log("Создать объект: Orders");
        _isInit = true;
    }
    
    private void CreateOrders()
    {
        _totalOrder = (byte)Random.Range(3, 5);
        _makeOrders = 0; // Сбрасываем счетчик выполненных заказов
    }

    private void OnAddMakeOrder()
    {
        OnUpdateOrder();
        ++_makeOrders;
    }
    
    private void OnUpdateOrder()
    {
        if (_makeOrders >= _totalOrder)
        {
            EventBus.GameOver.Invoke();
            Debug.Log("Заказы сделаны");
        }
        ShowOrders?.Invoke();
        UpdateOrders?.Invoke(_makeOrders, _totalOrder);
    }
}

