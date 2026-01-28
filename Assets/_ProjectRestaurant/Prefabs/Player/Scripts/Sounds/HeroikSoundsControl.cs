using System;
using UnityEngine;
using Zenject;

public class HeroikSoundsControl : MonoBehaviour, IPause
{
    [SerializeField] private AudioSource audioSource;

    private ObservableAudioSource _observable;
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
        audioSource.outputAudioMixerGroup = _soundsServiceGameplay.SoundManager.SFXGroup;

        _observable = new ObservableAudioSource(audioSource);
        _observable.OnClipChanged += OnClipChanged;
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
        _observable.OnClipChanged -= OnClipChanged;
    }

    public void PlayOneShotClip(AudioNameGamePlay nameClip)
    {
        AudioClip clip = _soundsServiceGameplay.AudioDictionary[nameClip];
        audioSource.PlayOneShot(clip);
    }
    
    public void PlayClip(AudioNameGamePlay name)
    {
        AudioClip clip = _soundsServiceGameplay.AudioDictionary[name];

        _observable.Clip = clip;
        audioSource.loop = true;

        if (!_isPause)
            _observable.Play();
    }

    
    public void StopClip()
    {
        if(audioSource.clip != null)
            audioSource.Stop();
        
        audioSource.clip = null;
        audioSource.loop = false;
    }

    public void SetPause(bool isPaused)
    {
        _isPause = isPaused;

        if (isPaused)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
    
    private void OnClipChanged(AudioClip clip)
    {
        if (_isPause)
        {
            audioSource.Pause();
            return;
        }

        audioSource.Play();
    }
}

public class ObservableAudioSource
{
    public event Action<AudioClip> OnClipChanged;

    private readonly AudioSource _source;
    private AudioClip _lastClip;

    public ObservableAudioSource(AudioSource source)
    {
        _source = source;
    }

    public AudioClip Clip
    {
        get => _source.clip;
        set
        {
            _source.clip = value;
            if (_lastClip != value)
            {
                _lastClip = value;
                OnClipChanged?.Invoke(value);
            }
        }
    }

    public void Play()
    {
        _source.Play();
    }

    public void Pause()
    {
        _source.Pause();
    }
}

