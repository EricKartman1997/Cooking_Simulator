using UnityEngine;

public class GetTable : MonoBehaviour, IGiveObj
{
    private GameObject _objectOnTheTable;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private float _timeCurrent = 0.17f;

    public void Initialize(GameObject objectOnTheTable,Heroik heroik)
    {
        _objectOnTheTable = objectOnTheTable;
        _heroik = heroik;
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
                    _heroik.ActiveObjHands(GiveObj(ref _objectOnTheTable));
                }
                else// объект есть на столе,руки заняты
                {
                    Debug.Log("объект есть на столе,руки заняты");
                }
                _timeCurrent = 0f;
            }
            else
            {
                Debug.LogWarning("Ждите перезарядки кнопки");
            }
        }
    }

    public GameObject GiveObj(ref GameObject obj)
    {
        return obj;
    }
}
