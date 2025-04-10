using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StovePoints: IDisposable
{
    private Transform _positionRawFood;
    private Transform _parentRawFood;
    private Transform _positionReadyFood;
    private Transform _parentReadyFood;

    public Transform ParentReadyFood => _parentReadyFood;

    public Transform PositionReadyFood => _positionReadyFood;

    public Transform ParentRawFood => _parentRawFood;

    public Transform PositionRawFood => _positionRawFood;
    
    public StovePoints(Transform positionRawFood, Transform parentRawFood, Transform positionReadyFood, Transform parentReadyFood)
    {
        _positionRawFood = positionRawFood;
        _parentRawFood = parentRawFood;
        _positionReadyFood = positionReadyFood;
        _parentReadyFood = parentReadyFood;
        
        Debug.Log("Создал объект: StovePoints");
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : StovePoints");
    }
}
