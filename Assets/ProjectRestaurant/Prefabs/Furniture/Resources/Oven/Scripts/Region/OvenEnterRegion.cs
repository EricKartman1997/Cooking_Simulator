using System.Collections.Generic;
using UnityEngine;

public class OvenEnterRegion : MonoBehaviour
{
    [SerializeField] private GameObject glassOn;
    [SerializeField] private GameObject glassOff;
    [SerializeField] private GameObject switchFirst;
    [SerializeField] private GameObject switchSecond;
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    
    [SerializeField] private Transform positionResult;
    [SerializeField] private Transform parentResult;
    [SerializeField] private ProductsContainer productsContainer;
    private Heroik _heroik;
    
    private Outline _outline;
    private Oven _oven;
    private OvenView _ovenView;
    private bool _isCreateOven = false;
    
    void Start()
    {
        _outline = GetComponent<Outline>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            if (_isCreateOven == false)
            {
                // в будущем сделать в фабрике
                _ovenView = new OvenView(glassOn, glassOff, switchFirst, switchSecond, timer, timerPoint, timerParent);
                _oven = new Oven(productsContainer,_heroik,positionResult,parentResult,_ovenView);
                _oven.HeroikIsTrigger();
                _isCreateOven = true;
            }
            else
            {
                _oven.HeroikIsTrigger();
                Debug.Log("Новый скрипт создан не был");
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _oven.HeroikIsTrigger();
            _heroik = null;
            _outline.OutlineWidth = 0f;
            if (_oven.IsAllowDestroy())
            {
                _ovenView.Dispose();
                _ovenView = null;
            
                _oven.Dispose();
                _oven = null;
            
                _isCreateOven = false;
            }
        }
    }
}
