using System;
using TMPro;
using UnityEngine;

public class OrdersUI : IDisposable
{
    private Orders _orders;
    private TextMeshProUGUI _scoretext;

    public OrdersUI(Orders orders, TextMeshProUGUI scoretext)
    {
        _orders = orders;
        _scoretext = scoretext;
        
        EventBus.UpdateOrder += UpdateOrders;
        UpdateOrders();
        Debug.Log("Создать объект: OrdersUI");
    }

    public void Dispose()
    {
        EventBus.UpdateOrder -= UpdateOrders;
        _orders?.Dispose();
        Debug.Log("У объекта вызван Dispose : OrdersUI");
    }
    // void Start()
    // {
    //     _orders = GetComponent<Orders>();
    //     UpdateOrders();
    // }

    private void UpdateOrders()
    {
        _scoretext.text = $"Заказы: {_orders.GetMakeOrders()}/{_orders.GetTotalOrder()}";
        if (_orders.GetMakeOrders() == _orders.GetTotalOrder())
        {
            EventBus.GameOver.Invoke();
            Debug.Log("Сработал GameOver в UpdateOrders");
        }
    }

    // private void OnEnable()
    // {
    //     EventBus.UpdateOrder += UpdateOrders;
    // }
    //
    // private void OnDisable()
    // {
    //     EventBus.UpdateOrder -= UpdateOrders;
    // }


}
