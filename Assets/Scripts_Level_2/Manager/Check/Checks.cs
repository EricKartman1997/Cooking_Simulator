using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Checks : MonoBehaviour
{
    //Initialized
    [SerializeField] private List<InfoAboutCheck> _allPrefChecks;
    [SerializeField] private GameObject _content;
    
    [SerializeField] private InfoAboutCheck _check1 = null;
    [SerializeField] private InfoAboutCheck _check2 = null;
    [SerializeField] private InfoAboutCheck _check3 = null;
    [SerializeField] private GameObject _cloneCheck1 = null;
    [SerializeField] private GameObject _cloneCheck2 = null;
    [SerializeField] private GameObject _cloneCheck3 = null;
    
    public void Initialized(List<InfoAboutCheck> allPrefChecks,GameObject content)
    {
        _allPrefChecks = allPrefChecks;
        _content = content;
    }

    public void AddCheck() // добавление чека
    {
        InfoAboutCheck curentCheck = _allPrefChecks[Random.Range(0, _allPrefChecks.Count)];
        if (_check1 == null)
        {
            _check1 = curentCheck;
            _cloneCheck1 = Instantiate(curentCheck.gameObject, _content.transform);
        }
        else if (_check2 == null)
        {
            _check2 = curentCheck;
            _cloneCheck2 = Instantiate(curentCheck.gameObject, _content.transform);
        }
        else if (_check3 == null)
        {
            _check3 = curentCheck;
            _cloneCheck3 = Instantiate(curentCheck.gameObject, _content.transform);
        }
        else
        {
            Debug.LogWarning("Чек добавлен не был - чеки полные");
        }
    }

    public void DeleteCheck(GameObject dish) // удаление чека
    {
        if (_check1 != null && _check1.GetDish() == dish.name)
        {
            EventBus.AddScore.Invoke(100,_check1.GetScore());
            _check1 = null;
            Destroy(_cloneCheck1);
            _cloneCheck1 = null;
            EventBus.AddOrder.Invoke();
            EventBus.UpdateOrder.Invoke();
            //Debug.Log("удалил первый чек");
        }
        else if (_check2 != null && _check2.GetDish() == dish.name)
        {
            EventBus.AddScore.Invoke(100,_check2.GetScore());
            _check2 = null;
            Destroy(_cloneCheck2);
            _cloneCheck2 = null;
            EventBus.AddOrder.Invoke();
            EventBus.UpdateOrder.Invoke();
            //Debug.Log("удалил второй чек");
        }
        else if (_check3 != null && _check3.GetDish() == dish.name)
        {
            EventBus.AddScore.Invoke(100,_check3.GetScore());
            _check3 = null;
            Destroy(_cloneCheck3);
            _cloneCheck3 = null;
            EventBus.AddOrder.Invoke();
            EventBus.UpdateOrder.Invoke();
            //Debug.Log("удалил третий чек");
        }
        else
        {
            Debug.Log("ошибка DeleteCheck");
        }
        
    }

    public bool CheckTheCheck(GameObject dish) // проверка есть ли в чеках заказанное блюдо
    {
        List<InfoAboutCheck> allChecks = new List<InfoAboutCheck>() {_check1,_check2,_check3};
        List<InfoAboutCheck> allChecksNotNull = new List<InfoAboutCheck>() {};
        foreach (var check in allChecks)
        {
            if (check != null)
            {
                allChecksNotNull.Add(check);
                Debug.Log("Добавил");
            }
        }
        foreach (var check in allChecksNotNull)
        {
            if (check.GetDish() == dish.name)
            {
                return true;
            }
        }
        return false;
    }

    public InfoAboutCheck GetCheck1()
    {
        return _check1;
    }
    
    public InfoAboutCheck GetCheck2()
    {
        return _check2;
    }
    
    public InfoAboutCheck GetCheck3()
    {
        return _check3;
    }

    public void DeleteAllChecks()
    {
        Destroy(_check1);
        _check1 = null;
        //Debug.Log("удалил первый чек");
        
        Destroy(_check2);
        _check2 = null;
        //Debug.Log("удалил второй чек");
        
        Destroy(_check3);
        _check3 = null;
        //Debug.Log("удалил третий чек");
    }
}
