using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
        }

        public async UniTask StartBlendAsync()
        {
            _animator.SetBool("Work", true);

            await _timer.StartTimerAsync(); // ждём завершения таймера

            _animator.SetBool("Work", false);
        }

        public void Dispose()
        {
            Debug.Log("BlenderView.Dispose");
        }
    }

}

