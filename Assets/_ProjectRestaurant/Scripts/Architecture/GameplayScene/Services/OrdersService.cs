using System;
using UnityEngine;

public class OrdersService : IDisposable
{
    public event Action GameOver;

    private byte _totalOrder; // всего заказов в игре
    private byte _makeOrders; // сколько сделано заказов
    
    private GamePlaySceneSettings _settings;
    private OrdersUI _ordersUI;
    private FactoryUIGameplay _factoryUIGameplay;
    
    public OrdersService(GamePlaySceneSettings settings, FactoryUIGameplay factoryUIGameplay)
    {
        _settings = settings;
        _factoryUIGameplay = factoryUIGameplay;
        
        CreateOrders();
    }

    public void Dispose()
    {
        //Debug.Log("У объекта вызван Dispose : OrdersService");
    }

    public void Init(bool isTutorialLevel = false)
    {
        _ordersUI = _factoryUIGameplay.OrdersUI;
        if (isTutorialLevel)
        {
            CreateOrdersTutorial();
        }
    }
    
    private void CreateOrdersTutorial()
    {
        _totalOrder = 1;
        _makeOrders = 0; // Сбрасываем счетчик выполненных заказов
    }
    
    
    private void CreateOrders()
    {
        _totalOrder = _settings.Orders;
        _makeOrders = 0; // Сбрасываем счетчик выполненных заказов
    }

    public void AddOrder()
    {
        Debug.Log("+ заказ");
        ++_makeOrders;
        UpdateOrder();
    }
    
    public void UpdateOrder()
    {
        if(_ordersUI == null) Debug.LogError("Ошибка OrderUI");
        if (_makeOrders >= _totalOrder)
        {
            GameOver?.Invoke();
            Debug.Log("Заказы сделаны");
        }
        _ordersUI.Show();
        _ordersUI.UpdateOrders(_makeOrders, _totalOrder);
    }
}

