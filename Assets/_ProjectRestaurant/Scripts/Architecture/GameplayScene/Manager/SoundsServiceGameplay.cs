using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SoundsServiceGameplay : ISoundsService
{
    private SoundManager _soundManager;
    private DiContainer _container;
    private LoadReleaseGameplay _loadReleaseGameplay;
    private GameObject empty = new GameObject("Sounds_Test");
    private AudioSource _sourceSFX;
    private AudioSource _sourceMusic;
    
    public AudioSource SourceSfx => _sourceSFX;
    public AudioSource SourceMusic => _sourceMusic;
    public SoundManager SoundManager => _soundManager;
    public IReadOnlyDictionary<AudioNameGamePlay, AudioClip> AudioDictionary => _loadReleaseGameplay.AudioDic;
    
    public SoundsServiceGameplay(SoundManager soundManager,DiContainer container, LoadReleaseGameplay loadReleaseGameplay)
    {
        _soundManager = soundManager;
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
    }

    public void CreateSounds()
    {
        GameObject sound = _container.InstantiatePrefab(_loadReleaseGameplay.ServiceDic[ServiceNameGamePlay.SoundsObject], empty.transform);
        
        GameObject obj = sound.transform.Find("SFX")?.gameObject;
        GameObject obj1 = sound.transform.Find("Music")?.gameObject;

        _sourceSFX = obj.GetComponent<AudioSource>();
        _sourceMusic = obj1.GetComponent<AudioSource>();
        
        if (_sourceSFX.outputAudioMixerGroup == null)
        {
            Debug.LogError("BootstrapMainMenu SFX == null");
        }
        
        if (_sourceMusic.outputAudioMixerGroup == null)
        {
            Debug.LogError("BootstrapMainMenu Music == null");
        }
        
        _sourceMusic.outputAudioMixerGroup = _soundManager.MusicGroup;
        _sourceSFX.outputAudioMixerGroup = _soundManager.SFXGroup;
        
        _container.Bind<AudioSource>().WithId("SFX").FromInstance(_sourceSFX);
        _container.Bind<AudioSource>().WithId("Music").FromInstance(_sourceMusic);
    }
    
    public void SetMusic()
    {
        _sourceMusic.clip = AudioDictionary[AudioNameGamePlay.GameplayFonMusic1];
        _sourceMusic.Play();
    }

    public void PauseSFX()
    {
        _sourceSFX.Stop();
    }
    
    public void UnPauseSFX()
    {
        _sourceSFX.Play();
    }
    
    public void StopSounds()
    {
        _sourceSFX.Stop();
        _sourceMusic.Stop();
    }
    
    public void MuteSources()
    {
        _sourceSFX.mute = true;
        //_sourceMusic.mute = true;
    }

    public void UnMuteSources()
    {
        _sourceSFX.mute = false;
        //_sourceMusic.mute = false;
    }
}
