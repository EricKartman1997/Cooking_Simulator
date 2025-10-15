using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundsServiceMainMenu : IDisposable
{
    private SoundManager _soundManager;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;
    private AudioSource _sourceSFX;
    private AudioSource _sourceMusic;
    
    public SoundManager SoundManager => _soundManager;

    public IReadOnlyDictionary<AudioNameMainMenu, AudioClip> AudioDictionary => _loadReleaseMainMenuScene.AudioDic;

    public SoundsServiceMainMenu(SoundManager soundManager, LoadReleaseMainMenuScene loadReleaseMainMenuScene,AudioSource sourceSfx, AudioSource sourceMusic)
    {
        _soundManager = soundManager;
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
        _sourceSFX = sourceSfx;
        _sourceMusic = sourceMusic;
    }
    
    public void Dispose()
    {
        Debug.Log(" Dispose SoundsServiceMainMenu");
    }
}
