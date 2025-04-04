using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class CuttingTableView : IDisposable
{
    private Animator _animator;
    private GameObject _timer;
    private Transform _timerPoint;
    private Transform _timerParent;

    public CuttingTableView(Animator animator, GameObject timer, Transform timerPoint, Transform timerParent)
    {
        _animator = animator;
        _timer = timer;
        _timerPoint = timerPoint;
        _timerParent = timerParent;
        
        Debug.Log("Создать объект: CuttingTableView");
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : CuttingTableView");
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
