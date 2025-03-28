using System.Collections.Generic;
using UnityEngine;

public class GiveTable : MonoBehaviour,IAcceptObject,IGiveObj,IIsAllowDestroy,IHeroikIsTrigger
{
    [SerializeField] private GameObject _ingredient;
    [SerializeField] private Transform _ingredientPoint;
    [SerializeField] private Transform _parentFood;
    [SerializeField] private List<GameObject> _unusableObjects;
    
    private Heroik _heroik;
    private bool _isHeroikTrigger = false;

    public void Initialize(Heroik heroik,Transform takingTablePoint,Transform parentFood, List<GameObject> unusableObjects)
    {
        _heroik = heroik;
        _ingredientPoint = takingTablePoint;
        _parentFood = parentFood;
        _unusableObjects = unusableObjects;
    }
    
    private void OnEnable()
    {
        EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        EventBus.PressE -= CookingProcess;
    }
    
    private void CookingProcess()
    {
        if(_isHeroikTrigger)
        {
            if(!Heroik.IsBusyHands) // руки не заняты
            {
                if (_ingredient == null) // ни одного активного объекта
                {
                    Debug.Log("У вас пустые руки и прилавок пуст");
                }
                else // на столе что-то есть
                {
                    _heroik.ActiveObjHands(GiveObj(ref _ingredient));
                }
            }
            else // заняты
            {
                if (_ingredient == null) // ни одного активного объекта
                {
                    if (_heroik.CheckObjForReturn(_unusableObjects))
                    {
                        AcceptObject(_heroik.GiveObjHands());
                    }
                    else
                    {
                        Debug.Log("Этот предмет положить нельзя");
                    }
                }
                else // активного объект есть
                {
                    Debug.Log("У вас полные руки и прилавок полон");
                }
            }
        }
    }
    
    public void AcceptObject(GameObject acceptObj)
    {
        _ingredient = acceptObj;
        _ingredient = Instantiate(_ingredient, _ingredientPoint.position, Quaternion.identity, _parentFood);
        _ingredient.name = _ingredient.name.Replace("(Clone)", "");
        _ingredient.SetActive(true);
    }
    
    public GameObject GiveObj(ref GameObject obj)
    {
        obj.SetActive(false);
        GameObject cObj = obj;
        Destroy(obj);
        _ingredient = null;
        return cObj;
    }

    public bool IsAllowDestroy()
    {
        if (_ingredient == null)
        {
            return true;
        }
        return false;
    }

    public void HeroikIsTrigger()
    {
        _isHeroikTrigger = !_isHeroikTrigger;
    }
    
}
