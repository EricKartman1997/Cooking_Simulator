using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class TimerNew : MonoBehaviour, IPause
{
    [SerializeField] private GameObject arrow;
    
    private float _time;
    private float _currentTime;
    private bool _isWork;
    private bool _isFinish;
    
    private bool _isPause;
    private IHandlerPause _pauseHandler;

    [Inject]
    private void ConstructZenject(IHandlerPause pauseHandler)
    {
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
    }
    
    private void OnDisable()
    {
        _currentTime = 0;
        _isWork = false;
    }

    private void OnDestroy()
    {
        _pauseHandler.Remove(this);
    }
    
    
    public void SetParentTimer(Transform parent)
    {
        gameObject.transform.SetParent(parent);
    }
    
    public async UniTask StartTimerAsync(float time,float currentTime)
    {
        _time = time;
        _currentTime = currentTime;
        
        _isWork = true;
        gameObject.SetActive(true);

        while (_currentTime < _time)
        {
            _currentTime += Time.deltaTime;

            float progress = _currentTime / _time;
            float angle = 360f * progress;

            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
            
            await UniTask.WaitUntil(() => _isPause == false);
            await UniTask.Yield();
        }
        _isWork = false;
        gameObject.SetActive(false);
    }
    
    public void SetPause(bool isPaused) => _isPause = isPaused;
}
