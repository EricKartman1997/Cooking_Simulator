using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Heroik : MonoBehaviour
{
    public Action<AudioNameGamePlay> PlayOneShotClip;
    public OneSubcribeAction ToInteractAction;
    
    [SerializeField] private Transform positionObj;
    [SerializeField] private GameObject currentTakenObjects;
    [SerializeField] private HeroikSoundsControl soundsControl;
    private IUseFurniture _useFurniture;
    private IngredientHandler _ingredientHandler;
    private ProductsFactory _productsFactory;

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

    [Inject]
    private void ConstructZenject(ProductsFactory productsFactory)
    {
        _productsFactory = productsFactory;
    }
    
    private void Awake()
    {
        PlayOneShotClip += soundsControl.PlayOneShotClip;
        ToInteractAction = new OneSubcribeAction();
    }
    
    private void Start()
    {
        _ingredientHandler = new IngredientHandler(positionObj,positionObj,currentTakenObjects,_productsFactory);
        //_ingredientHandler.CreateObj(currentTakenObjects);
    }
    
    private void OnDestroy()
    {
        PlayOneShotClip -= soundsControl.PlayOneShotClip;
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
        
        PlayOneShotClip?.Invoke(AudioNameGamePlay.ForbiddenSound);
        
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
