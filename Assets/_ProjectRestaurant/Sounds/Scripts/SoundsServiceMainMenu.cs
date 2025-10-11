using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundsServiceMainMenu : IDisposable
{
    private SoundManager _soundManager;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;
    
    public SoundManager SoundManager => _soundManager;

    public IReadOnlyDictionary<AudioNameMainMenu, AudioClip> AudioDictionary => _loadReleaseMainMenuScene.AudioDic;

    public SoundsServiceMainMenu(SoundManager soundManager, LoadReleaseMainMenuScene loadReleaseMainMenuScene)
    {
        _soundManager = soundManager;
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
    }
    
    public void Dispose()
    {
        
    }
}
