using UnityEngine;

public class GetTable : MonoBehaviour, IGiveObj
{
    private GameObject _objectOnTheTable;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...

    public void Initialize(GameObject objectOnTheTable,Heroik heroik)
    {
        _objectOnTheTable = objectOnTheTable;
        _heroik = heroik;
    }
    
    private void OnEnable()
    {
        EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        EventBus.PressE -= CookingProcess;
    }

    public GameObject GiveObj(ref GameObject obj)
    {
        return obj;
    }
    
    private void CookingProcess()
    {
        if (!Heroik.IsBusyHands) //объект есть на столе, руки незаняты
        {
            _heroik.ActiveObjHands(GiveObj(ref _objectOnTheTable));
        }
        else// объект есть на столе,руки заняты
        {
            Debug.Log("объект есть на столе,руки заняты");
        }
    }
}
