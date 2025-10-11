using System;

public class OrdersMediator: IDisposable
{
    private OrdersUI _ordersUI;
    private Orders _orders;

    public OrdersMediator(Orders orders, OrdersUI ordersUI)
    {
        _ordersUI = ordersUI;
        _orders = orders;
        _orders.UpdateOrders += OnUpdateOrders;
        _orders.ShowOrders += OnShow;
    }

    public void Dispose()
    {
        _orders.UpdateOrders -= OnUpdateOrders;
        _orders.ShowOrders -= OnShow;
    }
    
    private void OnShow()
    {
        _ordersUI.Show();
    }
    
    private void OnHide()
    {
        _ordersUI.Hide();
    }

    private void OnUpdateOrders(byte makeOrders, byte totalOrders)
    {
        _ordersUI.UpdateOrders(makeOrders,totalOrders);
    }
}