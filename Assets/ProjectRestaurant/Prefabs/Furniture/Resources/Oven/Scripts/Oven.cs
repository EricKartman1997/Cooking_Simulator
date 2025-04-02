using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class Oven : IDisposable,  IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IIsAllowDestroy,IHeroikIsTrigger
{
    private GameObject _glassOn;
    private GameObject _glassOff;
    private GameObject _switchFirst;
    private GameObject _switchSecond;
    private GameObject _timer;
    private Transform _timerPoint;
    private Transform _timerParent;
    
    private Dictionary<string, FromOven> _dictionaryProductName;
    private Heroik _heroik; // только для объекта героя, а надо и другие...
    private Transform _positionResult; // сделать отдельный класс
    private Transform _parentResult;   // сделать отдельный класс
    
    private bool _isWork = false;
    private bool _isHeroikTrigger = false;
    private GameObject _ingredient;
    private GameObject _result;

    public Oven(GameObject glassOn, GameObject glassOff, GameObject switchFirst, GameObject switchSecond, GameObject timer, Transform timerPoint, Transform timerParent, Dictionary<string, FromOven> dictionaryProductName, Heroik heroik, Transform positionResult, Transform parentResult)
    {
        _glassOn = glassOn;
        _glassOff = glassOff;
        _switchFirst = switchFirst;
        _switchSecond = switchSecond;
        _timer = timer;
        _timerPoint = timerPoint;
        _timerParent = timerParent;
        _dictionaryProductName = dictionaryProductName;
        _heroik = heroik;
        _positionResult = positionResult;
        _parentResult = parentResult;
        
        EventBus.PressE += CookingProcess;
        Debug.Log("Создать объект: Oven");
    }
    
    public void Dispose()
    {
        EventBus.PressE -= CookingProcess;
        Debug.Log("У объекта вызван Dispose : Oven");
    }
    public GameObject GiveObj(ref GameObject cloneResult)
    {
        //Debug.Log("забираем предмет");
        cloneResult.SetActive(false);
        GameObject cloneResultCopy = cloneResult;
        Object.Destroy(cloneResult);
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
                _result = Object.Instantiate(_result, _positionResult.position, Quaternion.identity, _parentResult);
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
        Object.Instantiate(_timer, _timerPoint.position, Quaternion.identity,_timerParent);
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
        _isHeroikTrigger = !_isHeroikTrigger;
    }
    
    private void CookingProcess()
    {
        if(_isHeroikTrigger == true)
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
