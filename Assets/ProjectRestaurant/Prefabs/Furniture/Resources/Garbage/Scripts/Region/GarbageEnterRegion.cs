using UnityEngine;

public class GarbageEnterRegion : MonoBehaviour
{
    private Outline _outline;
    private Garbage _script;
    private Heroik _heroik;
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
            if (!GetComponent<Garbage>())
            {
                _script = gameObject.AddComponent<Garbage>();
                _script.HeroikIsTrigger();
                _script.Initialize(_heroik);
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
            Destroy(_script);
            Debug.Log("скрипт был удален");
        }
    }
}
