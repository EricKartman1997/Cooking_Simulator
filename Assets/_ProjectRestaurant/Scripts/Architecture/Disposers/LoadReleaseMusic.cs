using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class LoadReleaseMusic : IInitializable, IDisposable
{
    private Dictionary<AudioName, AudioClip> _audioDic;
    private List<AudioClip> _loadedClips;
    
    public IReadOnlyDictionary<AudioName, AudioClip> AudioDic => _audioDic;

    public LoadReleaseMusic()
    {
        _audioDic = new Dictionary<AudioName, AudioClip>();
        _loadedClips = new List<AudioClip>();
    }
    
    public async void Initialize()
    {
        await InitMenuAudioAsync();
    }
    
    public void Dispose()
    {
        ReleaseMenuAudio();
    }

    private async Task InitMenuAudioAsync()
    {
        // Асинхронная загрузка всех ассетов параллельно
        var loadTasks = new List<Task<AudioClip>>
        {
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/Click-Second.wav"),
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/ClickFlags.wav"),
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/Swipe.mp3"),
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/Background.mp3")
        };

        var results = await Task.WhenAll(loadTasks);
        
        _audioDic.Add(AudioName.ClickButton, results[0]);
        _audioDic.Add(AudioName.HoverButton, results[1]);
        _audioDic.Add(AudioName.SwipePanel, results[2]);
        _audioDic.Add(AudioName.Background, results[3]);
    }

    private async Task<AudioClip> LoadAudioClipAsync(string address)
    {
        var operation = Addressables.LoadAssetAsync<AudioClip>(address);
        var audioClip = await operation.Task;
        _loadedClips.Add(audioClip);
        return audioClip;
    }
    
    private void ReleaseMenuAudio()
    {
        foreach (var audioClip in _loadedClips)
        {
            Addressables.Release(audioClip);
        }
        _loadedClips.Clear();
        _audioDic.Clear();
    }
}

public enum AudioName
{
    ClickButton,
    HoverButton,
    SwipePanel,
    Background
}
