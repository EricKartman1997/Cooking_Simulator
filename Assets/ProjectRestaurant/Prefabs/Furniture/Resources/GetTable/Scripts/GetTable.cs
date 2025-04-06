using UnityEngine;

public class GetTable : MonoBehaviour, IGiveObj
{
    private GetTableConfig _getTableConfig;
    private GameObject _objectOnTheTable;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...

    public void Initialize(Heroik heroik,GetTableConfig getTableConfig)
    {
        _getTableConfig = getTableConfig;
        _heroik = heroik;

        
        _objectOnTheTable = StaticManagerWithoutZenject.ProductsFactory.GetProductRef(_getTableConfig.GiveFood);
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
        if (_heroik.IsBusyHands == false) //объект есть на столе, руки незаняты
        {
            _heroik.ActiveObjHands(GiveObj(ref _objectOnTheTable));
        }
        else// объект есть на столе,руки заняты
        {
            Debug.Log("объект есть на столе,руки заняты");
        }
    }
    
}
