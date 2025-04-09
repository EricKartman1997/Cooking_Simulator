using System;
using System.Collections.Generic;
using UnityEngine;
public class Heroik : MonoBehaviour
{
    [SerializeField] private Transform positionObj;
    [SerializeField] private Transform parentObj;
    
    private GameObject _currentTakenObjects;
    private bool _isBusyHands = false; // руки не заняты
    
    public GameObject CurrentTakenObjects => _currentTakenObjects;
    
    public bool IsBusyHands => _isBusyHands;

    private void Start()
    {
        CreateObj();
    }
    public void ActiveObjHands(GameObject objTable) // взять объект в руки
    {
        if (_isBusyHands == true)
        {
            Debug.LogWarning("Руки заняты");
            return;
        }
        
        _currentTakenObjects = objTable;
        CreateObj();
        Destroy(objTable); // надо переделать
    }
    
    public GameObject GiveObjHands() // отдать объект из рук
    {
        if (_isBusyHands == true)
        {
            Debug.LogWarning("Руки пустые");
            return null;
        }
        
        _isBusyHands = false;
        return _currentTakenObjects;
    }
    
    public bool CheckObjForReturn(List<GameObject> _unusableObjects) // проверка на отдачу объекта из рук по списку объектов
    {
        List<string> _unusableObjectsNames = new List<string>();
        foreach (var food in _unusableObjects)
        {
            _unusableObjectsNames.Add(food.name); // Используем имя объекта
        }
        if (_unusableObjectsNames.Contains(_currentTakenObjects.name))
        {
            Debug.Log("Сработал false");
            return false;
        }
        return true;
    }
    
    public bool CheckObjForReturn(List<Type> _unusableObjects) // проверка на отдачу объекта из рук по компонентам
    {
        // Получаем все компоненты на объекте
        Component[] components = _currentTakenObjects.GetComponents<Component>();
        byte count = 0;

        // Проверяем каждый компонент
        foreach (Component component in components)
        {
            // Если тип компонента содержится в списке _unusableObjects
            if (_unusableObjects.Contains(component.GetType()))
            {
                count++;
                if (count == _unusableObjects.Count)
                {
                    //Debug.Log("На объекте найдены все компоненты");
                    return true;
                }
            }
        }

        // разрешенных компонентов не найдено
        Debug.Log("Продукт нельзя передать.");
        return false;
    }

    private void CreateObj()
    {
        if (_currentTakenObjects == null)
            return;
        
        _currentTakenObjects = StaticManagerWithoutZenject.ProductsFactory.GetProduct(_currentTakenObjects, positionObj, parentObj, true);
        _isBusyHands = true;
    }
}
