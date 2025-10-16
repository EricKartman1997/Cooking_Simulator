using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SoundsServiceMainMenu : IDisposable
{
    private SoundManager _soundManager;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;
    private AudioSource _sourceSFX;
    private AudioSource _sourceMusic;

    public AudioSource SourceSfx => _sourceSFX;

    public AudioSource SourceMusic => _sourceMusic;

    public SoundManager SoundManager => _soundManager;

    public IReadOnlyDictionary<AudioNameMainMenu, AudioClip> AudioDictionary => _loadReleaseMainMenuScene.AudioDic;

    public SoundsServiceMainMenu(SoundManager soundManager, LoadReleaseMainMenuScene loadReleaseMainMenuScene, [Inject(Id = "SFX")] AudioSource sfxAudio, [Inject(Id = "Music")] AudioSource backgroundAudio)
    {
        _soundManager = soundManager;
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
        _sourceSFX = sfxAudio;
        _sourceMusic = backgroundAudio;
    }
    
    public void SetMusic()
    {
        _sourceMusic.clip = _loadReleaseMainMenuScene.AudioDic[AudioNameMainMenu.Background];
        _sourceMusic.Play();
    }

    public void StopSounds()
    {
        _sourceSFX.Stop();
        _sourceMusic.Stop();
    }
    
    public void Dispose()
    {
        Debug.Log(" Dispose SoundsServiceMainMenu");
    }
}
