using System;
using System.Collections.Generic;
using UnityEngine;
public class Heroik : MonoBehaviour
{
    public GameObject _curentTakenObjects = null;// переделать
    public static bool IsBusyHands = false; // руки не заняты
    public static byte MakeOrders = 0;
    public GameObject[] TakenObjects;
    [SerializeField] private bool _interactionFurniture = false;
   

    private void Start()
    {
        foreach (var Obj in TakenObjects)
        {
            Obj.SetActive(false);
        }
    }
    public void ActiveObjHands(GameObject objTable) // взять объект в руки
    {
        foreach (var Obj in TakenObjects)
        {
            if (Obj.name == objTable.name)
            {
                Obj.SetActive(true);
                _curentTakenObjects = Obj;
                IsBusyHands = true;
            }
        }
    }
    public GameObject GiveObjHands() // отдать объект из рук
    {
        _curentTakenObjects.SetActive(false);
        IsBusyHands = false;
        var Obj = _curentTakenObjects;
        _curentTakenObjects = null;
        return Obj;
    }
    
    public bool CheckObjForReturn(List<GameObject> _unusableObjects) // отдать объект из рук с проверкой
    {
        List<string> _unusableObjectsNames = new List<string>();
        foreach (var food in _unusableObjects)
        {
            _unusableObjectsNames.Add(food.name); // Используем имя объекта
        }
        if (_unusableObjectsNames.Contains(_curentTakenObjects.name))
        {
            Debug.Log("Сработал false");
            return false;
        }
        return true;
    }
    
    public GameObject GetCurentTakenObjects()
    {
        return _curentTakenObjects;
    }
    // public void SetInteractionFurnitureTrue()
    // {
    //     _interactionFurniture = true;
    // }
    // public void SetInteractionFurnitureFalse()
    // {
    //     _interactionFurniture = false;
    // }

    public bool GetInteractionFurniture()
    {
        return _interactionFurniture;
    }

    // private void OnEnable()
    // {
    //     EventBus.NotPressE += SetInteractionFurnitureFalse;
    //     EventBus.PressE += SetInteractionFurnitureTrue;
    // }
    //
    // private void OnDisable()
    // {
    //     EventBus.NotPressE -= SetInteractionFurnitureFalse;
    //     EventBus.PressE -= SetInteractionFurnitureTrue;
    // }
    
}
