using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class Suvide :MonoBehaviour,IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn
{
    [SerializeField] private ProductsContainer productsContainer;
    
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private GameObject switchTimePrefab;
    [SerializeField] private GameObject switchTemperPrefab;
    [SerializeField] private HelperTimer firstTimer;
    [SerializeField] private HelperTimer secondTimer;
    [SerializeField] private HelperTimer thirdTimer;
    
    [SerializeField] private Transform firstPointIngredient;
    [SerializeField] private Transform secondPointIngredient;
    [SerializeField] private Transform thirdPointIngredient;
    
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    private SuvideView _suvideView;
    private SuvidePoints _suvidePoints;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private Animator _animator;
    
    private GameObject _result1 = null;
    private GameObject _result2 = null;
    private GameObject _result3 = null;
    private GameObject _ingredient1 = null;
    private GameObject _ingredient2 = null;
    private GameObject _ingredient3 = null;
    private float _timer = 0f;
    private float _updateInterval = 0.1f;
    private bool _isCookedResult1 = false;
    private bool _isCookedResult2 = false;
    private bool _isCookedResult3 = false;
    private bool _isReadyResult1 = false; 
    private bool _isReadyResult2 = false; 
    private bool _isReadyResult3 = false; 
    private bool _isWork = false;
    private bool _isHeroikTrigger = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _suvidePoints = new SuvidePoints(firstPointIngredient, secondPointIngredient, thirdPointIngredient, firstPointIngredient, secondPointIngredient, thirdPointIngredient);
        _suvideView = new SuvideView(waterPrefab, switchTimePrefab, switchTemperPrefab, firstTimer, secondTimer, thirdTimer, _animator);
    }

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
    }
    
    public void Update() // изменить
    {
        _timer += Time.deltaTime;
    
        if (_timer >= _updateInterval)
        {
            _timer = 0f;
            ChangeView(); 
        }
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
        if (_ingredient1 == null && _result1 == null)
        {
            _ingredient1 = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj, _suvidePoints.FirstPointIngredient,
                _suvidePoints.FirstPointIngredient, true,true);
        }
        else if (_ingredient2 == null && _result2 == null)
        {
            _ingredient2 = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj, _suvidePoints.SecondPointIngredient,
                _suvidePoints.SecondPointIngredient, true,true);
        }
        else if (_ingredient3== null && _result3 == null)
        {
            _ingredient3 = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj, _suvidePoints.ThirdPointIngredient,
                _suvidePoints.ThirdPointIngredient, true,true);
        }
        else
        {
            Debug.Log("место под ингредиенты нет");
        }
        Object.Destroy(acceptObj);
    }
    
    public void CreateResult(GameObject obj)
    {
        try
        {
            if (_isReadyResult1)
            {
                productsContainer.RecipesForSuvide.TryGetValue(obj.name, out ObjsForDistribution readyObj);
                if (readyObj != null)
                {
                    _result1 = StaticManagerWithoutZenject.ProductsFactory.GetProduct(readyObj.gameObject, _suvidePoints.FirstPointResult,
                        _suvidePoints.FirstPointResult, true,true);
                }
                else
                {
                    Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                }
                
            }
            else if (_isReadyResult2)
            {
                productsContainer.RecipesForSuvide.TryGetValue(obj.name, out ObjsForDistribution readyObj);
                if (readyObj != null)
                {
                    _result2 = StaticManagerWithoutZenject.ProductsFactory.GetProduct(readyObj.gameObject, _suvidePoints.SecondPointResult,
                        _suvidePoints.SecondPointResult, true,true);
                }
                else
                {
                    Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                }

            }
            else if (_isReadyResult3)
            {
                productsContainer.RecipesForSuvide.TryGetValue(obj.name, out ObjsForDistribution readyObj);
                if (readyObj != null)
                {
                    _result3 = StaticManagerWithoutZenject.ProductsFactory.GetProduct(readyObj.gameObject, _suvidePoints.ThirdPointResult,
                        _suvidePoints.ThirdPointResult, true,true);
                }
                else
                {
                    Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                }
                
            }
            else
            {
                Debug.LogError("Ошибка в CreateResult / переменная _readyResult");
            }
        }
        catch (Exception e)
        {
            Debug.Log("ошибка приготовления в сувиде" + e);
        }
    }
    
    public void TurnOn() 
    {
        if (_ingredient1 != null && !_isCookedResult1)
        {
            //_isWork = true;
            _isCookedResult1 = true;
            _suvideView.TurnOnFirstTimer();
            _isReadyResult1 = true;
        }
        else if (_ingredient2 != null && !_isCookedResult2)
        {
            _isWork = true;
            _isCookedResult2 = true;
            _suvideView.TurnOnSecondTimer();
            _isReadyResult2 = true;
        }
        else if (_ingredient3 != null && !_isCookedResult3)
        {
            _isWork = true;
            _isCookedResult3 = true;
            _suvideView.TurnOnThirdTimer();
            _isReadyResult3 = true;
        }
        else
        {
            Debug.LogError("Ошибка в TurnOn");
        }
    }

    public void TurnOff() 
    {
        if (_isReadyResult1)
        {
            _isWork = false;
            _isCookedResult1 = false;
            _ingredient1.SetActive(false);
            _ingredient1 = null;
            Object.Destroy(_ingredient1);
            _suvideView.TurnOff();
            _isReadyResult1 = false;
            
        }
        else if (_isReadyResult2)
        {
            _isWork = false;
            _isCookedResult2 = false;
            _ingredient2.SetActive(false);
            _ingredient2 = null;
            Object.Destroy(_ingredient2);
            _suvideView.TurnOff();
            _isReadyResult2 = false;
        }
        else if (_isReadyResult3)
        {
            _isWork = false;
            _isCookedResult3 = false;
            _ingredient3.SetActive(false);
            _ingredient3 = null;
            Object.Destroy(_ingredient3);
            _suvideView.TurnOff();
            _isReadyResult3 = false;
        }
        else
        {
            Debug.LogError("Ошибка в TurnOff");
        }
    }

    private void ChangeView()
    {
        if (_isCookedResult1 || _isCookedResult2 || _isCookedResult3)
        {
            _isWork = true;
            _suvideView.WorkingSuvide();
        }
        else
        {
            _isWork = false;
            _suvideView.NotWorkingSuvide();
        }
    }
    
    private void CookingProcess()
    {
        if(_isHeroikTrigger == false)
        {
            return;
        }
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            Debug.LogWarning("Сувид не работает");
            return;
        }
        
        if(_heroik.IsBusyHands == false) // руки не заняты
        {
            if (_result1 != null)
            {
                _heroik.ActiveObjHands(GiveObj(ref _result1));
            }
            else
            {
                if (_result2 != null)
                {
                    _heroik.ActiveObjHands(GiveObj(ref _result2));
                }
                else
                {
                    if (_result3 != null)
                    {
                        _heroik.ActiveObjHands(GiveObj(ref _result3));
                    }
                    else
                    {
                        Debug.Log("Сувид пуст руки тоже");
                    }
                }
            }
        }
        else // руки заняты
        {
            if (_result1 == null && !_isCookedResult1)
            {
                //проверка на подъодит ли предмет
                if (_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForSuvide)}))
                {
                    AcceptObject(_heroik.GiveObjHands());
                    TurnOn();
                    StartCookingProcessAsync(_ingredient1);
                }
                else
                {
                    Debug.Log("продукт не подходит для сувида");
                }
            }
            else if (_result1 != null || (_result1 == null && _isCookedResult1))
            {
                if (_result2 == null && !_isCookedResult2)
                {
                    //проверка на подъодит ли предмет
                    if (_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForSuvide)}))
                    {
                        AcceptObject(_heroik.GiveObjHands());
                        TurnOn();
                        StartCookingProcessAsync(_ingredient2);
                    }
                    else
                    {
                        Debug.LogWarning("продукт не подходит для сувида");
                    }
                }
                else if (_result2 != null || (_result2 == null && _isCookedResult2))
                {
                    if (_result3 == null && !_isCookedResult3)
                    {
                        //проверка на подъодит ли предмет
                        if (_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForSuvide)}))
                        {
                            AcceptObject(_heroik.GiveObjHands());
                            TurnOn();
                            StartCookingProcessAsync(_ingredient3);
                        }
                        else
                        {
                            Debug.LogWarning("продукт не подходит для сувида");
                        }
                    }
                    else if (_result3 != null || (_result3 == null && _isCookedResult3))
                    {
                        Debug.LogWarning("сувид заполнен");
                    }
                    else
                    {
                        Debug.LogError("Условие не сработало");
                    }
                }
                else
                {
                    Debug.LogError("Условие не сработало");
                }
            }
            else
            {
                Debug.LogError("Условие не сработало");
            }
        }
    }
    
    private async void StartCookingProcessAsync(GameObject obj)
    {
        await Task.Delay(10000);
        CreateResult(obj);
        TurnOff();
    }
    
}