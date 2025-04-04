using System;
using UnityEngine;

public class SuvidePoints : IDisposable
{
    private Transform _firstPointIngredient;
    private Transform _secondPointIngredient;
    private Transform _thirdPointIngredient;
    
    private Transform _firstPointResult;
    private Transform _secondPointResult;
    private Transform _thirdPointResult;
    
    public Transform FirstPointIngredient => _firstPointIngredient;

    public Transform SecondPointIngredient => _secondPointIngredient;

    public Transform ThirdPointIngredient => _thirdPointIngredient;

    public Transform FirstPointResult => _firstPointResult;

    public Transform SecondPointResult => _secondPointResult;

    public Transform ThirdPointResult => _thirdPointResult;
    
    public SuvidePoints(Transform firstPointIngredient, Transform secondPointIngredient, Transform thirdPointIngredient, Transform firstPointResult, Transform secondPointResult, Transform thirdPointResult)
    {
        _firstPointIngredient = firstPointIngredient;
        _secondPointIngredient = secondPointIngredient;
        _thirdPointIngredient = thirdPointIngredient;
        _firstPointResult = firstPointResult;
        _secondPointResult = secondPointResult;
        _thirdPointResult = thirdPointResult;
        
        Debug.Log("Создал объект: SuvidePoints");
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : SuvidePoints");
    }




}
