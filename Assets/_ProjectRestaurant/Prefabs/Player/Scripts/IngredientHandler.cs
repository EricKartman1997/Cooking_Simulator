using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Linq;

public class IngredientHandler: IDisposable
{
    private GameManager _gameManager;
    private Transform _ingredientPosition;
    private Transform _ingredientParent;
    private GameObject _currentTakenObjects;
    private bool _isBusyHands;
    
    public GameObject CurrentTakenObjects => _currentTakenObjects;
    public bool IsBusyHands => _isBusyHands;
    
    public IngredientHandler(Transform ingredientPosition, Transform ingredientParent, GameObject currentTakenObjects,MonoBehaviour monoBehaviour)
    {
        _ingredientPosition = ingredientPosition;
        _ingredientParent = ingredientParent;
        _currentTakenObjects = currentTakenObjects;
        
        monoBehaviour.StartCoroutine(Init());
        
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : IngredientHandler");
    }

    private IEnumerator Init()
    {
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        Debug.Log("Создан объект: IngredientHandler");
    }

    // взять объект в руки
    public bool TryPickUp(GameObject objTable) 
    {
        if (_isBusyHands == true)
        {
            Debug.LogWarning("Руки заняты");
            return false;
        }
        
        if (_currentTakenObjects != null)
        {
            Debug.LogWarning("Руки заняты");
            return false;
        }
        
        if (objTable == null)
        {
            Debug.LogWarning("Принимаемый объект пуст");
            return false;
        }
        
        CreateObj(objTable);
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
        if (_currentTakenObjects == null)
        {
            Debug.LogWarning("Объект в руках почему-то пуст");
            return null;
        }
        
        _isBusyHands = false;
        //GameObject copy = Object.Instantiate(_currentTakenObjects);
        //Object.Destroy(_currentTakenObjects);
        //_currentTakenObjects = null;
        //return copy;
        return _currentTakenObjects;
    }
    
    public bool CanGiveIngredient(List<Product> products)
    {
        // Получаем все компоненты Product на блюде
        var dishProducts = _currentTakenObjects.GetComponents<Product>();
        
        // Получаем уникальные типы из списка продуктов
        var validTypes = products
            .Select(p => p.GetType())
            .Distinct()
            .ToHashSet();

        // Считаем количество совпадений
        int matchCount = 0;
        foreach (var dishProduct in dishProducts)
        {
            if (validTypes.Contains(dishProduct.GetType()))
            {
                matchCount++;
            }
        }

        return matchCount == 1;
    }
    
    public void CleanObjOnHands()
    {
        Object.Destroy(_currentTakenObjects);
    }
    
    private void CreateObj(GameObject obj)
    {
        _currentTakenObjects = _gameManager.ProductsFactory.GetProduct(obj, _ingredientPosition, _ingredientParent, true);
        _isBusyHands = true;
    }
}  