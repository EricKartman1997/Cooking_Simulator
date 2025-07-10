using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Checks : MonoBehaviour
{
    [SerializeField] private CheckContainer _checkContainer;
    [SerializeField] private GameObject _content;
    
     private InfoAboutCheck _check1;
     private InfoAboutCheck _check2;
     private InfoAboutCheck _check3;
     private GameObject _cloneCheck1;
     private GameObject _cloneCheck2;
     private GameObject _cloneCheck3;

    private void OnEnable()
    {
        EventBus.DeleteCheck += DeleteOverdueCheck;
    }

    private void OnDisable()
    {
        EventBus.DeleteCheck -= DeleteOverdueCheck;
    }

    public void AddCheck() // добавление чека
    {
        InfoAboutCheck curentCheck = _checkContainer.AllPrefChecks[Random.Range(0, _checkContainer.AllPrefChecks.Count)];
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
    
    public void DeleteCheck(InfoAboutCheck check) // удаление чека
    {
        if (_cloneCheck1.GetComponent<InfoAboutCheck>() == check)
        {
            EventBus.AddScore.Invoke(0,_check1.GetScore());
            _check1 = null;
            Destroy(_cloneCheck1);
            _cloneCheck1 = null;
            EventBus.AddOrder.Invoke();
            EventBus.UpdateOrder.Invoke();
            return;
        }
        
        if (_cloneCheck2.GetComponent<InfoAboutCheck>() == check)
        {
            EventBus.AddScore.Invoke(0,_check2.GetScore());
            _check2 = null;
            Destroy(_cloneCheck2);
            _cloneCheck2 = null;
            EventBus.AddOrder.Invoke();
            EventBus.UpdateOrder.Invoke();
            return;
        }
        
        if (_cloneCheck3.GetComponent<InfoAboutCheck>() == check)
        {
            EventBus.AddScore.Invoke(0,_check3.GetScore());
            _check3 = null;
            Destroy(_cloneCheck3);
            _cloneCheck3 = null;
            EventBus.AddOrder.Invoke();
            EventBus.UpdateOrder.Invoke();
            return;
        }
        
        Debug.LogError("ошибка DeleteCheck");
        
    }

    public InfoAboutCheck CheckTheCheck(GameObject dish)
    {
        List<GameObject> allChecks = new List<GameObject>() {_cloneCheck1,_cloneCheck2,_cloneCheck3};
        InfoAboutCheck targetCheck = null;
        float minTime = float.MaxValue;

        foreach (var check in allChecks)
        {
            if (check == null) continue;

            // Находим чек для текущего блюда с минимальным временем
            if (check.GetComponent<InfoAboutCheck>().GetDish() == dish.name && check.GetComponent<InfoAboutCheck>().StartTime < minTime)
            {
                targetCheck = check.GetComponent<InfoAboutCheck>();
            }
        }

        return targetCheck;
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
    
    private void DeleteOverdueCheck(InfoAboutCheck check) // удаление просроченного чека
    {
        if (_check1 != null && _cloneCheck1.GetComponent<InfoAboutCheck>().StartTime <= 0f)
        {
            _check1 = null;
            Destroy(_cloneCheck1);
            _cloneCheck1 = null;

            //Debug.Log("просрочен 1 чек");
        }
        else if (_check2 != null && _cloneCheck2.GetComponent<InfoAboutCheck>().StartTime <= 0f)
        {
            _check2 = null;
            Destroy(_cloneCheck2);
            _cloneCheck2 = null;
            
            //Debug.Log("просрочен 2 чек");

        }
        else if (_check3 != null && _cloneCheck3.GetComponent<InfoAboutCheck>().StartTime <= 0f)
        {
            _check3 = null;
            Destroy(_cloneCheck3);
            _cloneCheck3 = null;

            //Debug.Log("просрочен 3 чек");
        }
        else
        {
            Debug.Log("ошибка DeleteOverdueCheck");
        }
        
    }
}
