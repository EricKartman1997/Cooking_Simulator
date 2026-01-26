using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

public class ChecksManager :ITickable, IDeleteCheck, IDeleteOverdueCheck, IAddCheck, IActionCheck, ICheckTheCheck
{
    public event Action<Check, CheckPrefabFactory, CheckType> AddCheckAction;
    public event Action<Check> RemoveCheckAction;
    
    private CheckFactory _checkFactoryScript;
    private CheckPrefabFactory _checkPrefabFactory;

    private Check _check1;
    private Check _check2;
    private Check _check3;

    private List<CheckType> _dishList;

    public Check Check1 => _check1;

    public Check Check2 => _check2;

    public Check Check3 => _check3;
    
    public ChecksManager(CheckFactory checksFactory,CheckPrefabFactory checkPrefabFactory)
    {
        _checkFactoryScript = checksFactory;
        _checkPrefabFactory = checkPrefabFactory;
        //EventBus.DeleteCheck += DeleteOverdueCheck;

        //Debug.Log("Создать объект: ChecksManager");
    }
    
    public void Tick()
    {
        TickChecks();
    }

    public void AddCheck() // добавление чека
    {
        //System.Enum.GetValues(typeof(CheckType)).Length
        //CheckType type = (CheckType)Random.Range(0, 0); // поменять
        CheckType type = _dishList[Random.Range(0, _dishList.Count)]; // правильно
        if (_check1 == null)
        {
            _check1 = _checkFactoryScript.Create(type);
            AddCheckAction?.Invoke(_check1, _checkPrefabFactory, type);
        }
        else if (_check2 == null)
        {
            _check2 = _checkFactoryScript.Create(type);
            AddCheckAction?.Invoke(_check2, _checkPrefabFactory, type);
        }
        else if (_check3 == null)
        {
            _check3 = _checkFactoryScript.Create(type);
            AddCheckAction?.Invoke(_check3, _checkPrefabFactory, type);
        }
        else
        {
            throw new InvalidOperationException("Невозможно добавить чек: все слоты заняты");
        }
    }
    
    public void DeleteCheck(Check check) // удаление чека
    {
        if (_check1 == check)
        {
            EventBus.AddScore.Invoke(0,_check1);
            RemoveCheckAction?.Invoke(_check1);
            _check1.Dispose();
            _check1 = null;
            EventBus.AddOrder.Invoke();
            EventBus.UpdateOrder.Invoke();
            return;
        }
        
        if (_check2 == check)
        {
            EventBus.AddScore.Invoke(0,_check2);
            RemoveCheckAction?.Invoke(_check2);
            _check2.Dispose();
            _check2 = null;
            EventBus.AddOrder.Invoke();
            EventBus.UpdateOrder.Invoke();
            return;
        }
        
        if (_check3 == check)
        {
            EventBus.AddScore.Invoke(0,_check3);
            RemoveCheckAction?.Invoke(_check3);
            _check3.Dispose();
            _check3 = null;
            EventBus.AddOrder.Invoke();
            EventBus.UpdateOrder.Invoke();
            return;
        }
        
        throw new ArgumentException($"Такого чека нет: {check}");
        
    }

    public Check CheckTheCheck(GameObject dish) // проверка есть ли блюда в чеках
    {
        List<Check> allChecks = new List<Check>() {_check1,_check2,_check3};
        Check targetCheck = null;
        float minTime = float.MaxValue;

        foreach (var check in allChecks)
        {
            if (check == null)
            {
                continue;
            }

            // Находим чек для текущего блюда с минимальным временем
            if (HaveSameProductComponent(check.Dish,dish) && check.StartTime < minTime)
            {
                minTime = check.StartTime;
                targetCheck = check;
            }
        }

        return targetCheck;
    }
    
    private bool HaveSameProductComponent(GameObject obj1, GameObject obj2)
    {
        if (obj1 == null) 
            throw new ArgumentNullException(nameof(obj1), "Первый объект не может быть null");
        if (obj2 == null) 
            throw new ArgumentNullException(nameof(obj2), "Второй объект не может быть null");

        // Получаем компоненты Product с проверкой количества
        Product[] products1 = GetSingleProductComponent(obj1);
        Product[] products2 = GetSingleProductComponent(obj2);

        // Сравниваем типы единственных компонентов
        return products1[0].GetType() == products2[0].GetType();
    }

    private Product[] GetSingleProductComponent(GameObject obj)
    {
        Product[] products = obj.GetComponents<Product>();
        
        if (products.Length == 0)
            throw new InvalidOperationException(
                $"Объект '{obj.name}' не содержит ни одного компонента типа Product");
        
        if (products.Length > 1)
            throw new InvalidOperationException(
                $"Объект '{obj.name}' содержит {products.Length} компонентов Product. Допустим только один.");
        
        return products;
    }

    public void DeleteAllChecks()
    {
        // Object.Destroy(_cloneCheck1);
        // _check1 = null;
        RemoveCheckAction?.Invoke(_check1);
        //Debug.Log("удалил первый чек");
        
        // Object.Destroy(_cloneCheck2);
        // _check2 = null;
        RemoveCheckAction?.Invoke(_check2);
        //Debug.Log("удалил второй чек");
        
        // Object.Destroy(_cloneCheck3);
        // _check3 = null;
        RemoveCheckAction?.Invoke(_check3);
        //Debug.Log("удалил третий чек");
    }
    
    public void DeleteOverdueCheck(Check check) // удаление просроченного чека
    {
        if (_check1 != null && _check1.StartTime <= 0f)
        {
            // Object.Destroy(_cloneCheck1);
            // _cloneCheck1 = null;
            RemoveCheckAction?.Invoke(_check1);
            _check1 = null;

            //Debug.Log("просрочен 1 чек");
        }
        else if (_check2 != null && _check2.StartTime <= 0f)
        {
            // Object.Destroy(_cloneCheck2);
            // _cloneCheck2 = null;
            RemoveCheckAction?.Invoke(_check2);
            _check2 = null;
            
            //Debug.Log("просрочен 2 чек");

        }
        else if (_check3 != null && _check3.StartTime <= 0f)
        {
            // Object.Destroy(_cloneCheck3);
            // _cloneCheck3 = null;
            RemoveCheckAction?.Invoke(_check3);
            _check3 = null;

            //Debug.Log("просрочен 3 чек");
        }
        else
        {
            throw new Exception("ошибка DeleteOverdueCheck");
        }
        
    }
    
    private void TickChecks()
    {
        _check1?.Tick();
        _check2?.Tick();
        _check3?.Tick();
    }
}

public enum CheckType
{
    BakedFish,
    BakedMeat,
    BakedSalad,
    FruitSalad,
    CutletMedium,
    WildBerryCocktail,
    FreshnessCocktail
}