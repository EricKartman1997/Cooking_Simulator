using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Suvide : MonoBehaviour, IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IIsAllowDestroy,IHeroikIsTrigger
{
    // Initialize
    [SerializeField] private GameObject _waterPrefab;
    [SerializeField] private GameObject _switchTimePrefab;
    [SerializeField] private GameObject _switchTemperPrefab;
    [SerializeField] private HelperTimer _firstTimer;
    [SerializeField] private HelperTimer _secondTimer;
    [SerializeField] private HelperTimer _thirdTimer;
    [SerializeField]private SuvidePoints _suvidePoints;
    [SerializeField]private Animator _animator; // добавить анимацию
    [SerializeField]private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private Dictionary<string, ObjsForDistribution> _dictionaryRecipes;
    
    [SerializeField]private GameObject _result1 = null;
    [SerializeField]private GameObject _result2 = null;
    [SerializeField]private GameObject _result3 = null;
    [SerializeField]private GameObject _ingredient1 = null;
    [SerializeField]private GameObject _ingredient2 = null;
    [SerializeField]private GameObject _ingredient3 = null;
    [SerializeField]private bool _isCookedResult1 = false;
    [SerializeField]private bool _isCookedResult2 = false;
    [SerializeField]private bool _isCookedResult3 = false;
    [SerializeField]private bool _isReadyResult1 = false; 
    [SerializeField]private bool _isReadyResult2 = false; 
    [SerializeField]private bool _isReadyResult3 = false; 
    
    private bool _isWork = false;
    private bool _heroikIsTrigger = false;

    public void Initialize(Animator animator,Heroik heroik,HelperTimer firstTimer,
        HelperTimer secondTimer, HelperTimer thirdTimer, GameObject switchTemperPrefab,GameObject switchTimePrefab,
        GameObject waterPrefab,SuvidePoints suvidePoints,Dictionary<string, ObjsForDistribution> recipes)
    {
        _animator = animator;
        _heroik = heroik;
        _firstTimer = firstTimer;
        _secondTimer = secondTimer;
        _thirdTimer = thirdTimer;
        _switchTemperPrefab = switchTemperPrefab;
        _switchTimePrefab = switchTimePrefab;
        _waterPrefab = waterPrefab;
        _suvidePoints = suvidePoints;
        _dictionaryRecipes = recipes;
    }
    
    private void OnEnable()
    {
        EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        EventBus.PressE -= CookingProcess;
    }
    
    public GameObject GiveObj(ref GameObject obj) 
    {
        obj.SetActive(false);
        GameObject cobj = obj;
        Destroy(obj);
        obj = null;
        return cobj;
    }
    
    public void AcceptObject(GameObject acceptObj) 
    {
        if (_ingredient1 == null && _result1 == null)
        {

            _ingredient1 = acceptObj;
            _ingredient1 = Instantiate(_ingredient1, _suvidePoints.GetFirstPointIngredient().position, Quaternion.identity, _suvidePoints.GetFirstPointIngredient());
            _ingredient1.transform.localPosition = Vector3.zero;
            _ingredient1.transform.localRotation = Quaternion.identity;
            _ingredient1.name = _ingredient1.name.Replace("(Clone)", "");
            _ingredient1.SetActive(true);
        }
        else if (_ingredient2 == null && _result2 == null)
        {
            _ingredient2 = acceptObj;
            _ingredient2 = Instantiate(_ingredient2, _suvidePoints.GetSecondPointIngredient().position, Quaternion.identity, _suvidePoints.GetSecondPointIngredient());
            _ingredient2.transform.localPosition = Vector3.zero;
            _ingredient2.transform.localRotation = Quaternion.identity;
            _ingredient2.name = _ingredient2.name.Replace("(Clone)", "");
            _ingredient2.SetActive(true);
        }
        else if (_ingredient3== null && _result3 == null)
        {
            _ingredient3 = acceptObj;
            _ingredient3 = Instantiate(_ingredient3, _suvidePoints.GetThirdPointIngredient().position, Quaternion.identity, _suvidePoints.GetThirdPointIngredient());
            _ingredient3.transform.localPosition = Vector3.zero;
            _ingredient3.transform.localRotation = Quaternion.identity;
            _ingredient3.name = _ingredient3.name.Replace("(Clone)", "");
            _ingredient3.SetActive(true);
        }
        else
        {
            Debug.Log("место под ингредиенты нет");
        }
    }
    
    public void CreateResult(GameObject obj)
    {
        try
        {
            if (_isReadyResult1)
            {
                _dictionaryRecipes.TryGetValue(obj.name, out ObjsForDistribution readyObj);
                if (readyObj != null)
                {
                    _result1 = readyObj.gameObject;
                    _result1 = Instantiate(_result1, _suvidePoints.GetFirstPointResult().position , Quaternion.identity, _suvidePoints.GetFirstPointResult());
                    _result1.transform.localPosition = Vector3.zero;
                    _result1.transform.localRotation = Quaternion.identity;
                    _result1.name = _result1.name.Replace("(Clone)", "");
                    _result1.SetActive(true);
                    //_isReadyResult1 = false;
                }
                else
                {
                    Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                }
                
            }
            else if (_isReadyResult2)
            {
                _dictionaryRecipes.TryGetValue(obj.name, out ObjsForDistribution readyObj);
                if (readyObj != null)
                {
                    _result2 = readyObj.gameObject;
                    _result2 = Instantiate(_result2, _suvidePoints.GetSecondPointResult().position , Quaternion.identity, _suvidePoints.GetSecondPointResult());
                    _result2.transform.localPosition = Vector3.zero;
                    _result2.transform.localRotation = Quaternion.identity;
                    _result2.name = _result2.name.Replace("(Clone)", "");
                    _result2.SetActive(true);
                    //_isReadyResult2 = false;
                }
                else
                {
                    Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                }

            }
            else if (_isReadyResult3)
            {
                _dictionaryRecipes.TryGetValue(obj.name, out ObjsForDistribution readyObj);
                if (readyObj != null)
                {
                    _result3 = readyObj.gameObject;
                    _result3 = Instantiate(_result3, _suvidePoints.GetThirdPointResult().position , Quaternion.identity, _suvidePoints.GetThirdPointResult());
                    _result3.transform.localPosition = Vector3.zero;
                    _result3.transform.localRotation = Quaternion.identity;
                    _result3.name = _result3.name.Replace("(Clone)", "");
                    _result3.SetActive(true);
                    //_isReadyResult3 = false;
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
            //_animator.SetBool("Work", true);
            Instantiate(_firstTimer.timer, _firstTimer.timerPoint.position, Quaternion.identity,_firstTimer.timerParent);
            _isReadyResult1 = true;
        }
        else if (_ingredient2 != null && !_isCookedResult2)
        {
            _isWork = true;
            _isCookedResult2 = true;
            //_animator.SetBool("Work", true);
            Instantiate(_secondTimer.timer, _secondTimer.timerPoint.position, Quaternion.identity,_secondTimer.timerParent);
            _isReadyResult2 = true;
        }
        else if (_ingredient3 != null && !_isCookedResult3)
        {
            _isWork = true;
            _isCookedResult3 = true;
            //_animator.SetBool("Work", true);
            Instantiate(_thirdTimer.timer, _thirdTimer.timerPoint.position, Quaternion.identity,_thirdTimer.timerParent);
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
            Destroy(_ingredient1);
            //_animator.SetBool("Work", false);
            _isReadyResult1 = false;
            
        }
        else if (_isReadyResult2)
        {
            _isWork = false;
            _isCookedResult2 = false;
            _ingredient2.SetActive(false);
            _ingredient2 = null;
            Destroy(_ingredient2);
            //_animator.SetBool("Work", false);
            _isReadyResult2 = false;
        }
        else if (_isReadyResult3)
        {
            _isWork = false;
            _isCookedResult3 = false;
            _ingredient3.SetActive(false);
            _ingredient3 = null;
            Destroy(_ingredient3);
            //_animator.SetBool("Work", false);
            _isReadyResult3 = false;
        }
        else
        {
            Debug.LogError("Ошибка в TurnOff");
        }
    }
    
    public bool IsAllowDestroy()
    {
        if (_ingredient1 == null && _ingredient2 == null && _ingredient3 == null && _result1 == null &&
            _result2 == null && _result3 == null && !_isWork)
        {
            return true;
        }
        return false;
    }

    public void HeroikIsTrigger()
    {
        _heroikIsTrigger = !_heroikIsTrigger;
    }
    
    private void WorkingSuvide()
    {
        _waterPrefab.SetActive(true);
        _switchTemperPrefab.transform.rotation = Quaternion.Euler(45, -90, -90);
        _switchTimePrefab.transform.rotation = Quaternion.Euler(175, -90, -90);
    }
    private void NotWorkingSuvide()
    {
        _waterPrefab.SetActive(false);
        _switchTemperPrefab.transform.rotation = Quaternion.Euler(90, -90, -90);
        _switchTimePrefab.transform.rotation = Quaternion.Euler(90, -90, -90);
    }
    
    private void CookingProcess()
    {
        // переделать в отдельный метод
        if (_isCookedResult1 || _isCookedResult2 || _isCookedResult3)
        {
            _isWork = true;
            WorkingSuvide();
        }
        else
        {
            _isWork = false;
            NotWorkingSuvide();
        }
        
        if (_heroikIsTrigger == true)
        {
            if(!Heroik.IsBusyHands) // руки не заняты
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
    }
    
    private async void StartCookingProcessAsync(GameObject obj)
    {
        await Task.Delay(10000);
        CreateResult(obj);
        TurnOff();
    }

}