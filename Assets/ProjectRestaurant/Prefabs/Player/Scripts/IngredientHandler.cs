using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class IngredientHandler: IDisposable
{
    private GameManager _gameManager;
    private Transform _ingredientPosition;
    private Transform _ingredientParent;
    private GameObject _currentTakenObjects;
    private bool _isBusyHands;
    
    public GameObject CurrentTakenObjects => _currentTakenObjects;
    public bool IsBusyHands => _isBusyHands;
    
    public IngredientHandler(Transform ingredientPosition, Transform ingredientParent, GameObject currentTakenObjects,GameManager gameManager)
    {
        _ingredientPosition = ingredientPosition;
        _ingredientParent = ingredientParent;
        _currentTakenObjects = currentTakenObjects;
        _gameManager = gameManager;
        
        Debug.Log("Создан объект: IngredientHandler");
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : IngredientHandler");
    }

    // взять объект в руки
    public bool TryPickUp(GameObject objTable) 
    {
        if (_isBusyHands == true)
        {
            Debug.LogWarning("Руки заняты");
            return false;
        }
        
        if (objTable == null)
        {
            Debug.LogWarning("Принимаемый объект пуст");
            return false;
        }
        
        _currentTakenObjects = objTable;
        CreateObj();
        Object.Destroy(objTable); // надо переделать
        return true;
    }
    
    // отдать объект из рук
    public GameObject TryGiveIngredient() 
    {
        if (_isBusyHands == false)
        {
            Debug.LogWarning("Руки пустые");
            return null;
        }
        
        _isBusyHands = false;
        return _currentTakenObjects;
    }
    
    // проверка на отдачу объекта из рук по списку объектов
    public bool CanGiveIngredient(List<GameObject> unusableObjects) 
    {
        List<string> unusableObjectsNames = new List<string>();
        foreach (var food in unusableObjects)
        {
            unusableObjectsNames.Add(food.name); // Используем имя объекта
        }
        if (unusableObjectsNames.Contains(_currentTakenObjects.name))
        {
            Debug.Log("Сработал false");
            return false;
        }
        return true;
    }
    
    // проверка на отдачу объекта из рук по компонентам
    public bool CanGiveIngredient(List<Type> unusableObjects) 
    {
        // Получаем все компоненты на объекте
        Component[] components = _currentTakenObjects.GetComponents<Component>();
        byte count = 0;

        // Проверяем каждый компонент
        foreach (Component component in components)
        {
            // Если тип компонента содержится в списке _unusableObjects
            if (unusableObjects.Contains(component.GetType()))
            {
                count++;
                if (count == unusableObjects.Count)
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

    public void CreateObj()
    {
        if (_currentTakenObjects == null)
            return;
        
        _currentTakenObjects = _gameManager.ProductsFactory.GetProduct(_currentTakenObjects, _ingredientPosition, _ingredientParent, true);
        _isBusyHands = true;
    }


}  