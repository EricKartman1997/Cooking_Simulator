using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class SuvideView : IDisposable
{
    private GameObject _waterPrefab;
    private GameObject _switchTimePrefab;
    private GameObject _switchTemperPrefab;
    private HelperTimer _firstTimer;
    private HelperTimer _secondTimer;
    private HelperTimer _thirdTimer;
    private Animator _animator; // добавить анимацию

    public SuvideView(GameObject waterPrefab, GameObject switchTimePrefab, GameObject switchTemperPrefab, HelperTimer firstTimer, HelperTimer secondTimer, HelperTimer thirdTimer, Animator animator)
    {
        _waterPrefab = waterPrefab;
        _switchTimePrefab = switchTimePrefab;
        _switchTemperPrefab = switchTemperPrefab;
        _firstTimer = firstTimer;
        _secondTimer = secondTimer;
        _thirdTimer = thirdTimer;
        _animator = animator;
        
        Debug.Log("Создал объект: SuvideView");
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : SuvideView");
    }
    
    public void TurnOnFirstTimer() 
    {
        //_animator.SetBool("Work", true);
        Object.Instantiate(_firstTimer.timer, _firstTimer.timerPoint.position, Quaternion.identity,_firstTimer.timerParent);
    }
    
    public void TurnOnSecondTimer() 
    {
        //_animator.SetBool("Work", true);
        Object.Instantiate(_secondTimer.timer, _secondTimer.timerPoint.position, Quaternion.identity,_secondTimer.timerParent);
    }
    
    public void TurnOnThirdTimer() 
    {
        //_animator.SetBool("Work", true);
        Object.Instantiate(_thirdTimer.timer, _thirdTimer.timerPoint.position, Quaternion.identity,_thirdTimer.timerParent);
    }

    public void TurnOff() 
    {
        //_animator.SetBool("Work", false);
    }
    
    public void WorkingSuvide()
    {
        _waterPrefab.SetActive(true);
        _switchTemperPrefab.transform.rotation = Quaternion.Euler(45, -90, -90);
        _switchTimePrefab.transform.rotation = Quaternion.Euler(175, -90, -90);
    }
    public void NotWorkingSuvide()
    {
        _waterPrefab.SetActive(false);
        _switchTemperPrefab.transform.rotation = Quaternion.Euler(90, -90, -90);
        _switchTimePrefab.transform.rotation = Quaternion.Euler(90, -90, -90);
    }
}
