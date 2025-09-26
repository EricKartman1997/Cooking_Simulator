using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class LoadReleaseGameplayScene : IInitializable, IDisposable
{
    private Dictionary<RawIngredientName, GameObject> _rawIngredientDic;
    private Dictionary<CookedIngredientName, GameObject> _cookedIngredientDic;
    private Dictionary<CookedFoodName, GameObject> _cookedFoodDic;
    
    private Dictionary<FurnitureName, GameObject> _furnitureDic;
    private Dictionary<OtherObjsName, GameObject> _otherObjsDic;
    private Dictionary<PlayerName, GameObject> _playerDic;
    
    private Dictionary<AudioNameGamePlay, AudioClip> _audioDic;
    private Dictionary<MenuName, GameObject> _menuDic;
    
    //private Dictionary<TimerName, GameObjectp> _timersDic;
    
    private List<GameObject> _loadedRawIngredient;
    private List<GameObject> _loadedCookedIngredient;
    private List<GameObject> _loadedCookedFood;
    
    private List<GameObject> _loadedFurniture;
    private List<GameObject> _loadedOtherObjs;
    private List<GameObject> _loadedPlayer;
    
    private List<AudioClip> _loadedClips;
    private List<GameObject> _loadedMenu;
    
    //private List<GameObject> _loadedTimers;
    
    public IReadOnlyDictionary<AudioNameGamePlay, AudioClip> AudioDic => _audioDic;

    public LoadReleaseGameplayScene()
    {
        _audioDic = new Dictionary<AudioNameGamePlay, AudioClip>();
        _loadedClips = new List<AudioClip>();
        // сделать для всех словарей и листов
    }
    
    public async void Initialize()
    {
        await InitMenuAudioAsync();
    }
    
    public void Dispose()
    {
        ReleaseMenuAudio();
    }

    // закинуть в адресейблы объекты
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
        
        _audioDic.Add(AudioNameGamePlay.ClickButton, results[0]);
        _audioDic.Add(AudioNameGamePlay.HoverButton, results[1]);
        _audioDic.Add(AudioNameGamePlay.SwipePanel, results[2]);
        _audioDic.Add(AudioNameGamePlay.Background, results[3]);
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

public enum RawIngredientName
{
    Apple,
    Orange,
    Meat,
    Fish,
    RawCutlet,
    Strawberry,
    Lime,
    Cherry,
    Blueberry
}

public enum CookedIngredientName
{
    BakedApple,
    BakedOrange
}

public enum CookedFoodName
{
    FreshSalad,
    BakedSalad,
    BakedMeat,
    BakedFish,
    BurnCutlet,
    FreshnessCocktail,
    FruitSalad,
    MediumCutlet,
    MixBakedFruit,
    WildBerryCocktail
}

public enum FurnitureName
{
    Blender,
    CuttingTable,
    GetTable,
    GiveTable,
    Suvide,
    Distribution,
    Garbage,
    Oven,
    Stove
}

public enum OtherObjsName
{
    Floor
}

public enum PlayerName
{
    RobotPlayer
}

public enum AudioNameGamePlay
{
    ClickButton,
    HoverButton,
    SwipePanel,
    Background
}

public enum MenuName
{
    DefaultMenu
}


