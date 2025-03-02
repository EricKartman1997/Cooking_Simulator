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

    private Transform _parentIngredients;
    private Transform _parentResults;

    public void Initialize(Transform firstPoint,Transform secondPoint,Transform thirdPoint,Transform firstPointResult,Transform secondPointResult,Transform thirdPointResult,Transform parentIngredients,Transform parentResults)
    {
        _firstPointIngredient = firstPoint;
        _secondPointIngredient = secondPoint;
        _thirdPointIngredient = thirdPoint;
        _firstPointResult = firstPointResult;
        _secondPointResult = secondPointResult;
        _thirdPointResult = thirdPointResult;
        _parentIngredients = parentIngredients;
        _parentResults = parentResults;
    }

    public Vector3 GetFirstPointIngredient()
    {
        return _firstPointIngredient.transform.position;
    }
    public Vector3 GetSecondPointIngredient()
    {
        return _secondPointIngredient.transform.position;
    }
    public Vector3 GetThirdPointIngredient()
    {
        return _thirdPointIngredient.transform.position;
    }
    public Vector3 GetFirstPointResult()
    {
        return _thirdPointResult.transform.position;
    }
    public Vector3 GetSecondPointResult()
    {
        return _secondPointResult.transform.position;
    }
    public Vector3 GetThirdPointResult()
    {
        return _firstPointResult.transform.position;
    }
    public Transform GetParentIngredients()
    {
        return _parentIngredients;
    }
    public Transform GetParentResults()
    {
        return _parentResults;
    }
}
