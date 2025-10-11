using TMPro;
using UnityEngine;

public class OrdersUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ordersText;
    
    public void UpdateOrders(byte makeOrders, byte totalOrders)
    {
        ordersText.text = $"Заказы: {makeOrders}/{totalOrders}";
    }
    
    public void Show() => gameObject.SetActive(true);
    
    public void Hide() => gameObject.SetActive(false);

}
