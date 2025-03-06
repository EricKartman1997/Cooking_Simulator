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
    [SerializeField] private Timer_Prefab _firstTimer;
    [SerializeField] private Timer_Prefab _secondTimer;
    [SerializeField] private Timer_Prefab _thirdTimer;
    [SerializeField]private SuvidePoints _suvidePoints;
    [SerializeField]private Animator _animator; // добавить анимацию
    [SerializeField]private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private Dictionary<string, ReadyFood> _dictionaryRecipes;
    
    [SerializeField]private GameObject _result1 = null;
    [SerializeField]private GameObject _result2 = null;
    [SerializeField]private GameObject _result3 = null;
    [SerializeField]private GameObject _ingredient1 = null;
    [SerializeField]private GameObject _ingredient2 = null;
    [SerializeField]private GameObject _ingredient3 = null;
    // [SerializeField]private GameObject _cloneIngredient1; // мб убрать
    // [SerializeField]private GameObject _cloneIngredient2;
    // [SerializeField]private GameObject _cloneIngredient3;
    [SerializeField]private bool _isCookedFirstResult = false;
    [SerializeField]private bool _isCookedSecondResult = false;
    [SerializeField]private bool _isCookedThirdResult = false;
    [SerializeField]private bool _readyResult1 = false; 
    [SerializeField]private bool _readyResult2 = false; 
    [SerializeField]private bool _readyResult3 = false; 
    
    private bool _isWork = false;
    private float _timeCurrent = 0.17f;
    private bool _heroikIsTrigger = false;

    public void Initialize(Animator animator,Heroik heroik,Timer_Prefab firstTimer,
        Timer_Prefab secondTimer, Timer_Prefab thirdTimer, GameObject switchTemperPrefab,GameObject switchTimePrefab,
        GameObject waterPrefab,SuvidePoints suvidePoints,Dictionary<string, ReadyFood> recipes)
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
    
    private void Update()
    {
        // переделать в отдельный метод
        if (_isCookedFirstResult || _isCookedSecondResult || _isCookedThirdResult)
        {
            _isWork = true;
            WorkingSuvide();
        }
        else
        {
            _isWork = false;
            NotWorkingSuvide();
        }
        
        _timeCurrent += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && _heroikIsTrigger)
        {
            if (_timeCurrent >= 0.17f)
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
                    if (_result1 == null && !_isCookedFirstResult)
                    {
                        //проверка на подъодит ли предмет
                        if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
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
                    else if (_result1 != null || (_result1 == null && _isCookedFirstResult))
                    {
                        if (_result2 == null && !_isCookedSecondResult)
                        {
                            //проверка на подъодит ли предмет
                            if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
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
                        else if (_result2 != null || (_result2 == null && _isCookedSecondResult))
                        {
                            if (_result3 == null && !_isCookedThirdResult)
                            {
                                //проверка на подъодит ли предмет
                                if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
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
                            else if (_result3 != null || (_result3 == null && _isCookedThirdResult))
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
                _timeCurrent = 0f;
            }
            else
            {
                Debug.LogWarning("Ждите перезарядки кнопки");
            }
        }
    }
    
    private async void StartCookingProcessAsync(GameObject obj)
    {
        await Task.Delay(10000);
        CreateResult(obj);
        TurnOff();
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
            if (_readyResult1)
            {
                _dictionaryRecipes.TryGetValue(obj.name, out ReadyFood readyObj);
                if (readyObj != null)
                {
                    _result1 = readyObj.gameObject;
                    _result1 = Instantiate(_result1, _suvidePoints.GetFirstPointResult().position , Quaternion.identity, _suvidePoints.GetFirstPointResult());
                    _result1.transform.localPosition = Vector3.zero;
                    _result1.transform.localRotation = Quaternion.identity;
                    _result1.name = _result1.name.Replace("(Clone)", "");
                    _result1.SetActive(true);
                    //_readyResult1 = false;
                }
                else
                {
                    Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                }
                
            }
            else if (_readyResult2)
            {
                _dictionaryRecipes.TryGetValue(obj.name, out ReadyFood readyObj);
                if (readyObj != null)
                {
                    _result2 = readyObj.gameObject;
                    _result2 = Instantiate(_result2, _suvidePoints.GetSecondPointResult().position , Quaternion.identity, _suvidePoints.GetSecondPointResult());
                    _result2.transform.localPosition = Vector3.zero;
                    _result2.transform.localRotation = Quaternion.identity;
                    _result2.name = _result2.name.Replace("(Clone)", "");
                    _result2.SetActive(true);
                    //_readyResult2 = false;
                }
                else
                {
                    Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                }

            }
            else if (_readyResult3)
            {
                _dictionaryRecipes.TryGetValue(obj.name, out ReadyFood readyObj);
                if (readyObj != null)
                {
                    _result3 = readyObj.gameObject;
                    _result3 = Instantiate(_result3, _suvidePoints.GetThirdPointResult().position , Quaternion.identity, _suvidePoints.GetThirdPointResult());
                    _result3.transform.localPosition = Vector3.zero;
                    _result3.transform.localRotation = Quaternion.identity;
                    _result3.name = _result3.name.Replace("(Clone)", "");
                    _result3.SetActive(true);
                    //_readyResult3 = false;
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
        if (_ingredient1 != null && !_isCookedFirstResult)
        {
            //_isWork = true;
            _isCookedFirstResult = true;
            //_animator.SetBool("Work", true);
            Instantiate(_firstTimer.timer, _firstTimer.timerPoint.position, Quaternion.identity,_firstTimer.timerParent);
            _readyResult1 = true;
        }
        else if (_ingredient2 != null && !_isCookedSecondResult)
        {
            _isWork = true;
            _isCookedSecondResult = true;
            //_animator.SetBool("Work", true);
            Instantiate(_secondTimer.timer, _secondTimer.timerPoint.position, Quaternion.identity,_secondTimer.timerParent);
            _readyResult2 = true;
        }
        else if (_ingredient3 != null && !_isCookedThirdResult)
        {
            _isWork = true;
            _isCookedThirdResult = true;
            //_animator.SetBool("Work", true);
            Instantiate(_thirdTimer.timer, _thirdTimer.timerPoint.position, Quaternion.identity,_thirdTimer.timerParent);
            _readyResult3 = true;
        }
        else
        {
            Debug.LogError("Ошибка в TurnOn");
        }
    }

    public void TurnOff() 
    {
        if (_readyResult1)
        {
            _isWork = false;
            _isCookedFirstResult = false;
            _ingredient1.SetActive(false);
            _ingredient1 = null;
            Destroy(_ingredient1);
            //_animator.SetBool("Work", false);
            _readyResult1 = false;
            
        }
        else if (_readyResult2)
        {
            _isWork = false;
            _isCookedSecondResult = false;
            _ingredient2.SetActive(false);
            _ingredient2 = null;
            Destroy(_ingredient2);
            //_animator.SetBool("Work", false);
            _readyResult2 = false;
        }
        else if (_readyResult3)
        {
            _isWork = false;
            _isCookedThirdResult = false;
            _ingredient3.SetActive(false);
            _ingredient3 = null;
            Destroy(_ingredient3);
            //_animator.SetBool("Work", false);
            _readyResult3 = false;
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

}