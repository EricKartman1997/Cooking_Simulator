using UnityEngine;
using Zenject;

public class HeroikSoundsControl : MonoBehaviour, IPause
{
    [SerializeField] private AudioSource audioSource;
    
    private SoundsServiceGameplay _soundsServiceGameplay;
    private bool _isPause;
    private IHandlerPause _pauseHandler;
    private AudioStateMachinePlayer _audioStateMachinePlayer;
    
    public bool IsPause => _isPause;

    [Inject]
    private void Construct(
        SoundsServiceGameplay soundsServiceGameplay,
        IHandlerPause handlerPause)
    {
        _pauseHandler = handlerPause;
        _soundsServiceGameplay = soundsServiceGameplay;
    }

    private void Awake()
    {
        _audioStateMachinePlayer = new AudioStateMachinePlayer(this,GetComponent<PlayerController>());
    }

    private void Update()
    {
        _audioStateMachinePlayer.Update();
    }

    private void OnEnable()
    {
        _pauseHandler.Add(this);
    }

    private void OnDisable()
    {
        _pauseHandler.Remove(this);
    }

    public void PlayOneShotClip(AudioNameGamePlay nameClip)
    {
        AudioClip clip = _soundsServiceGameplay.AudioDictionary[nameClip];
        audioSource.PlayOneShot(clip);
    }
    
    public void PlayClip(AudioNameGamePlay nameClip)
    {
        AudioClip clip = _soundsServiceGameplay.AudioDictionary[nameClip];
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    public void StopClip()
    {
        if(audioSource.clip != null)
            audioSource.Stop();
        
        audioSource.clip = null;
        audioSource.loop = false;
    }

    public void SetPause(bool isPaused) => _isPause = isPaused;
}
