using System;
using UnityEngine;

public class StovePoints: IDisposable
{
    private Transform _positionRawFood;
    public Transform PositionRawFood => _positionRawFood;
    
    public StovePoints(Transform positionRawFood)
    {
        _positionRawFood = positionRawFood;
        
        Debug.Log("Создал объект: StovePoints");
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : StovePoints");
    }
}
