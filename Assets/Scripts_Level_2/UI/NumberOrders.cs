using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberOrders : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI scoretext;
    void Start()
    {
        scoretext.text = $"Orders: {Heroik.MakeOrders}/{Level.NumberOrdersInLevel}";
    }

    public void UpdateOrders()
    {
        scoretext.text = $"Orders: {Heroik.MakeOrders}/{Level.NumberOrdersInLevel}";
    }


}
