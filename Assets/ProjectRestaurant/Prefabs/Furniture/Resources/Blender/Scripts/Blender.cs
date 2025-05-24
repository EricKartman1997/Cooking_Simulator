using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class Blender : MonoBehaviour,IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IFindReadyFood
{
    [SerializeField] private ProductsContainer productsContainer;
    
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform pointUp;
    [SerializeField] private Transform timerParent;
    
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;
    [SerializeField] private Transform thirdPoint;

    
    private Heroik _heroik = null;
    private BlenderPoints _blenderPoints;
    private BlenderView _blenderView;
    
    private GameObject _ingredient1 = null;
    private GameObject _ingredient2 = null;
    private GameObject _ingredient3 = null;
    private GameObject _result = null;
    private bool _isWork = false;
    private bool _isHeroikTrigger = false;
    
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _blenderPoints = new BlenderPoints(firstPoint, secondPoint, thirdPoint, pointUp, pointUp);
        _blenderView = new BlenderView(timer, pointUp, timerParent, _animator);
    }

    void Start()
    {
        //_animator.SetBool("Work", false);
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
            _isHeroikTrigger = false;
            _outline.OutlineWidth = 0f;
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik = null;
            _isHeroikTrigger = false;
            _outline.OutlineWidth = 0f;
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
            _ingredient1 = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj, _blenderPoints.FirstPoint.transform, _blenderPoints.ParentFood,true);
        }
        else if (_ingredient2 == null)
        {
            _ingredient2 = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj, _blenderPoints.SecondPoint.transform, _blenderPoints.ParentFood,true);
        }
        else if (_ingredient3 == null)
        {
            _ingredient3 = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj, _blenderPoints.ThirdPoint.transform, _blenderPoints.ParentFood,true);
        }
        else
        {
            Debug.LogWarning("В блендере нет места");
        }
        Object.Destroy(acceptObj);
    }
    
    public void CreateResult(GameObject obj)
    {
        _result = StaticManagerWithoutZenject.ProductsFactory.GetProduct(obj, _blenderPoints.SecondPoint.transform, _blenderPoints.ParentReadyFood,true);
    }

    public void TurnOn()
    {
        _ingredient1.SetActive(false);
        _ingredient2.SetActive(false);
        _ingredient3.SetActive(false);
        _isWork = true;
        _blenderView.TurnOn();
    }

    public void TurnOff()
    {
        _isWork = false;
        Object.Destroy(_ingredient1);
        Object.Destroy(_ingredient2);
        Object.Destroy(_ingredient3);
        _ingredient1 = null;
        _ingredient2 = null;
        _ingredient3 = null;
        _blenderView.TurnOff();
    }
    
    public GameObject FindReadyFood()
    {
        List<GameObject> currentFruits = new List<GameObject>(){_ingredient1,_ingredient2,_ingredient3};
        if (SuitableIngredients(currentFruits,productsContainer.RequiredFreshnessCocktail))
        {
            return productsContainer.FreshnessCocktail;
        }
        if(SuitableIngredients(currentFruits,productsContainer.RequiredWildBerryCocktail))
        {
            return productsContainer.WildBerryCocktail;
        }
        return productsContainer.Rubbish;
    }

    public bool SuitableIngredients(List<GameObject> currentFruits, List<GameObject> requiredFruits)
    {
        List<string> requiredFruitsNames = new List<string>();
        List<string> currentFruitNames = new List<string>();
        foreach (var fruit in currentFruits)
        {
            currentFruitNames.Add(fruit.name); // Используем имя объекта
        }
        foreach (var fruit in requiredFruits)
        {
            requiredFruitsNames.Add(fruit.name); // Используем имя объекта
        }
        foreach (string fruit in requiredFruitsNames)
        {
            if (!currentFruitNames.Contains(fruit))
            {
                return false;
            }
        }
        return true;
    }
    
    private async void StartCookingProcessAsync(GameObject obj)
    {
        await Task.Delay(4000);
        TurnOff();
        CreateResult(obj);
    }
    private void CookingProcess()
    {
        if(_isHeroikTrigger == false)
        {
            return;
        }
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            Debug.LogWarning("Блендер не работает");
            return;
        }
        
        if(_heroik.IsBusyHands == false) // руки не заняты
        {
            if (_isWork)
            {
                Debug.Log("ждите блендер готовится");
            }
            else
            {
                if (_result == null)
                {
                    if (_ingredient1 == null)
                    {
                        Debug.Log("Руки пусты ингредиентов нет");
                    }
                    else
                    {
                        if (_ingredient2 == null)
                        {
                            _heroik.ActiveObjHands(GiveObj(ref _ingredient1));
                            _ingredient1 = null;
                        }
                        else
                        {
                            _heroik.ActiveObjHands(GiveObj(ref _ingredient2));
                            _ingredient2 = null;
                        }
                    }
                }
                else
                {
                    _heroik.ActiveObjHands(GiveObj(ref _result));
                }
                        
            }
                    
        }
        else // руки заняты
        {
            if (_isWork)
            {
                Debug.Log("ждите блендер готовится");
            }
            else
            {
                if (_result == null)
                {
                    if (_ingredient1 == null)
                    {
                        if(_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForBlender),typeof(Fruit)}))
                        {
                            AcceptObject(_heroik.GiveObjHands());
                            Debug.Log("Предмет первый положен в блендер");
                        }
                        else
                        {
                            Debug.Log("с предметом нельзя взаимодействовать");
                        }
                    }
                    else
                    {
                        if (_ingredient2 == null)
                        {
                            if(_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForBlender),typeof(Fruit)}))
                            {
                                AcceptObject(_heroik.GiveObjHands());
                                Debug.Log("Предмет второй положен в блендер");
                            }
                            else
                            {
                                Debug.Log("с предметом нельзя взаимодействовать");
                            }
                        }
                        else
                        {
                            if(_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForBlender),typeof(Fruit)}))
                            {
                                AcceptObject(_heroik.GiveObjHands());
                                Debug.Log("Предмет третий положен в блендер");
                                TurnOn(); 
                                GameObject objdish = FindReadyFood();
                                StartCookingProcessAsync(objdish);
                            }
                            else
                            {
                                Debug.Log("с предметом нельзя взаимодействовать");
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Руки полные уберите предмет");
                }
            }
        }
    }
    
}
