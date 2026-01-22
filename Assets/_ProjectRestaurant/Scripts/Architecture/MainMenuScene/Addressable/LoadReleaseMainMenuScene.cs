using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadReleaseMainMenuScene : IInitializable, IDisposable //,ILoadRelease<AudioNameMainMenu>
{
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    
    private Dictionary<AudioNameMainMenu, AudioClip> _audioDic;
    private List<AudioClip> _loadedClips;
    private List<GameObject> _loadedPrefabs;
    private Dictionary<ServiceNameMeinMenu, GameObject> _serviceDic = new Dictionary<ServiceNameMeinMenu, GameObject>();
    private Dictionary<CamerasNameMainMenu, GameObject> _camerasDic = new Dictionary<CamerasNameMainMenu, GameObject>();
    private Dictionary<UINameMainMenu, GameObject> _uiDic = new Dictionary<UINameMainMenu, GameObject>();

    private bool _isLoaded;
    
    public IReadOnlyDictionary<AudioNameMainMenu, AudioClip> AudioDic => _audioDic;
    public IReadOnlyDictionary<GlobalPref, GameObject> GlobalPrefDic => _loadReleaseGlobalScene.GlobalPrefDic;

    public Dictionary<ServiceNameMeinMenu, GameObject> ServiceDic => _serviceDic;

    public Dictionary<CamerasNameMainMenu, GameObject> CamerasDic => _camerasDic;
    
    public Dictionary<UINameMainMenu, GameObject> UIDic => _uiDic;

    public bool IsLoaded => _isLoaded;

    public LoadReleaseMainMenuScene(LoadReleaseGlobalScene loadReleaseGlobalScene)
    {
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        
        _audioDic = new Dictionary<AudioNameMainMenu, AudioClip>();
        //_prefDic = new Dictionary<PrefUINameMainMenu, GameObject>();
        _loadedClips = new List<AudioClip>();
        _loadedPrefabs = new List<GameObject>();
        //Debug.Log("Enter LoadReleaseMainMenuScene");
    }
    
    public async void Initialize()
    {
        await Task.WhenAll(
            InitMenuAudioAsync(),
            LoadUIPrefabsAsync(),
            LoadCamerasPrefabsAsync(),
            ServicePrefabsAsync()
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
    
    #region Load Methods

    private async Task InitMenuAudioAsync()
    {
        var loadTasks = new List<Task<AudioClip>>
        {
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/Click-Second.wav"),
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/ClickFlags.wav"),
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/Swipe.mp3"),
            LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Menu/Background.mp3"),
            LoadAudioClipAsync("MenuFonMusic")
        };

        var results = await Task.WhenAll(loadTasks);
        
        _audioDic.Add(AudioNameMainMenu.ClickButton, results[0]);
        _audioDic.Add(AudioNameMainMenu.HoverButton, results[1]);
        _audioDic.Add(AudioNameMainMenu.SwipePanel, results[2]);
        _audioDic.Add(AudioNameMainMenu.Background, results[3]);
        _audioDic.Add(AudioNameMainMenu.MenuFonMusic, results[4]);
    }

    private async Task LoadUIPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("UIPanel"),
            LoadGameObjectAsync("CanvasShowLoading"),

        };

        var results = await Task.WhenAll(loadTasks);
        
        _uiDic.Add(UINameMainMenu.UIPanel, results[0]);
        _uiDic.Add(UINameMainMenu.CanvasShowLoading, results[1]);

    }
    
    private async Task LoadCamerasPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("MainCameraMainMenu"),
        };

        var results = await Task.WhenAll(loadTasks);
        
        _camerasDic.Add(CamerasNameMainMenu.MainCamera, results[0]);

    }
    
    private async Task ServicePrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("SoundMainMenu"),
        };

        var results = await Task.WhenAll(loadTasks);
        
        _serviceDic.Add(ServiceNameMeinMenu.SoundsObject, results[0]);
        
        //Debug.Log("прошел LoadCustomPrefabsAsync");
    }
    
    #endregion

    #region Helper Methods
    private async Task<AudioClip> LoadAudioClipAsync(string address)
    {
        var operation = Addressables.LoadAssetAsync<AudioClip>(address);
        var audioClip = await operation.Task;
        _loadedClips.Add(audioClip);
        return audioClip;
    }

    private async Task<GameObject> LoadGameObjectAsync(string address)
    {
        var operation = Addressables.LoadAssetAsync<GameObject>(address);
        var prefab = await operation.Task;
        if (prefab != null)
        {
            _loadedPrefabs.Add(prefab);
            return prefab;
        }
        Debug.LogError("Ошибка загрузки LoadGameObjectAsync");
        return null;
    }
    
    #endregion
    
    #region Release Methods
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
        //_prefDic.Clear();
    }
    
    #endregion
    
}

public enum AudioNameMainMenu
{
    ClickButton,
    HoverButton,
    SwipePanel,
    Background,
    MenuFonMusic
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

public enum UINameMainMenu
{
    UIPanel,
    CanvasShowLoading
}

public enum ServiceNameMeinMenu
{
    SoundsObject
}

public enum CamerasNameMainMenu
{
    MainCamera
}