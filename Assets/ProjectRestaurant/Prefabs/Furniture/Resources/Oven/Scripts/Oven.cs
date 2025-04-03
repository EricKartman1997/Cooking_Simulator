using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class Oven : IDisposable,  IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IIsAllowDestroy,IHeroikIsTrigger
{
    private Dictionary<string, FromOven> _dictionaryProductName;
    private Heroik _heroik; // только для объекта героя, а надо и другие...
    private Transform _positionResult; // сделать отдельный класс
    private Transform _parentResult;   // сделать отдельный класс
    private OvenView _ovenView;
    
    private bool _isWork = false;
    private bool _isHeroikTrigger = false;
    private GameObject _ingredient;
    private GameObject _result;

    public Oven(Dictionary<string, FromOven> dictionaryProductName, Heroik heroik, Transform positionResult, Transform parentResult,OvenView ovenView)
    {
        _dictionaryProductName = dictionaryProductName;
        _heroik = heroik;
        _positionResult = positionResult;
        _parentResult = parentResult;
        _ovenView = ovenView;
        
        EventBus.PressE += CookingProcess;
        Debug.Log("Создать объект: Oven");
    }
    
    public void Dispose()
    {
        EventBus.PressE -= CookingProcess;
        Debug.Log("У объекта вызван Dispose : Oven");
    }
    public GameObject GiveObj(ref GameObject giveObj)
    {
        GameObject giveObjCopy = Object.Instantiate(giveObj);
        giveObjCopy.SetActive(false);
        giveObjCopy.name = giveObjCopy.name.Replace("(Clone)", "");
        DeleteObj(giveObj);
        return giveObjCopy;
    }

    public void AcceptObject(GameObject obj)
    {
        _ingredient = obj;
        _ingredient = Object.Instantiate(_ingredient);
        _ingredient.name = _ingredient.name.Replace("(Clone)", "");
        _ingredient.SetActive(false);
        Object.Destroy(obj);
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
        _ovenView.TurnOn();
    }

    public void TurnOff()
    {
        _isWork = false;
        _ovenView.TurnOff();
        Object.Destroy(_ingredient);
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
            if (_heroik.IsBusyHands == false) // руки не заняты
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
    
    private void DeleteObj(GameObject obj)
    {
        obj.SetActive(false);
        Object.Destroy(obj);
        obj = null;
    }
    
}
