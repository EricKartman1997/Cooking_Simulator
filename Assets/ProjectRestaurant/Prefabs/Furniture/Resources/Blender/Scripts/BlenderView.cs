using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BlenderFurniture
{
    public class BlenderView : IDisposable
    {
        private NewTimer _timer;
        private Animator _animator;
    
        public NewTimer Timer => _timer;
        
        internal BlenderView(NewTimer timer, Animator animator)
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
            //Object.Instantiate(_timer, _timerPoint.position, Quaternion.identity,_timerParent);
            _timer.gameObject.SetActive(true);
        }
    
        public void TurnOff()
        {
            _animator.SetBool("Work", false);
        }
    
    
    }
}

