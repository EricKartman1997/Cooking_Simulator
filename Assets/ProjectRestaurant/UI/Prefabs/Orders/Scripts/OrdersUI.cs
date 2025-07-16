using System;
using TMPro;
using UnityEngine;
using System.Collections;

public class OrdersUI : IDisposable
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    
    private Orders _orders;
    
    private TextMeshProUGUI _scoretext;

    public OrdersUI(Orders orders, CoroutineMonoBehaviour coroutineMonoBehaviour)
    {
        _orders = orders;
        _coroutineMonoBehaviour = coroutineMonoBehaviour;
        EventBus.UpdateOrder += UpdateOrders;

        _coroutineMonoBehaviour.StartCoroutine(Init());
        
        //Debug.Log("Создать объект: OrdersUI");
    }

    public void Dispose()
    {
        EventBus.UpdateOrder -= UpdateOrders;
        _orders?.Dispose();
        Debug.Log("У объекта вызван Dispose : OrdersUI");
    }
    
    private IEnumerator Init()
    {
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        
        while (_uiManager == null)
        {
            _uiManager = _gameManager.UIManager;
            yield return null;
        }

        _scoretext = _uiManager.ScoreText;
        
        UpdateOrders();
        Debug.Log("Создать объект: OrdersUI");
    }

    private void UpdateOrders()
    {
        _scoretext.text = $"Заказы: {_orders.GetMakeOrders()}/{_orders.GetTotalOrder()}";
        if (_orders.GetMakeOrders() == _orders.GetTotalOrder())
        {
            EventBus.GameOver.Invoke();
            Debug.Log("Сработал GameOver в UpdateOrders");
        }
    }

}
