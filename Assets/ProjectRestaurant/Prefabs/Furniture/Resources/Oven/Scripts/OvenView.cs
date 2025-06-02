using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class OvenView : IDisposable
{
    private const string ANIMATIONCLOSE = "Close";
    private const string ANIMATIONOPEN = "Open";
    
     private GameObject _switchFirst;
     private GameObject _switchSecond;
     private NewTimer _timer;
     private Animator _animator;

     public OvenView(GameObject switchFirst, GameObject switchSecond, NewTimer timer,Animator animator)
     {
         _switchFirst = switchFirst;
         _switchSecond = switchSecond;
         _timer = timer;
         _animator = animator;
         
         _animator.SetBool(ANIMATIONCLOSE,false);
         _animator.SetBool(ANIMATIONOPEN,true);
         
         Debug.Log("Создан объект: OvenView");
     }

     public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : OvenView");
    }
     
    public void TurnOn()
    {
        ActiveView();
        _timer.gameObject.SetActive(true);
        //Object.Instantiate(_timer, _timerPoint.position, Quaternion.identity,_timerParent);
    }

    public void TurnOff()
    {
        PassiveView();
    }

    private void ActiveView()
    {
        _animator.SetBool(ANIMATIONCLOSE,true);
        _animator.SetBool(ANIMATIONOPEN,false);
        _switchFirst.transform.localRotation = Quaternion.Euler(55, 0, 0);
        _switchSecond.transform.localRotation = Quaternion.Euler(-85, 0, 0);
    }
    
    private void PassiveView()
    {
        _animator.SetBool(ANIMATIONCLOSE,false);
        _animator.SetBool(ANIMATIONOPEN,true);
        _switchFirst.transform.localRotation = Quaternion.Euler(0, 0, 0);
        _switchSecond.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
