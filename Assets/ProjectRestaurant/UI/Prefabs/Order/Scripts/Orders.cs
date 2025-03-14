using UnityEngine;
using Random = UnityEngine.Random;

public class Orders : MonoBehaviour
{
    private byte _totalOrder; // всего заказов в игре
    private byte _makeOrders; // сколько сделано заказов
    private byte _stayedOrders; // осталось сделать заказов

    public int StayedOrders
    {
        get
        {
            _stayedOrders = (byte)(_totalOrder - _makeOrders); // Вычисляем оставшиеся заказы
            return _stayedOrders;
        }
    }
    private void Awake()
    {
        CreateOrders();
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

    public int GetMakeOrders()
    {
        return _makeOrders;
    }
    
    public int GetTotalOrder()
    {
        return _totalOrder;
    }
    
    private void OnEnable()
    {
        EventBus.AddOrder += AddMakeOrder;
    }

    private void OnDisable()
    {
        EventBus.AddOrder -= AddMakeOrder;
    }
}

