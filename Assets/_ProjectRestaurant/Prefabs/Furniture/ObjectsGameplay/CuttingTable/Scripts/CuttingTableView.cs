using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CuttingTableFurniture
{
    public class CuttingTableView : IDisposable
    {
        private Animator _animator;
        private TimerFurniture _timer;
    
        public TimerFurniture Timer => _timer;
    
        internal CuttingTableView(Animator animator, TimerFurniture timer)
        {
            _animator = animator;
            _timer = timer;
            
            //Debug.Log("Создать объект: CuttingTableView");
        }
        
        public void Dispose()
        {
            Debug.Log("У объекта вызван Dispose : CuttingTableView");
        }
        
        public async UniTask StartCuttingTableAsync()
        {
            _animator.SetBool("Work", true);

            await _timer.StartTimerAsync(); // ждём завершения таймера

            _animator.SetBool("Work", false);
        }
    }
}

