using System;
using UnityEngine;
public class Garbage : MonoBehaviour, IAcceptObject
{
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private bool _isHeroikTrigger;
    private GameObject _obj;
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    
    void Start()
    {
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
    
    public void AcceptObject(GameObject acceptObj)
    {
        _obj = acceptObj;
        Destroy(acceptObj);
    }
    
    private void CookingProcess()
    {
        if (_isHeroikTrigger == false)
        {
            return;
        }
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            Debug.LogWarning("Мусорка не работает");
            return;
        }
        
        try
        { 
            AcceptObject(_heroik.TryGiveIngredient());
            DeleteObj();
        }
        catch (Exception e)
        {
            Debug.Log("Вам нечего выкидывать" + e);
        }
    }
    
    private void DeleteObj()
    {
        _obj.SetActive(false);
        Destroy(_obj);
        _obj = null;
    }
    
}
