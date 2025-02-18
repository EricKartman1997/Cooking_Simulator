using UnityEngine;

public class DonationTable : Furniture
{
    [SerializeField] private GameObject objectOnTheTable;
    
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private float _timeCurrent = 0.17f;

    public void Initialize(GameObject objectOnTheTable,Heroik _heroik)
    {
        this.objectOnTheTable = objectOnTheTable;
        this._heroik = _heroik;
    }
    
    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (_timeCurrent >= 0.17f)
            {
                if (!Heroik.IsBusyHands) //объект есть на столе, руки незаняты
                {
                    _heroik.ActiveObjHands(objectOnTheTable);
                }
                else// объект есть на столе,руки заняты
                {
                    if(objectOnTheTable.name != _heroik._curentTakenObjects.name)
                    {
                        Debug.Log($"Объект есть на столе: {objectOnTheTable.name}, руки заняты:{Heroik.IsBusyHands}");
                        Debug.Log($"вы пытаетесь взять объект {objectOnTheTable.name}, когда у вас в руках {_heroik._curentTakenObjects.name}");
                    }
                    else
                    {
                        Debug.Log($"Объект есть на столе: {objectOnTheTable.name}, руки заняты:{Heroik.IsBusyHands}");
                        Debug.Log("вы пытаетесь взять один и тот же объект");
                    }
                }
                _timeCurrent = 0f;
            }
            else
            {
                Debug.LogWarning("Ждите перезарядки кнопки");
            }
        }
    }
    
}
