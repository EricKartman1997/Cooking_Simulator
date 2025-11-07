using System.Collections.Generic;
using UnityEngine;
public class Heroik : MonoBehaviour
{
    public OneSubcribeAction ToInteractAction;
    
    [SerializeField] private Transform positionObj;
    [SerializeField] private GameObject currentTakenObjects;
    private IUseFurniture _useFurniture;
    private GameManager _gameManager;
    private IngredientHandler _ingredientHandler;

    public IUseFurniture CurrentUseFurniture
    {
        get
        {
            return _useFurniture;
        }
        set
        {
            if (_useFurniture == null)
            {
                _useFurniture = value;
                return;
            }
            
            IUseFurniture copy = _useFurniture;
            _useFurniture = value;
            copy.UpdateCondition();
        }
    }
    public GameObject CurrentTakenObjects => _ingredientHandler.CurrentTakenObjects;
    public bool IsBusyHands => _ingredientHandler.IsBusyHands;

    private void Awake()
    {
        ToInteractAction = new OneSubcribeAction();
    }

    private void Start()
    {
        _gameManager = StaticManagerWithoutZenject.GameManager;
        _ingredientHandler = new IngredientHandler(positionObj,positionObj,currentTakenObjects,this);
        //_ingredientHandler.CreateObj(currentTakenObjects);
    }
    
    public bool TryPickUp(GameObject ingredient) // взять объект в руки
    {
        return _ingredientHandler.TryPickUp(ingredient); 
    }
    
    // не забыть уничтожить объект из рук
    public GameObject TryGiveIngredient(List<Product> forbiddenIngredients) // отдать объект из рук
    {
        if (_ingredientHandler.CanGiveIngredient(forbiddenIngredients))
        {
            return _ingredientHandler.TryGiveIngredient(); 
        }
        Debug.LogWarning("Объект не подходит для этой furniture");
        return null;
    }
    
    public bool CanGiveIngredient(List<Product> forbiddenIngredients) // проверка на отдачу объекта из рук по списку объектов
    {
        return _ingredientHandler.CanGiveIngredient(forbiddenIngredients); 
    }

    public void CleanObjOnHands()
    {
        _ingredientHandler.CleanObjOnHands();
    }
}
