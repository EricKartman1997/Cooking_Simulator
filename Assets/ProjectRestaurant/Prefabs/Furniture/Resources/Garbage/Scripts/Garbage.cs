using System;
using UnityEngine;
public class Garbage :MonoBehaviour, IAcceptObject
{
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private bool _heroikIsTrigger;
    private GameObject _obj;
    
    public void Initialize(Heroik _heroik)
    {
        this._heroik = _heroik;
    }
    
    private void OnEnable()
    {
        EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        EventBus.PressE -= CookingProcess;
    }
    
    public void HeroikIsTrigger()
    {
        _heroikIsTrigger = !_heroikIsTrigger;
    }

    public void AcceptObject(GameObject acceptObj)
    {
        _obj = acceptObj;
    }
    
    private void CookingProcess()
    {
        if (_heroikIsTrigger == true)
        {
            try
            { 
                AcceptObject(_heroik.GiveObjHands());
            }
            catch (Exception e)
            {
                Debug.Log("Вам нечего выкидывать" + e);
            }
        }
    }
}
