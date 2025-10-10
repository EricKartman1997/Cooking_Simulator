using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SoundsServiceMainMenu : IDisposable
{
    private SoundManager _soundManager;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;
    //private AudioSource _sFX;
    //private AudioSource _background;

    //public AudioSource SFX => _sFX;

    //public AudioSource Background => _background;
    
    public SoundManager SoundManager => _soundManager;

    public IReadOnlyDictionary<AudioNameMainMenu, AudioClip> AudioDictionary => _loadReleaseMainMenuScene.AudioDic;

    public SoundsServiceMainMenu(SoundManager soundManager, LoadReleaseMainMenuScene loadReleaseMainMenuScene)
    {
        _soundManager = soundManager;
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
        //_sFX = sfxAudio;
        //_background = backgroundAudio;
        //,[Inject(Id = "SFX")] AudioSource sfxAudio, [Inject(Id = "Background")] AudioSource backgroundAudio
    }
    
    public void Dispose()
    {
        
    }
}
