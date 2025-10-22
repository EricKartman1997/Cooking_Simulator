using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BlenderFurniture
{
    public class BlenderView : IDisposable
    {
        private TimerFurniture _timer;
        private Animator _animator;
    
        public TimerFurniture Timer => _timer;
        
        internal BlenderView(TimerFurniture timer, Animator animator)
        {
            _timer = timer;
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
        }
    
        public void TurnOff()
        {
            _animator.SetBool("Work", false);
        }
    
    
    }
}

