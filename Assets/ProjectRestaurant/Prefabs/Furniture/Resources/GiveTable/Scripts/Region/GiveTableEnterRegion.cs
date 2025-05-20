using System.Collections.Generic;
using UnityEngine;

public class GiveTableEnterRegion : MonoBehaviour
{
    [SerializeField] private Transform _takingTablePoint;
    [SerializeField] private Transform _parentFood;
    [SerializeField] private List<GameObject> _unusableObjects;
    
    private Heroik _heroik;
    private Outline _outline;
    private GiveTable _script;
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
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            if (!GetComponent<GiveTable>())
            {
                _script = gameObject.AddComponent<GiveTable>();
                _script.HeroikIsTrigger();
                _script.Initialize(_heroik,_takingTablePoint,_parentFood,_unusableObjects);
            }
            else
            {
                _script.HeroikIsTrigger();
                Debug.Log("Новый скрипт создан не был");
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _script.HeroikIsTrigger();
            _heroik = null;
            _outline.OutlineWidth = 0f;
            if (_script.IsAllowDestroy())
            {
                Destroy(_script);
                Debug.Log("скрипты был удален");
            }
        }
    }
}
