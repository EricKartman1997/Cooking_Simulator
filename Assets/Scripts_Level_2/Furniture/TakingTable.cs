using System.Collections.Generic;
using UnityEngine;

public class TakingTable : MonoBehaviour,IAcceptObject,IGiveObj,IIsAllowDestroy,IHeroikIsTrigger
{
    //[SerializeField] private GameObject[] objectOnTheTable;
    [SerializeField] private GameObject _currentObjectOnTable;
    [SerializeField] private Transform _takingTablePoint;
    [SerializeField] private Transform _parentFood;
    [SerializeField] private List<GameObject> _unusableObjects;
    
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private float _timeCurrent = 0.17f;
    [SerializeField] private GameObject _cloneCurrentObjectOnTable;
    private bool _heroikIsTrigger = false;

    public void Initialize(Heroik heroik,Transform takingTablePoint,Transform parentFood, List<GameObject> unusableObjects)
    {
        //_currentObjectOnTable = currentObjectOnTable;
        _heroik = heroik;
        _takingTablePoint = takingTablePoint;
        _parentFood = parentFood;
        _unusableObjects = unusableObjects;
    }

    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.E) && _heroikIsTrigger)
        {
            if (_timeCurrent >= 0.17f)
            {
                if(!Heroik.IsBusyHands) // руки не заняты
                {
                    if (_currentObjectOnTable == null) // ни одного активного объекта
                    {
                        Debug.Log("У вас пустые руки и прилавок пуст");
                    }
                    else // на столе что-то есть
                    {
                        _heroik.ActiveObjHands(GiveObj(ref _cloneCurrentObjectOnTable));
                    }
                }
                else // заняты
                {
                    if (_currentObjectOnTable == null) // ни одного активного объекта
                    {
                        if (_heroik.CheckObjForReturn(_unusableObjects))
                        {
                            AcceptObject(_heroik.GiveObjHands());
                        }
                        else
                        {
                            Debug.Log("Этот предмет положить нельзя");
                        }
                    }
                    else // активного объект есть
                    {
                        Debug.Log("У вас полные руки и прилавок полон");
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
    

    public void AcceptObject(GameObject acceptObj)
    {
        _currentObjectOnTable = acceptObj;
        _cloneCurrentObjectOnTable = Instantiate(_currentObjectOnTable, _takingTablePoint.position, Quaternion.identity, _parentFood);
        _cloneCurrentObjectOnTable.name = _cloneCurrentObjectOnTable.name.Replace("(Clone)", "");
        _cloneCurrentObjectOnTable.SetActive(true);
        
        
    }
    
    public GameObject GiveObj(ref GameObject obj)
    {
        obj.SetActive(false);
        GameObject cObj = obj;
        Destroy(obj);
        _currentObjectOnTable = null;
        return cObj;
    }

    private bool CheckAcceptObject(GameObject acceptObj)
    {
        List<string> _unusableObjectsNames = new List<string>();
        foreach (var fruit in _unusableObjects)
        {
            _unusableObjectsNames.Add(fruit.name); // Используем имя объекта
        }
        if (_unusableObjectsNames.Contains(acceptObj.name))
        {
            return false;
        }
        return true;
    }

    public bool IsAllowDestroy()
    {
        if (_currentObjectOnTable == null)
        {
            return true;
        }
        return false;
    }

    public void HeroikIsTrigger()
    {
        _heroikIsTrigger = !_heroikIsTrigger;
    }
}
