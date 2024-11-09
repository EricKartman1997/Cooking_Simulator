using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Script : MonoBehaviour
{
    [SerializeField] private string _Name;
    [SerializeField] private int _Age;
    [SerializeField] private List<string> names;
    [SerializeField] private Cars[] cars;
    
    private void Start()
    {
        // Debug.Log($"Привет меня зовут {_Name}, мне {_Age} ");
        // yield return new WaitForSeconds(3);
        // names.Remove("Egor");
        // Debug.Log("Egor очищен из списка!");
        // yield return new WaitForSeconds(3);
        // names.Add("Aleksandar");
        // Debug.Log("Aleksandar добавлен в список!");
        // yield return new WaitForSeconds(3);
        // names.Clear();
        // Debug.Log("Список очищен!");
        foreach (var car in cars)
        {
            car.GetInfo();
        }
    }
}