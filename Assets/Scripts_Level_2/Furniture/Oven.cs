using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Oven : MonoBehaviour, IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IIsAllowDestroy,IHeroikIsTrigger
{
    [SerializeField] private GameObject _glassOn;
    [SerializeField] private GameObject _glassOff;
    [SerializeField] private GameObject _switchFirst;
    [SerializeField] private GameObject _switchSecond;
    [SerializeField] private GameObject _timer;
    [SerializeField] private Transform _timerPoint;
    [SerializeField] private Transform _timerParent;
    private Dictionary<string, FoodReadyOven> _dictionaryProductName;
    private Heroik _heroik; // только для объекта героя, а надо и другие...
    private Transform _positionResult; // сделать отдельный класс
    private Transform _parentResult;   // сделать отдельный класс
    
    private bool _isWork = false;
    private bool _heroikIsTrigger = false;
    private float _timeCurrent = 0.17f;
    [SerializeField] private GameObject _ingredient;
    [SerializeField] private GameObject _result;
    [SerializeField] private GameObject _cloneResult;

    public void Initialize(GameObject glassOn, GameObject glassOff,GameObject switchFirst,GameObject switchSecond,GameObject timer,Transform timerPoint,Transform timerParent,Heroik heroik,Transform positionResult, Transform perentResult,Dictionary<string, FoodReadyOven> dictionaryProductName)
    {
        _glassOn = glassOn;
        _glassOff = glassOff;
        _switchFirst = switchFirst;
        _switchSecond = switchSecond;
        _timer = timer;
        _timerPoint = timerPoint;
        _timerParent = timerParent;
        _heroik = heroik;
        _positionResult = positionResult;
        _parentResult = perentResult;
        _dictionaryProductName = dictionaryProductName;
    }
    
    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.E) && _heroikIsTrigger )
        {
            if (_timeCurrent >= 0.17f)
            {
                if (!Heroik.IsBusyHands) // руки не заняты
                {
                    if (_isWork)
                    {
                        Debug.Log("ждите печка работает");
                    }
                    else
                    {
                        if (_result != null)
                        {
                            _heroik.ActiveObjHands(GiveObj(ref _cloneResult));
                        }
                        else
                        {
                            Debug.Log("печка пуста руки тоже");
                        }
                    }
                }
                else // заняты
                {
                    if (_isWork)
                    {
                        Debug.Log("ждите печка работает");
                    }
                    else
                    {
                        if (_result != null)
                        {
                            Debug.Log("Сначала заберите предмет");
                        }
                        else
                        {
                            if (_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForOven)}))
                            {
                                AcceptObject(_heroik.GiveObjHands());
                                TurnOn();
                                StartCookingProcessAsync(_ingredient);
                            }
                        }
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
        await Task.Delay(5000);
        TurnOff();
        CreateResult(obj);
    }
    
    public GameObject GiveObj(ref GameObject cloneResult)
    {
        Debug.Log("забираем предмет");
        cloneResult.SetActive(false);
        GameObject cloneResultCopy = cloneResult;
        Destroy(cloneResult);
        _result = null;
        return cloneResultCopy;
    }

    public void AcceptObject(GameObject obj)
    {
        _ingredient = obj;
    }
    
    public void TurnOn()
    {
        _isWork = true;
        _glassOff.SetActive(false);
        _glassOn.SetActive(true);
        _switchFirst.transform.rotation = Quaternion.Euler(0, 0, -90);
        _switchSecond.transform.rotation = Quaternion.Euler(0, 0, -135);
        Instantiate(_timer, _timerPoint.position, Quaternion.identity,_timerParent);
    }

    public void TurnOff()
    {
        _isWork = false;
        _glassOff.SetActive(true);
        _glassOn.SetActive(false);
        _switchFirst.transform.rotation = Quaternion.Euler(0, 0, 0);
        _switchSecond.transform.rotation = Quaternion.Euler(0, 0, 0);
        _ingredient = null;
    }
    
    public bool IsAllowDestroy()
    {
        if (!_isWork && _result == null)
        {
            return true;
        }
        return false;
    }
    
    public void HeroikIsTrigger()
    {
        _heroikIsTrigger = !_heroikIsTrigger;
    }

    public void CreateResult(GameObject obj)
    {
        try
        {
            RawFood rawFood = obj.GetComponent<RawFood>();
            _dictionaryProductName.TryGetValue(rawFood.name, out FoodReadyOven bakedObj);
            _result = bakedObj.gameObject;
            _cloneResult = Instantiate(_result, _positionResult.position, Quaternion.identity, _parentResult);
            _cloneResult.name = _cloneResult.name.Replace("(Clone)", "");
            _cloneResult.SetActive(true);
        }
        catch (Exception e)
        {
            Debug.Log("ошибка приготовления в духовке" + e);
        }
    }
    
}
