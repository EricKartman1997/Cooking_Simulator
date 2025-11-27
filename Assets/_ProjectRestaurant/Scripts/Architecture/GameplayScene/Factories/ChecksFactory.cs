using System;
using UnityEngine;
using Zenject;

public class ChecksFactory: IDisposable
{
    private CheckContainer _checkContainer;
    private LoadReleaseGameplay _loadReleaseGameplay; // префабы чеков
    private DiContainer _installer;
    private ChecksManager _checksManager;

    public ChecksFactory(CheckContainer checkContainer, LoadReleaseGameplay loadReleaseGameplay, DiContainer installer,
        ChecksManager checksManager)
    {
        _checkContainer = checkContainer;
        _loadReleaseGameplay = loadReleaseGameplay;
        _installer = installer;
        _checksManager = checksManager;
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : ChecksFactory");
    }
    
    public GameObject GetCheckPrefab(CheckType type, Check check, Transform parent = null)
    {
        CheckConfig config = GetConfig(type);
        GameObject instance = _installer.InstantiatePrefab(config.Prefab, parent);
        CheckUI checkUI = instance.GetComponent<CheckUI>();
        checkUI.Init(check);
        return instance;
    }
    
    public Check GetCheck(CheckType type)
    {
        return GetCheckInstance(type);
    }
    
    private CheckConfig GetConfig(CheckType type)
    {
        switch (type)
        {
            case CheckType.BakedFish:
                return _checkContainer.BakedFish;
            case CheckType.BakedMeat:
                return _checkContainer.BakedMeat;
            case CheckType.BakedSalad:
                return _checkContainer.BakedSalad;
            case CheckType.FruitSalad:
                return _checkContainer.FruitSalad;
            case CheckType.CutletMedium:
                return _checkContainer.CutletMedium;
            case CheckType.WildBerryCocktail:
                return _checkContainer.WildBerryCocktail;
            case CheckType.FreshnessCocktail:
                return _checkContainer.FreshnessCocktail;
            default:
                throw new System.ArgumentException($"Не известный CheckType: {type}");
        }
        
    }
    
    private Check GetCheckInstance(CheckType type)
    {
        CheckConfig config = GetConfig(type);
        switch (type)
        {
            case CheckType.BakedFish:
                return new BakedFishCheck(config.Prefab, config.StartTime, config.Score, config.Dish, _checksManager);
            case CheckType.BakedMeat:
                return new BakedMeatCheck(config.Prefab, config.StartTime, config.Score, config.Dish, _checksManager);
            case CheckType.BakedSalad:
                return new BakedSaladCheck(config.Prefab, config.StartTime, config.Score, config.Dish, _checksManager);
            case CheckType.FruitSalad:
                return new FruitSaladCheck(config.Prefab, config.StartTime, config.Score, config.Dish, _checksManager);
            case CheckType.CutletMedium:
                return new CutletMediumCheck(config.Prefab, config.StartTime, config.Score, config.Dish, _checksManager);
            case CheckType.WildBerryCocktail:
                return new WildBerryCocktailCheck(config.Prefab, config.StartTime, config.Score, config.Dish, _checksManager);
            case CheckType.FreshnessCocktail:
                return new FreshnessCocktailCheck(config.Prefab, config.StartTime, config.Score, config.Dish, _checksManager);
            default:
                throw new System.ArgumentException($"Не известный Check: {type}");
        }
    }
}