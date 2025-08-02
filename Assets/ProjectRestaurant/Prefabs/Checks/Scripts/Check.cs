using System;
using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Check: IDisposable
{
    public event Action StopTimeEvent; 
    
    private GameObject _prefab;
    private float _startTime;
    private float _score;
    private GameObject _dish;
    
    
    public GameObject Prefab => _prefab;
    
    public float StartTime => _startTime;
    
    public float Score => _score;
    
    public GameObject Dish => _dish;
    

    protected Check(GameObject prefab, float startTime, float score, GameObject dish)
    {
        _prefab = prefab;
        _startTime = startTime;
        _score = score;
        _dish = dish;
    }
    
    public void Dispose()
    {
        
    }

    public abstract void Accept(ICheckVisitor checkVisitor);
    
    
    public IEnumerator UpdateTime(TextMeshProUGUI remTimeText)
    {

        while (_startTime > 0)
        {
            _startTime -= Time.deltaTime;
            remTimeText.text = string.Format("{0:00}:{1:00}", 0f,  _startTime);
            yield return null;
        }
        Debug.Log("Чек удален");
        EventBus.DeleteCheck.Invoke(this); // разобратся

    }
    
    public void StopUpdateTime()
    {
        StopTimeEvent.Invoke();
    }


}
