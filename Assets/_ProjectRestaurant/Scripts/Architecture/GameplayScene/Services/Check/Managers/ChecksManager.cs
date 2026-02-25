using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

public class ChecksManager :ITickable, IDeleteCheck, IDeleteOverdueCheck, IAddCheck, ICheckTheCheck
{
    private CheckFactory _checkFactoryScript;
    private CheckPrefabFactory _checkPrefabFactory;
    private OrdersService _ordersService;
    private ScoreService _scoreService;
    private ChecksPanalUI _checksPanalUI;
    private FactoryUIGameplay _factoryUIGameplay;

    private Check _check1;
    private Check _check2;
    private Check _check3;

    private List<CheckType> _dishList;

    public Check Check1 => _check1;

    public Check Check2 => _check2;

    public Check Check3 => _check3;
    
    public ChecksManager(CheckFactory checksFactory,CheckPrefabFactory checkPrefabFactory,GamePlaySceneSettings settings,
        OrdersService ordersService,ScoreService scoreService,FactoryUIGameplay factoryUIGameplay)//,BootstrapGameplay bootstrapGameplay
    {
        _dishList = settings.DishList;
        _checkFactoryScript = checksFactory;
        _checkPrefabFactory = checkPrefabFactory;
        _ordersService = ordersService;
        _scoreService = scoreService;
        _factoryUIGameplay = factoryUIGameplay;
    }

    public void Init()
    {
        _checksPanalUI = _factoryUIGameplay.ChecksPanalUI;
    }
    
    public void Tick()
    {
        TickChecks();
    }
    
    public void AddCheck() // добавление чека
    {
        CheckType type = _dishList[Random.Range(0, _dishList.Count)];
        if (_check1 == null)
        {
            _check1 = _checkFactoryScript.Create(type);
            _checksPanalUI.AddCheck(_check1, _checkPrefabFactory, type);
        }
        else if (_check2 == null)
        {
            _check2 = _checkFactoryScript.Create(type);
            _checksPanalUI.AddCheck(_check2, _checkPrefabFactory, type);
        }
        else if (_check3 == null)
        {
            _check3 = _checkFactoryScript.Create(type);
            _checksPanalUI.AddCheck(_check3, _checkPrefabFactory, type);
        }
        else
        {
            throw new InvalidOperationException("Невозможно добавить чек: все слоты заняты");
        }
    }

    public void AddCheckTutorial() // добавление чека
    {
        CheckType type = CheckType.FruitSalad;
        if (_check1 == null)
        {
            _check1 = _checkFactoryScript.Create(type);
            _checksPanalUI.AddCheck(_check1, _checkPrefabFactory, type);
            _check1.IsStop = true;
            _check1.ChangeTimeForTutorial();
        }
       
    }
    
    public void DeleteCheck(Check check) // удаление чека
    {
        if (_check1 == check)
        {
            _scoreService.AddScore(0,_check1);
            _checksPanalUI.RemoveCheck(_check1);
            _check1.Dispose();
            _check1 = null;
            _ordersService.AddOrder();
            _ordersService.UpdateOrder();
            return;
        }
        
        if (_check2 == check)
        {
            _scoreService.AddScore(0,_check2);
            _checksPanalUI.RemoveCheck(_check2);
            _check2.Dispose();
            _check2 = null;
            _ordersService.AddOrder();
            _ordersService.UpdateOrder();
            return;
        }
        
        if (_check3 == check)
        {
            _scoreService.AddScore(0,_check3);
            _checksPanalUI.RemoveCheck(_check3);
            _check3.Dispose();
            _check3 = null;
            _ordersService.AddOrder();
            _ordersService.UpdateOrder();
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
        
        Product[] products1 = GetSingleProductComponent(obj1);
        Product[] products2 = GetSingleProductComponent(obj2);
        
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
    
    public void DeleteOverdueCheck(Check check) // удаление просроченного чека
    {
        if (_check1 != null && _check1.StartTime <= 0f)
        {
            _checksPanalUI.RemoveCheck(_check1);
            _check1 = null;
        }
        else if (_check2 != null && _check2.StartTime <= 0f)
        {
            _checksPanalUI.RemoveCheck(_check2);
            _check2 = null;
        }
        else if (_check3 != null && _check3.StartTime <= 0f)
        {
            _checksPanalUI.RemoveCheck(_check3);
            _check3 = null;
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