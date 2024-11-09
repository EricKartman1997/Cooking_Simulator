using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{
    [SerializeField] private string _carID;
    [SerializeField] private string _carModel;
    [SerializeField] private string _carMarka;
    [SerializeField] private string _carHours;

    public void GetInfo()
    {
        Debug.Log($"Id транспортного средства: {_carID}\n" +
                  $"Модель транспортного средства: {_carModel}\n" +
                  $"Марка транспортного средства: {_carMarka}\n" +
                  $"Часы работы транспортного средства: {_carHours}\n");
    }
}
