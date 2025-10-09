using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class LoadReleaseMainMenuScene : IInitializable, IDisposable
{
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    
    private Dictionary<AudioNameMainMenu, AudioClip> _audioDic;
    private Dictionary<PrefUINameMainMenu, GameObject> _prefDic;
    private List<AudioClip> _loadedClips;
    private List<GameObject> _loadedPrefabs;

    private bool _isLoaded;
    
    public IReadOnlyDictionary<AudioNameMainMenu, AudioClip> AudioDic => _audioDic;
    public IReadOnlyDictionary<PrefUINameMainMenu, GameObject> PrefDic => _prefDic;
    public IReadOnlyDictionary<GlobalPref, GameObject> GlobalPrefDic => _loadReleaseGlobalScene.GlobalPrefDic;
    public bool IsLoaded => _isLoaded;

    public LoadReleaseMainMenuScene(LoadReleaseGlobalScene loadReleaseGlobalScene)
    {
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        
        _audioDic = new Dictionary<AudioNameMainMenu, AudioClip>();
        _prefDic = new Dictionary<PrefUINameMainMenu, GameObject>();
        _loadedClips = new List<AudioClip>();
        _loadedPrefabs = new List<GameObject>();
        //Debug.Log("Enter LoadReleaseMainMenuScene");
    }
    
    public async void Initialize()
    {
        await Task.WhenAll(
            InitMenuAudioAsync(),
            InitMenuPrefabsAsync()
        );
        _isLoaded = true;
        //Debug.Log("Initialize LoadReleaseMainMenuScene");
    }
    
    public void Dispose()
    {
        //ReleaseMenuAudio();
        ReleaseMenuPrefabs();
        _isLoaded = false;
    }

    private async Task InitMenuAudioAsync()
    {
        var loadTasks = new List<Task<AudioClip>>
        {
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/Click-Second.wav"),
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/ClickFlags.wav"),
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/Swipe.mp3"),
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/Background.mp3")
        };

        var results = await Task.WhenAll(loadTasks);
        
        _audioDic.Add(AudioNameMainMenu.ClickButton, results[0]);
        _audioDic.Add(AudioNameMainMenu.HoverButton, results[1]);
        _audioDic.Add(AudioNameMainMenu.SwipePanel, results[2]);
        _audioDic.Add(AudioNameMainMenu.Background, results[3]);
    }

    private async Task InitMenuPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadPrefabAsync("UIPanel")
        };

        var results = await Task.WhenAll(loadTasks);
        
        _prefDic.Add(PrefUINameMainMenu.UIPanel, results[0]);
    }

    private async Task<AudioClip> LoadAudioClipAsync(string address)
    {
        var operation = Addressables.LoadAssetAsync<AudioClip>(address);
        var audioClip = await operation.Task;
        _loadedClips.Add(audioClip);
        return audioClip;
    }

    private async Task<GameObject> LoadPrefabAsync(string address)
    {
        var operation = Addressables.LoadAssetAsync<GameObject>(address);
        var prefab = await operation.Task;
        _loadedPrefabs.Add(prefab);
        return prefab;
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

    private void ReleaseMenuPrefabs()
    {
        foreach (var prefab in _loadedPrefabs)
        {
            Addressables.Release(prefab);
        }
        _loadedPrefabs.Clear();
        _prefDic.Clear();
    }
}

public enum AudioNameMainMenu
{
    ClickButton,
    HoverButton,
    SwipePanel,
    Background
}

public enum PrefUINameMainMenu
{
    //MenuPanel,
    //SettingsPanel,
    //SocialNetworksPanel,
    //Sounds,
    //WarringWindows,
    UIPanel
}