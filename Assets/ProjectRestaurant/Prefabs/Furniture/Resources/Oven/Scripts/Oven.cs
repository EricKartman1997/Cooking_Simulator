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
    private Dictionary<string, FromOven> _dictionaryProductName;
    private Heroik _heroik; // только для объекта героя, а надо и другие...
    private Transform _positionResult; // сделать отдельный класс
    private Transform _parentResult;   // сделать отдельный класс
    
    private bool _isWork = false;
    private bool _heroikIsTrigger = false;
    [SerializeField] private GameObject _ingredient;
    [SerializeField] private GameObject _result;

    public void Initialize(GameObject glassOn, GameObject glassOff,GameObject switchFirst,GameObject switchSecond,GameObject timer,Transform timerPoint,Transform timerParent,Heroik heroik,Transform positionResult, Transform perentResult,Dictionary<string, FromOven> dictionaryProductName)
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
    
    private void OnEnable()
    {
        EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        EventBus.PressE -= CookingProcess;
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
    
    public void CreateResult(GameObject obj)
    {
        try
        {
            _dictionaryProductName.TryGetValue(obj.name, out FromOven bakedObj);
            if (bakedObj != null)
            {
                _result = bakedObj.gameObject;
                _result = Instantiate(_result, _positionResult.position, Quaternion.identity, _positionResult);
                _result.transform.localPosition = Vector3.zero;
                _result.transform.localRotation = Quaternion.identity;
                _result.name = _result.name.Replace("(Clone)", "");
                _result.SetActive(true);
            }
            else
            {
                Debug.LogError("Ошибка в CreateResult, такого ключа нет");
            }
            
        }
        catch (Exception e)
        {
            Debug.Log("ошибка приготовления в духовке" + e);
        }
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
    
    private void CookingProcess()
    {
        if(_heroikIsTrigger == true)
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
                        _heroik.ActiveObjHands(GiveObj(ref _result));
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
        }
    }
    
    private async void StartCookingProcessAsync(GameObject obj)
    {
        await Task.Delay(5000);
        TurnOff();
        CreateResult(obj);
    }
}
