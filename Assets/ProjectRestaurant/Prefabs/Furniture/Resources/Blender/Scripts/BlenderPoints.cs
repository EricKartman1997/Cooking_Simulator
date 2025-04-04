using System;
using UnityEngine;

public class BlenderPoints : IDisposable
{
    private readonly Transform _firstPoint;
    private readonly Transform _secondPoint;
    private readonly Transform _thirdPoint;
    private readonly Transform _parentFood;
    private readonly Transform _parentReadyFood;

    public BlenderPoints (Transform firstPoint,Transform secondPoint,Transform thirdPoint,Transform parentFood, Transform parentReadyFood)
    {
        _firstPoint = firstPoint;
        _secondPoint = secondPoint;
        _thirdPoint = thirdPoint;
        _parentFood = parentFood;
        _parentReadyFood = parentReadyFood;
        
        Debug.Log("Создал объект: BlenderPoints");
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : BlenderPoints");
    }

    public Transform FirstPoint => _firstPoint;
    public Transform SecondPoint => _secondPoint;
    public Transform ThirdPoint => _thirdPoint;
    public Transform ParentFood => _parentFood;
    public Transform ParentReadyFood => _parentReadyFood;
    
}
