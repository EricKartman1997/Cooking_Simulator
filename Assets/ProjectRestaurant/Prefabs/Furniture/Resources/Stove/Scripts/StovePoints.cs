using System;
using UnityEngine;

public class StovePoints: IDisposable
{
    private Transform _positionRawFood;
    private Transform _parentRawFood;
    
    public Transform ParentRawFood => _parentRawFood;

    public Transform PositionRawFood => _positionRawFood;
    
    public StovePoints(Transform positionRawFood, Transform parentRawFood)
    {
        _positionRawFood = positionRawFood;
        _parentRawFood = parentRawFood;
        
        Debug.Log("Создал объект: StovePoints");
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : StovePoints");
    }
}
