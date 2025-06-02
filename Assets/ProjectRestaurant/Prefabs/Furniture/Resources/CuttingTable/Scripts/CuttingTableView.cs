using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class CuttingTableView : IDisposable
{
    private Animator _animator;
    private NewTimer _timer;
    
    public CuttingTableView(Animator animator, NewTimer timer)
    {
        _animator = animator;
        _timer = timer;
        
        Debug.Log("Создать объект: CuttingTableView");
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : CuttingTableView");
    }

    public void TurnOn()
    {
        _animator.SetBool("Work", true);
        _timer.gameObject.SetActive(true);
        //Object.Instantiate(_timer, _timerPoint.position, Quaternion.identity,_timerPoint);
    }
    
    public void TurnOff()
    {
        _animator.SetBool("Work", false);
    }
}
