using TMPro;
using UnityEngine;

public class NumberOrders : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI scoretext;
    void Start()
    {
        scoretext.text = $"Заказы: {Heroik.MakeOrders}/{Level.NumberOrdersInLevel}";
    }

    public void UpdateOrders()
    {
        scoretext.text = $"Заказы: {Heroik.MakeOrders}/{Level.NumberOrdersInLevel}";
    }
}
