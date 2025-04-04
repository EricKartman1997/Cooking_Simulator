using System;
using UnityEngine;

public class CuttingTablePoints : IDisposable
{
    private Transform _positionIngredient1; 
    private Transform _positionIngredient2; 
    private Transform _parentIngredient;    
    private Transform _positionResult;      
    private Transform _parentResult;

    public CuttingTablePoints(Transform positionIngredient1, Transform positionIngredient2, Transform parentIngredient, Transform positionResult, Transform parentResult)
    {
        _positionIngredient1 = positionIngredient1;
        _positionIngredient2 = positionIngredient2;
        _parentIngredient = parentIngredient;
        _positionResult = positionResult;
        _parentResult = parentResult;
        
        Debug.Log("Создать объект: CuttingTablePoints");
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : CuttingTablePoints");
    }

    public Transform PositionIngredient1 => _positionIngredient1;

    public Transform PositionIngredient2 => _positionIngredient2;

    public Transform ParentIngredient => _parentIngredient;

    public Transform PositionResult => _positionResult;

    public Transform ParentResult => _parentResult;
}
