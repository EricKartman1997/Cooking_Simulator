using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Orders : IDisposable
{
    private byte _totalOrder; // всего заказов в игре
    private byte _makeOrders; // сколько сделано заказов
    private byte _stayedOrders; // осталось сделать заказов

    public Orders()
    {
        EventBus.AddOrder += AddMakeOrder;
        CreateOrders();
        Debug.Log("Создать объект: Orders");
    }

    public void Dispose()
    {
        EventBus.AddOrder -= AddMakeOrder;
        Debug.Log("У объекта вызван Dispose : Orders");
    }
    // private void Awake()
    // {
    //     CreateOrders();
    // }
    //
    // private void OnEnable()
    // {
    //     EventBus.AddOrder += AddMakeOrder;
    // }
    //
    // private void OnDisable()
    // {
    //     EventBus.AddOrder -= AddMakeOrder;
    // }
    
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

