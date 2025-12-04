using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SuvideFurniture
{
    public class SuvideView : IDisposable, IPause
    {
        private GameObject _waterPrefab;
        private GameObject _switchTimePrefab;
        private GameObject _switchTemperPrefab;
        private TimerFurniture _firstTimer;
        private TimerFurniture _secondTimer;
        private TimerFurniture _thirdTimer;
        private Animator _animator; // добавить анимацию
        private IHandlerPause _pauseHandler;
    
        internal SuvideView(GameObject waterPrefab, GameObject switchTimePrefab, GameObject switchTemperPrefab,
            TimerFurniture firstTimer, TimerFurniture secondTimer, TimerFurniture thirdTimer, Animator animator, IHandlerPause pauseHandler)
        {
            _waterPrefab = waterPrefab;
            _switchTimePrefab = switchTimePrefab;
            _switchTemperPrefab = switchTemperPrefab;
            _firstTimer = firstTimer;
            _secondTimer = secondTimer;
            _thirdTimer = thirdTimer;
            _animator = animator;
            _pauseHandler = pauseHandler;
            _pauseHandler.Add(this);
            
            //Debug.Log("Создал объект: SuvideView");
        }
    
        public void Dispose()
        {
            Debug.Log("У объекта вызван Dispose : SuvideView");
        }
        
        public void TurnOn() 
        {
            //_animator.SetBool("Work", true);
        }
    
        public void TurnOff() 
        {
            //_animator.SetBool("Work", false);
        }
        
        public async UniTask StartSuvideFirstTimerAsync()
        {
            WorkingSuvide();

            await _firstTimer.StartTimerAsync(); // ждём завершения таймера

            NotWorkingSuvide();
        }
        
        public async UniTask StartSuvideSecondTimerAsync()
        {
            WorkingSuvide();

            await _secondTimer.StartTimerAsync(); // ждём завершения таймера

            NotWorkingSuvide();
        }
        
        public async UniTask StartSuvideThirdTimerAsync()
        {
            WorkingSuvide();

            await _thirdTimer.StartTimerAsync(); // ждём завершения таймера

            NotWorkingSuvide();
        }
        
        public void WorkingSuvide()
        {
            _waterPrefab.SetActive(true);
            _switchTemperPrefab.transform.localRotation = Quaternion.Euler(-135, 0, 0);
            _switchTimePrefab.transform.localRotation = Quaternion.Euler(-60, 0, 0);
        }
        public void NotWorkingSuvide()
        {
            _waterPrefab.SetActive(false);
            _switchTemperPrefab.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _switchTimePrefab.transform.localRotation = Quaternion.Euler(0, 0, 0);
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
