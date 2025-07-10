using TMPro;
using UnityEngine;

public class OrdersUI : MonoBehaviour
{
    private Orders _orders;
    [SerializeField] private TextMeshProUGUI scoretext;
    void Start()
    {
        _orders = GetComponent<Orders>();
        UpdateOrders();
    }

    public void UpdateOrders()
    {
        scoretext.text = $"Заказы: {_orders.GetMakeOrders()}/{_orders.GetTotalOrder()}";
        if (_orders.GetMakeOrders() == _orders.GetTotalOrder())
        {
            EventBus.GameOver.Invoke();
            Debug.Log("Сработал GameOver в UpdateOrders");
        }
    }

    private void OnEnable()
    {
        EventBus.UpdateOrder += UpdateOrders;
    }

    private void OnDisable()
    {
        EventBus.UpdateOrder -= UpdateOrders;
    }
}
