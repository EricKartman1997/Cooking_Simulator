using System;
using System.Collections.Generic;
using UnityEngine;
public class Heroik : MonoBehaviour
{
    [SerializeField] private Transform positionObj;
    [SerializeField] private GameObject currentTakenObjects;
    //private bool _isBusyHands = false; // руки не заняты
    private GameManager _gameManager;
    private IngredientHandler _ingredientHandler;
    
    public GameObject CurrentTakenObjects => _ingredientHandler.CurrentTakenObjects;
    public bool IsBusyHands => _ingredientHandler.IsBusyHands;
    
    private void Start()
    {
        _gameManager = StaticManagerWithoutZenject.GameManager;
        _ingredientHandler = new IngredientHandler(positionObj,positionObj,currentTakenObjects,_gameManager);
        _ingredientHandler.CreateObj();
    }
    
    public void TryPickUp(GameObject ingredient) // взять объект в руки
    {
        _ingredientHandler.TryPickUp(ingredient); 
    }
    
    public GameObject TryGiveIngredient() // отдать объект из рук
    {
        return _ingredientHandler.TryGiveIngredient();  
    }
    
    public bool CanGiveIngredient(List<GameObject> forbiddenIngredients) // проверка на отдачу объекта из рук по списку объектов
    {
        return _ingredientHandler.CanGiveIngredient(forbiddenIngredients); 
    }
    
    public bool CanGiveIngredient(List<Type> forbiddenComponents) // проверка на отдачу объекта из рук по компонентам
    {
        return _ingredientHandler.CanGiveIngredient(forbiddenComponents);
    }
    
}
