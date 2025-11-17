using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class LoadReleaseGameplay : IDisposable, IInitializable
{
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    
    private Dictionary<PlayerName, GameObject> _playerDic = new Dictionary<PlayerName, GameObject>();
    private Dictionary<OtherObjsName, GameObject> _environmentDic = new Dictionary<OtherObjsName, GameObject>();
    private Dictionary<FurnitureName, GameObject> _furnitureDic = new Dictionary<FurnitureName, GameObject>();
    private Dictionary<IngredientName, GameObject> _ingredientDic = new Dictionary<IngredientName, GameObject>();
    private Dictionary<ViewDishName, GameObject> _viewDishDic = new Dictionary<ViewDishName, GameObject>();
    private Dictionary<UIName, GameObject> _uiDic = new Dictionary<UIName, GameObject>();
    private Dictionary<CustomFurnitureName, GameObject> _customDic = new Dictionary<CustomFurnitureName, GameObject>();
    private Dictionary<CamerasName, GameObject> _camerasDic = new Dictionary<CamerasName, GameObject>();
    private Dictionary<AudioNameGamePlay, AudioClip> _audioDic = new Dictionary<AudioNameGamePlay, AudioClip>();
    private Dictionary<ServiceNameGamePlay, GameObject> _seviceDic = new Dictionary<ServiceNameGamePlay, GameObject>();
    
    private List<GameObject> _loadedPrefabs = new List<GameObject>();
    private List<AudioClip> _loadedClips = new List<AudioClip>();
    
    private bool _isLoaded;
    
    public IReadOnlyDictionary<GlobalPref, GameObject> GlobalPrefDic => _loadReleaseGlobalScene.GlobalPrefDic;
    public IReadOnlyDictionary<PlayerName, GameObject> PlayerDic => _playerDic;
    public IReadOnlyDictionary<OtherObjsName, GameObject> EnvironmentDic => _environmentDic;
    public IReadOnlyDictionary<FurnitureName, GameObject> FurnitureDic => _furnitureDic;
    public IReadOnlyDictionary<IngredientName, GameObject> IngredientDic => _ingredientDic;
    public IReadOnlyDictionary<ViewDishName, GameObject> ViewDishDic => _viewDishDic;
    public IReadOnlyDictionary<UIName, GameObject> UINameDic => _uiDic;
    public IReadOnlyDictionary<CamerasName, GameObject> CamerasDic => _camerasDic;
    public IReadOnlyDictionary<CustomFurnitureName, GameObject> CustomDic => _customDic;
    public IReadOnlyDictionary<AudioNameGamePlay, AudioClip> AudioDic => _audioDic;
    public IReadOnlyDictionary<ServiceNameGamePlay, GameObject> ServiceDic => _seviceDic;
    
    public bool IsLoaded => _isLoaded;

    public LoadReleaseGameplay(LoadReleaseGlobalScene loadReleaseGlobalScene)
    {
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
    }

    public void Dispose()
    {
        ReleasePlayerPrefabs();
        _isLoaded = false;
        //Debug.Log("Dispose LoadReleaseGameplay");
    }
    
    public async void Initialize()
    {
        await Task.WhenAll(
            LoadPlayerPrefabsAsync(),
            LoadEnvironmentPrefabsAsync(),
            LoadFurniturePrefabsAsync(), 
            LoadFoodPrefabsAsync(),
            LoadViewDishPrefabsAsync(),
            LoadUIPrefabsAsync(),
            LoadCamerasPrefabsAsync(),
            LoadCustomPrefabsAsync(),
            SoundsPrefabsAsync(),
            ServicePrefabsAsync()
            
        );
        //Debug.Log("Загружены все ресурсы для Gameplay");
        _isLoaded = true;
    }
    
    #region Load Methods
    private async Task LoadPlayerPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("PlayerDefault")
        };

        var results = await Task.WhenAll(loadTasks);
        
        _playerDic.Add(PlayerName.RobotPlayer, results[0]);
        //Debug.Log("прошел LoadPlayerPrefabsAsync");
    }
    
    private async Task LoadEnvironmentPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("Floor"),
            LoadGameObjectAsync("LightMain")
            
        };

        var results = await Task.WhenAll(loadTasks);
        
        _environmentDic.Add(OtherObjsName.Floor, results[0]);
        _environmentDic.Add(OtherObjsName.LightMain, results[1]);
        //Debug.Log("прошел LoadEnvironmentPrefabsAsync");
    }
    
    private async Task LoadFurniturePrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("GetTable"),
            LoadGameObjectAsync("GiveTable"),
            LoadGameObjectAsync("CuttingTable"),
            LoadGameObjectAsync("Garbage"),
            LoadGameObjectAsync("Oven"),
            LoadGameObjectAsync("Suvide"),
            LoadGameObjectAsync("Distribution"),
            LoadGameObjectAsync("Blender"),
            LoadGameObjectAsync("Stove"),
        };

        var results = await Task.WhenAll(loadTasks);
        
        _furnitureDic.Add(FurnitureName.GetTable, results[0]);
        _furnitureDic.Add(FurnitureName.GiveTable, results[1]);
        _furnitureDic.Add(FurnitureName.CuttingTable, results[2]);
        _furnitureDic.Add(FurnitureName.Garbage, results[3]);
        _furnitureDic.Add(FurnitureName.Oven, results[4]);
        _furnitureDic.Add(FurnitureName.Suvide, results[5]);
        _furnitureDic.Add(FurnitureName.Distribution, results[6]);
        _furnitureDic.Add(FurnitureName.Blender, results[7]);
        _furnitureDic.Add(FurnitureName.Stove, results[8]);
        
        //Debug.Log("прошел LoadFurniturePrefabsAsync");
    }
    
    private async Task LoadFoodPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("Apple"),
            LoadGameObjectAsync("BakedApple"),
            LoadGameObjectAsync("Orange"),
            LoadGameObjectAsync("BakedOrange"),
            LoadGameObjectAsync("Fish"),
            LoadGameObjectAsync("BakedFish"),
            LoadGameObjectAsync("Meat"),
            LoadGameObjectAsync("BakedMeat"),
            LoadGameObjectAsync("BurnCutlet"),
            LoadGameObjectAsync("Blueberry"),
            LoadGameObjectAsync("Strawberry"),
            LoadGameObjectAsync("WildBerryCocktail"),
            LoadGameObjectAsync("MixBakedFruit"),
            LoadGameObjectAsync("MediumCutlet"),
            LoadGameObjectAsync("RawCutlet"),
            LoadGameObjectAsync("FreshnessCocktail"),
            LoadGameObjectAsync("FruitSalad"),
            LoadGameObjectAsync("Cherry"),
            LoadGameObjectAsync("Lime"),
            LoadGameObjectAsync("Rubbish"),
        };

        var results = await Task.WhenAll(loadTasks);
        
        _ingredientDic.Add(IngredientName.Apple, results[0]);
        _ingredientDic.Add(IngredientName.BakedApple, results[1]);
        _ingredientDic.Add(IngredientName.Orange, results[2]);
        _ingredientDic.Add(IngredientName.BakedOrange, results[3]);
        _ingredientDic.Add(IngredientName.Fish, results[4]);
        _ingredientDic.Add(IngredientName.BakedFish, results[5]);
        _ingredientDic.Add(IngredientName.Meat, results[6]);
        _ingredientDic.Add(IngredientName.BakedMeat, results[7]);
        _ingredientDic.Add(IngredientName.BurnCutlet, results[8]);
        _ingredientDic.Add(IngredientName.Blueberry, results[9]);
        _ingredientDic.Add(IngredientName.Strawberry, results[10]);
        _ingredientDic.Add(IngredientName.WildBerryCocktail, results[11]);
        _ingredientDic.Add(IngredientName.MixBakedFruit, results[12]);
        _ingredientDic.Add(IngredientName.MediumCutlet, results[13]);
        _ingredientDic.Add(IngredientName.RawCutlet, results[14]);
        _ingredientDic.Add(IngredientName.FreshnessCocktail, results[15]);
        _ingredientDic.Add(IngredientName.FruitSalad, results[16]);
        _ingredientDic.Add(IngredientName.Cherry, results[17]);
        _ingredientDic.Add(IngredientName.Lime, results[18]);
        _ingredientDic.Add(IngredientName.Rubbish, results[19]);
        
        //Debug.Log("прошел LoadFoodPrefabsAsync");
        
    }
    
    private async Task LoadViewDishPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("AppleViewDish"),
            LoadGameObjectAsync("OrangeViewDish"),
            LoadGameObjectAsync("MeatViewDish"),
            LoadGameObjectAsync("FishViewDish"),
            LoadGameObjectAsync("RawCutletViewDish"),
            LoadGameObjectAsync("StrawberryViewDish"),
            LoadGameObjectAsync("LimeViewDish"),
            LoadGameObjectAsync("CherryViewDish"),
            LoadGameObjectAsync("BlueberryViewDish"),
        };

        var results = await Task.WhenAll(loadTasks);
        
        _viewDishDic.Add(ViewDishName.AppleViewDish, results[0]);
        _viewDishDic.Add(ViewDishName.OrangeViewDish, results[1]);
        _viewDishDic.Add(ViewDishName.MeatViewDish, results[2]);
        _viewDishDic.Add(ViewDishName.FishViewDish, results[3]);
        _viewDishDic.Add(ViewDishName.RawCutletViewDish, results[4]);
        _viewDishDic.Add(ViewDishName.StrawberryViewDish, results[5]);
        _viewDishDic.Add(ViewDishName.LimeViewDish, results[6]);
        _viewDishDic.Add(ViewDishName.CherryViewDish, results[7]);
        _viewDishDic.Add(ViewDishName.BlueberryViewDish, results[8]);
        
        //Debug.Log("прошел LoadViewDishPrefabsAsync");
    }
    
    private async Task LoadUIPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("GameWindow"),
            LoadGameObjectAsync("GameOverWindow"),
            LoadGameObjectAsync("MainFrameCanvas")
        };

        var results = await Task.WhenAll(loadTasks);
        
        _uiDic.Add(UIName.GameWindow, results[0]);
        _uiDic.Add(UIName.GameOverWindow, results[1]);
        _uiDic.Add(UIName.MainFrameCanvas, results[2]);
        
        //Debug.Log("прошел LoadUIPrefabsAsync");
    }
    
    private async Task LoadCamerasPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("MainCamera"),
            LoadGameObjectAsync("TopDownCamera"),
        };

        var results = await Task.WhenAll(loadTasks);
        
        _camerasDic.Add(CamerasName.MainCamera, results[0]);
        _camerasDic.Add(CamerasName.TopDownCamera, results[1]);
        
        //Debug.Log("прошел LoadUIPrefabsAsync");
    }
    
    private async Task LoadCustomPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("CrossCustomizationTable"),
            LoadGameObjectAsync("Default"),
            LoadGameObjectAsync("ToysCustomizationTable"),
            LoadGameObjectAsync("CrockeryCustomizationTable"),
        };

        var results = await Task.WhenAll(loadTasks);
        
        _customDic.Add(CustomFurnitureName.TurnOff, results[0]);
        _customDic.Add(CustomFurnitureName.Default, results[1]);
        _customDic.Add(CustomFurnitureName.NewYear, results[2]);
        _customDic.Add(CustomFurnitureName.Crock, results[3]);
        
        //Debug.Log("прошел LoadCustomPrefabsAsync");
    }
    
    private async Task SoundsPrefabsAsync()
    {

        var loadTasks = new List<Task<AudioClip>>
        {
            //TODO: изменить названия на более краткие
            //TODO: Проверить были ли она удалены в MainScene
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
        
        //Debug.Log("прошел LoadCustomPrefabsAsync");
    }
    
    private async Task ServicePrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("SoundsObject"),
        };

        var results = await Task.WhenAll(loadTasks);
        
        _seviceDic.Add(ServiceNameGamePlay.SoundsObject, results[0]);
        
        //Debug.Log("прошел LoadCustomPrefabsAsync");
    }
    
    #endregion
    
    #region Helper Methods
    private async Task<GameObject> LoadGameObjectAsync(string address)
    {
        var operation = Addressables.LoadAssetAsync<GameObject>(address);
        var prefab = await operation.Task;
        if (prefab != null)
        {
            _loadedPrefabs.Add(prefab);
            return prefab;
        }
        Debug.Log("Ошибка загрузки LoadGameObjectAsync");
        return null;
    }
    
    private async Task<AudioClip> LoadAudioClipAsync(string address)
    {
        var operation = Addressables.LoadAssetAsync<AudioClip>(address);
        var audioClip = await operation.Task;
        _loadedClips.Add(audioClip);
        return audioClip;
    }
    
    #endregion
    
    #region Release Methods
    private void ReleasePlayerPrefabs()
    {
        foreach (var prefab in _loadedPrefabs)
        {
            Addressables.Release(prefab);
        }
        _loadedPrefabs.Clear();
        _playerDic.Clear();
    }
    
    #endregion
}

public enum IngredientName
{
    Apple,
    Orange,
    Meat,
    Fish,
    RawCutlet,
    Strawberry,
    Lime,
    Cherry,
    Blueberry,
    BakedApple,
    BakedOrange,
    BakedMeat,
    BakedFish,
    BurnCutlet,
    FreshnessCocktail,
    FruitSalad,
    MediumCutlet,
    MixBakedFruit,
    WildBerryCocktail,
    Rubbish
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
    Floor,
    LightMain
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
    Background,
}

public enum ServiceNameGamePlay
{
    SoundsObject
}

public enum UIName
{
    GameWindow,
    GameOverWindow,
    MainFrameCanvas
}

public enum CamerasName
{
    MainCamera,
    TopDownCamera,
}

public enum CustomFurnitureName
{
    TurnOff,
    NewYear,
    Crock,
    Default
}

public enum ViewDishName
{
    AppleViewDish,
    OrangeViewDish,
    MeatViewDish,
    FishViewDish,
    RawCutletViewDish,
    StrawberryViewDish,
    LimeViewDish,
    CherryViewDish,
    BlueberryViewDish
}
