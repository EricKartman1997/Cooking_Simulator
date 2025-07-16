using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class Orders : IDisposable
{
    private GameManager _gameManager;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    private byte _totalOrder; // всего заказов в игре
    private byte _makeOrders; // сколько сделано заказов
    private byte _stayedOrders; // осталось сделать заказов

    public Orders(CoroutineMonoBehaviour coroutineMonoBehaviour)
    {
        _coroutineMonoBehaviour = coroutineMonoBehaviour;
        EventBus.AddOrder += AddMakeOrder;
        
        _coroutineMonoBehaviour.StartCoroutine(Init());
        CreateOrders();
        //Debug.Log("Создать объект: Orders");
    }

    public void Dispose()
    {
        EventBus.AddOrder -= AddMakeOrder;
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
    }
    
    public int GetMakeOrders()
    {
        return _makeOrders;
    }
    
    public int GetTotalOrder()
    {
        return _totalOrder;
    }
    private void CreateOrders()
    {
        _totalOrder = (byte)Random.Range(3, 5);
        _makeOrders = 0; // Сбрасываем счетчик выполненных заказов
    }

    private void AddMakeOrder()
    {
        ++_makeOrders;
    }
}

