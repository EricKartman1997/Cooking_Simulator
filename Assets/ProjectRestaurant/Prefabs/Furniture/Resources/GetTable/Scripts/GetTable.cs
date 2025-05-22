using UnityEngine;

public class GetTable : MonoBehaviour, IGiveObj
{
    [SerializeField] private GetTableConfig getTableConfig;
    [SerializeField] private Transform parentViewDish;
    
    private DecorationFurniture _decorationFurniture;
    private Outline _outline;
    private GameObject _objectOnTheTable;
    private Heroik _heroik; // только для объекта героя, а надо и другие...
    private bool _isHeroikTrigger;

    void Start()
    {
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
        
        StaticManagerWithoutZenject.ViewFactory.GetProduct(getTableConfig.FoodView,parentViewDish);
        _objectOnTheTable = StaticManagerWithoutZenject.ProductsFactory.GetProductRef(getTableConfig.GiveFood);
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

    public GameObject GiveObj(ref GameObject giveObj)
    {
        GameObject giveObjCopy = StaticManagerWithoutZenject.ProductsFactory.GetProduct(giveObj, false);
        return giveObjCopy;
    }
    
    private void CookingProcess()
    {
        if (_isHeroikTrigger == false)
        {
            return;
        }
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            Debug.LogWarning("Стол не работает");
            return;
        }

        if (_heroik.IsBusyHands == false) //объект есть на столе, руки незаняты
        {
            _heroik.ActiveObjHands(GiveObj(ref _objectOnTheTable));
        }
        else// объект есть на столе, руки заняты
        {
            Debug.Log("объект есть на столе,руки заняты");
        }
    }
    
}
