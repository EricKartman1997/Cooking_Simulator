using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OvenFurniture
{
    public class OvenView : IDisposable
    {
        private const string ANIMATIONCLOSE = "Close";
        private const string ANIMATIONOPEN = "Open";
        private GameObject _switchFirst;
        private GameObject _switchSecond;
        private TimerFurniture _timer;
        private Animator _animator;

        public TimerFurniture Timer => _timer;
         
        internal OvenView(GameObject switchFirst, GameObject switchSecond,TimerFurniture timerFurniture, Animator animator)
        {
            _switchFirst = switchFirst;
            _switchSecond = switchSecond;
            _timer = timerFurniture;
            _animator = animator;
             
            _animator.SetBool(ANIMATIONCLOSE,false);
            _animator.SetBool(ANIMATIONOPEN,true);
            //Debug.Log("Создан объект: OvenView");
        }
    
        public void Dispose()
        {
            Debug.Log("У объекта вызван Dispose : OvenView");
        }
        
        public async UniTask StartOvenAsync()
        {
            ActiveView();

            await _timer.StartTimerAsync(); // ждём завершения таймера

            PassiveView();
        }
         
        public void TurnOn()
        {
            ActiveView();
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

}
