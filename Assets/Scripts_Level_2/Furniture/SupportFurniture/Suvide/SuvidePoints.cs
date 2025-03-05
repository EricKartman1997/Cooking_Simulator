using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuvidePoints : MonoBehaviour
{
    private Transform _firstPointIngredient;
    private Transform _secondPointIngredient;
    private Transform _thirdPointIngredient;
    
    private Transform _firstPointResult;
    private Transform _secondPointResult;
    private Transform _thirdPointResult;

    //private Transform _parentIngredients;
    //private Transform _parentResults;

    public void Initialize(Transform firstPoint,Transform secondPoint,Transform thirdPoint,Transform firstPointResult,Transform secondPointResult,Transform thirdPointResult,Transform parentIngredients,Transform parentResults)
    {
        _firstPointIngredient = firstPoint;
        _secondPointIngredient = secondPoint;
        _thirdPointIngredient = thirdPoint;
        _firstPointResult = firstPointResult;
        _secondPointResult = secondPointResult;
        _thirdPointResult = thirdPointResult;
        //_parentIngredients = parentIngredients;
        //_parentResults = parentResults;
    }

    public Transform GetFirstPointIngredient()
    {
        return _firstPointIngredient.transform;
    }
    public Transform GetSecondPointIngredient()
    {
        return _secondPointIngredient.transform;
    }
    public Transform GetThirdPointIngredient()
    {
        return _thirdPointIngredient.transform;
    }
    public Transform GetFirstPointResult()
    {
        return _firstPointResult.transform;
    }
    public Transform GetSecondPointResult()
    {
        return _secondPointResult.transform;
    }
    public Transform GetThirdPointResult()
    {
        return _thirdPointResult.transform;
    }
    // public Transform GetParentIngredients()
    // {
    //     return _parentIngredients;
    // }
    // public Transform GetParentResults()
    // {
    //     return _parentResults;
    // }
}
