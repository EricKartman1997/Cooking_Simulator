using System;
using UnityEngine;

public class OrdersService
{
    public event Action GameOver;

    private byte _totalOrder; 
    private byte _makeOrders;
    
    private GamePlaySceneSettings _settings;
    private OrdersUI _ordersUI;
    private FactoryUIGameplay _factoryUIGameplay;
    
    public OrdersService(GamePlaySceneSettings settings, FactoryUIGameplay factoryUIGameplay)
    {
        _settings = settings;
        _factoryUIGameplay = factoryUIGameplay;
        
        CreateOrders();
    }

    public void Init(bool isTutorialLevel = false)
    {
        _ordersUI = _factoryUIGameplay.OrdersUI;
        if (isTutorialLevel)
        {
            CreateOrdersTutorial();
            UpdateOrderTraining();
        }
    }
    
    private void CreateOrdersTutorial()
    {
        _totalOrder = 1;
        _makeOrders = 0; 
    }
    
    
    private void CreateOrders()
    {
        _totalOrder = _settings.Orders;
        _makeOrders = 0;
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
    
    private void UpdateOrderTraining()
    {
        if(_ordersUI == null) Debug.LogError("Ошибка OrderUI");
        _ordersUI.UpdateOrders(_makeOrders, _totalOrder);
        if (_makeOrders >= _totalOrder)
        {
            GameOver?.Invoke();
            Debug.Log("Заказы сделаны");
        }
    }
}

