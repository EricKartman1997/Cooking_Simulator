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

    public GameObject GiveObj(ref GameObject giveObj)
    {
        GameObject giveObjCopy = Instantiate(giveObj);
        giveObjCopy.SetActive(false);
        giveObjCopy.name = giveObjCopy.name.Replace("(Clone)", "");
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
