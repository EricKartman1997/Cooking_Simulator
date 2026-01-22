using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SoundsServiceMainMenu : ISoundsService
{
    private SoundManager _soundManager;
    private DiContainer _container;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;
    private AudioSource _sourceSFX;
    private AudioSource _sourceMusic;
    private GameObject empty = new GameObject("Sounds_Test");

    public AudioSource SourceSfx => _sourceSFX;

    public AudioSource SourceMusic => _sourceMusic;

    public SoundManager SoundManager => _soundManager;

    public IReadOnlyDictionary<AudioNameMainMenu, AudioClip> AudioDictionary => _loadReleaseMainMenuScene.AudioDic;

    public SoundsServiceMainMenu(SoundManager soundManager, LoadReleaseMainMenuScene loadReleaseMainMenuScene,DiContainer container)
    {
        _soundManager = soundManager;
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
        _container = container;

    }
    
    public void CreateSounds()
    {
        GameObject sound = _container.InstantiatePrefab(_loadReleaseMainMenuScene.ServiceDic[ServiceNameMeinMenu.SoundsObject], empty.transform);
        
        GameObject obj = sound.transform.Find("SFX")?.gameObject;
        GameObject obj1 = sound.transform.Find("Music")?.gameObject;

        _sourceSFX = obj.GetComponent<AudioSource>();
        _sourceMusic = obj1.GetComponent<AudioSource>();
        
        _container.Bind<AudioSource>().WithId("SFX").FromInstance(_sourceSFX);
        _container.Bind<AudioSource>().WithId("Music").FromInstance(_sourceMusic);
    }
    
    public void SetMusic()
    {
        _sourceMusic.clip = _loadReleaseMainMenuScene.AudioDic[AudioNameMainMenu.MenuFonMusic];
        //_sourceMusic.Play();
    }
    
    public void PlaySounds()
    {
        if(_sourceSFX == null && _sourceMusic == null)
            return;
        
        _sourceSFX.Play();
        _sourceMusic.Play();
    }

    public void StopSounds()
    {
        if(_sourceSFX == null && _sourceMusic == null)
            return;
        
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
