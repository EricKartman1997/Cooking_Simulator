using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BlenderFurniture
{
    public class BlenderView : IDisposable, IPause
    {
        private TimerFurniture _timer;
        private Animator _animator;
        
        private IHandlerPause _pauseHandler;

        public TimerFurniture Timer => _timer;

        internal BlenderView(TimerFurniture timer, Animator animator,IHandlerPause pauseHandler)
        {
            _timer = timer;
            _animator = animator;
            _pauseHandler = pauseHandler;
            _pauseHandler.Add(this);
        }

        public async UniTask StartBlendAsync()
        {
            _animator.SetBool("Work", true);

            await _timer.StartTimerAsync(); // ждём завершения таймера

            _animator.SetBool("Work", false);
        }

        public void Dispose()
        {
            _pauseHandler.Remove(this);
            Debug.Log("BlenderView.Dispose");
        }
        
        public void SetPause(bool isPaused)
        {
            if (isPaused == true)
            {
                _animator.speed = 0f;
                return;
            }
            _animator.speed = 1;
            
        }
    }

}

