using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class BlenderView : IDisposable
{
    private GameObject _timer;
    private Transform _timerPoint;
    private Transform _timerParent;
    private Animator _animator;

    public BlenderView(GameObject timer, Transform timerPoint, Transform timerParent, Animator animator)
    {
        _timer = timer;
        _timerPoint = timerPoint;
        _timerParent = timerParent;
        _animator = animator;
        
        Debug.Log("Создал объект: BlenderView");
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : BlenderView");
    }
    
    public void TurnOn()
    {
        _animator.SetBool("Work", true);
        Object.Instantiate(_timer, _timerPoint.position, Quaternion.identity,_timerParent);
    }

    public void TurnOff()
    {
        _animator.SetBool("Work", false);
    }


}
