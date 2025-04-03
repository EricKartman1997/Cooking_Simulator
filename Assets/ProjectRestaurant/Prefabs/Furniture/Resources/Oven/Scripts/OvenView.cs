using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class OvenView : IDisposable
{
     private GameObject _glassOn;
     private GameObject _glassOff;
     private GameObject _switchFirst;
     private GameObject _switchSecond;
     private GameObject _timer;
     private Transform _timerPoint;
     private Transform _timerParent;

     public OvenView(GameObject glassOn, GameObject glassOff, GameObject switchFirst, GameObject switchSecond, GameObject timer, Transform timerPoint, Transform timerParent)
     {
         _glassOn = glassOn;
         _glassOff = glassOff;
         _switchFirst = switchFirst;
         _switchSecond = switchSecond;
         _timer = timer;
         _timerPoint = timerPoint;
         _timerParent = timerParent;
         
         Debug.Log("Создать объект: OvenView");
     }

     public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : OvenView");
    }
     
    public void TurnOn()
    {
        ActiveView();
        Object.Instantiate(_timer, _timerPoint.position, Quaternion.identity,_timerParent);
    }

    public void TurnOff()
    {
        PassiveView();
    }

    private void ActiveView()
    {
        _glassOff.SetActive(false);
        _glassOn.SetActive(true);
        _switchFirst.transform.rotation = Quaternion.Euler(0, 0, -90);
        _switchSecond.transform.rotation = Quaternion.Euler(0, 0, -135);
    }
    
    private void PassiveView()
    {
        _glassOff.SetActive(true);
        _glassOn.SetActive(false);
        _switchFirst.transform.rotation = Quaternion.Euler(0, 0, 0);
        _switchSecond.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
