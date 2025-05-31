using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class CuttingTable : MonoBehaviour,IGiveObj,IAcceptObject,ICreateResult,ITurnOffOn,IFindReadyFood
{

    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    
    [SerializeField] private Transform positionIngredient1; 
    [SerializeField] private Transform positionIngredient2; 
    [SerializeField] private Transform positionResult;      

    [SerializeField] private ProductsContainer productsContainer;
    

    private bool _isWork = false;
    private bool _isHeroikTrigger = false;
    private GameObject _ingredient1 = null;
    private GameObject _ingredient2 = null;
    private GameObject _result = null;
    
    private Heroik _heroik = null;
    private DecorationFurniture _decorationFurniture;
    private Outline _outline;
    private Animator _animator;
    private CuttingTablePoints _cuttingTablePoints;
    private CuttingTableView _cuttingTableView;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _animator.SetBool("Work", false);
        _cuttingTablePoints = StaticManagerWithoutZenject.HelperScriptFactory.GetCuttingTablePoints(positionIngredient1,positionIngredient2,positionResult);
        _cuttingTableView = StaticManagerWithoutZenject.HelperScriptFactory.GetCuttingTableView(_animator,timer,timerPoint);
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            _outline.OutlineWidth = 2f;
            _isHeroikTrigger = true;
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            _isHeroikTrigger = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            _outline.OutlineWidth = 0f;
            _isHeroikTrigger = false;
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik = null;
            _outline.OutlineWidth = 0f;
            _isHeroikTrigger = false;
            
        }
    }
    
    private void OnEnable()
    {
        EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        EventBus.PressE -= CookingProcess;
    }
    
    public GameObject GiveObj(ref GameObject giveObj)
    {
        return giveObj;
    }
    
    public void AcceptObject(GameObject acceptObj)
    {
        if (_ingredient1 == null)
        {
            _ingredient1 = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj, _cuttingTablePoints.PositionIngredient1,
                _cuttingTablePoints.PositionIngredient1,true);
        }
        else if (_ingredient2 == null)
        {
             _ingredient2 = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj, _cuttingTablePoints.PositionIngredient2,
                _cuttingTablePoints.PositionIngredient2,true);
        }
        else
        {
            Debug.LogWarning("На нарезочном столе нет места");
        }
        Object.Destroy(acceptObj);
    }
    
    public void CreateResult(GameObject obj)
    {
        _result = StaticManagerWithoutZenject.ProductsFactory.GetProduct(obj, _cuttingTablePoints.PositionResult,
            _cuttingTablePoints.PositionResult,true);
    }
    
    public void TurnOn()
    {
        _isWork = true;
        _ingredient1.SetActive(false);
        _ingredient2.SetActive(false);
        _cuttingTableView.TurnOn();
    }
    
    public void TurnOff()
    {
        _isWork = false;
        Object.Destroy(_ingredient1);
        Object.Destroy(_ingredient2);
        _ingredient1 = null;
        _ingredient2 = null;
        _cuttingTableView.TurnOff();
    }
    
    public GameObject FindReadyFood()
    {
        List<GameObject> currentIngredient = new List<GameObject>(){_ingredient1,_ingredient2};
        if (SuitableIngredients(currentIngredient,productsContainer.RequiredFruitSalad))
        {
            return productsContainer.FruitSalad;
        }
        if(SuitableIngredients(currentIngredient,productsContainer.RequiredMixBakedFruit))
        {
            return productsContainer.MixBakedFruit;
        }
        return productsContainer.Rubbish;
    }

    public bool SuitableIngredients(List<GameObject> currentIngredients, List<GameObject> requiredFruits)
    {
        List<string> requiredFruitsNames = new List<string>();
        List<string> currentIngredientsNames = new List<string>();
        foreach (var ingredient in currentIngredients)
        {
            currentIngredientsNames.Add(ingredient.name); // Используем имя объекта
        }
        foreach (var ingredient in requiredFruits)
        {
            requiredFruitsNames.Add(ingredient.name); // Используем имя объекта
        }
        foreach (string ingredient in requiredFruitsNames)
        {
            if (!currentIngredientsNames.Contains(ingredient))
            {
                return false;
            }
        }
        return true;
    }
    
    private void CookingProcess()
    {
        if(_isHeroikTrigger == false)
        {
            return;
        }
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            Debug.LogWarning("Стол не работает");
            return;
        }
        
        if(_heroik.IsBusyHands == false) // руки не заняты
        {
            if (_isWork)
            {
                Debug.Log("ждите блюдо готовится");
            }
            else
            {
                if (_result == null)
                {
                    if (_ingredient1 == null)
                    {
                        Debug.Log("У вас пустые руки и прилавок пуст");
                    }
                    else //есть первый ингредиент // забираете первый ингредиент 
                    {
                        _heroik.ActiveObjHands(GiveObj(ref _ingredient1));
                        _ingredient1 = null;
                    }
                }
                else //есть результат // забрать результат
                {
                    _heroik.ActiveObjHands(GiveObj(ref _result));
                    _result = null;
                    Debug.Log("Вы забрали конечный продукт"); 
                }
            }
        }
        else // заняты
        {
            if (_isWork)
            {
                Debug.Log("ждите блюдо готовится");
            }
            else
            {
                if (_result == null)
                {
                    if (_ingredient1 == null)// ингредиентов нет
                    {
                        if(_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForCutting)}))
                        {
                            AcceptObject(_heroik.GiveObjHands());
                        }
                        else
                        {
                            Debug.Log("с предметом нельзя взаимодействовать");
                        }
                    }
                    else// есть первый ингредиент
                    {
                        if (_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForCutting)}))
                        {
                            AcceptObject(_heroik.GiveObjHands());
                            //Debug.Log("Предмет второй положен на нарезочный стол");
                            TurnOn(); 
                            GameObject objdish = FindReadyFood();
                            StartCookingProcessAsync(objdish);
                        }
                        else
                        {
                            Debug.Log("Объект не подходит для слияния");
                        }
                    }
                }
                else
                {
                    Debug.Log("Сначала уберите предмет из рук");
                }
                        
            }
                    
        }
    }
    
    private async void StartCookingProcessAsync(GameObject obj)
    {
        await Task.Delay(3000);
        TurnOff();
        CreateResult(obj);
    }
}
