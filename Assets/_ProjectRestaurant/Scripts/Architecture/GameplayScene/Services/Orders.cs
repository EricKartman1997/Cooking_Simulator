using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class Orders : IDisposable
{
    public event Action<byte,byte> UpdateOrders;
    public event Action ShowOrders;

    private byte _totalOrder; // всего заказов в игре
    private byte _makeOrders; // сколько сделано заказов
    //private byte _stayedOrders; // осталось сделать заказов
    
    public Orders()
    {
        EventBus.AddOrder += OnAddMakeOrder;
        EventBus.UpdateOrder += OnUpdateOrder;
        
        CreateOrders();
        OnUpdateOrder();
        //Debug.Log("Создать объект: Orders");
    }

    public void Dispose()
    {
        EventBus.AddOrder -= OnAddMakeOrder;
        EventBus.UpdateOrder -= OnUpdateOrder;
        //Debug.Log("У объекта вызван Dispose : Orders");
    }

    public void UpdateOrder()
    {
        ShowOrders?.Invoke();
        UpdateOrders?.Invoke(_makeOrders, _totalOrder);
    }
    
    private void CreateOrders()
    {
        _totalOrder = (byte)Random.Range(3, 5);
        _makeOrders = 0; // Сбрасываем счетчик выполненных заказов
    }

    private void OnAddMakeOrder()
    {
        Debug.Log("+ заказ");
        ++_makeOrders;
        OnUpdateOrder();
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

