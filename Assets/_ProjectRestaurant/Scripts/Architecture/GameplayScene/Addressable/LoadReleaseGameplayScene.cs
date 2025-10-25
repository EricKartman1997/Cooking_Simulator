using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class LoadReleaseGameplayScene : IInitializable, IDisposable
{
    // Словари для хранения загруженных объектов
    // private Dictionary<RawIngredientName, GameObject> _rawIngredientDic;
    // private Dictionary<CookedIngredientName, GameObject> _cookedIngredientDic;
    // private Dictionary<CookedFoodName, GameObject> _cookedFoodDic;
    //private Dictionary<FurnitureName, GameObject> _furnitureDic;
    private Dictionary<OtherObjsName, GameObject> _otherObjsDic;
    private Dictionary<PlayerName, GameObject> _playerDic;
    //private Dictionary<AudioNameGamePlay, AudioClip> _audioDic;
    //private Dictionary<MenuName, GameObject> _menuDic;
    private Dictionary<CustomFurnitureName, GameObject> _customFurnitureDic;
    private Dictionary<ViewDishName, GameObject> _viewDishDic;
    
    // Списки для отслеживания загруженных ресурсов
    private List<GameObject> _loadedRawIngredient;
    private List<GameObject> _loadedCookedIngredient;
    private List<GameObject> _loadedCookedFood;
    //private List<GameObject> _loadedFurniture;
    private List<GameObject> _loadedOtherObjs;
    private List<GameObject> _loadedPlayer;
    //private List<AudioClip> _loadedClips;
    //private List<GameObject> _loadedMenu;
    private List<GameObject> _loadedCustomFurniture;
    private List<GameObject> _loadedViewDish;

    // Публичные свойства для доступа к словарям
    // public IReadOnlyDictionary<RawIngredientName, GameObject> RawIngredientDic => _rawIngredientDic;
    // public IReadOnlyDictionary<CookedIngredientName, GameObject> CookedIngredientDic => _cookedIngredientDic;
    // public IReadOnlyDictionary<CookedFoodName, GameObject> CookedFoodDic => _cookedFoodDic;
    //public IReadOnlyDictionary<FurnitureName, GameObject> FurnitureDic => _furnitureDic;
    public IReadOnlyDictionary<OtherObjsName, GameObject> OtherObjsDic => _otherObjsDic;
    public IReadOnlyDictionary<PlayerName, GameObject> PlayerDic => _playerDic;
    //public IReadOnlyDictionary<AudioNameGamePlay, AudioClip> AudioDic => _audioDic;
    //public IReadOnlyDictionary<MenuName, GameObject> MenuDic => _menuDic;
    public IReadOnlyDictionary<CustomFurnitureName, GameObject> CustomFurnitureDic => _customFurnitureDic;
    public IReadOnlyDictionary<ViewDishName, GameObject> ViewDishDic => _viewDishDic;

    public LoadReleaseGameplayScene()
    {
        // Инициализация словарей
        // _rawIngredientDic = new Dictionary<RawIngredientName, GameObject>();
        // _cookedIngredientDic = new Dictionary<CookedIngredientName, GameObject>();
        // _cookedFoodDic = new Dictionary<CookedFoodName, GameObject>();
        //_furnitureDic = new Dictionary<FurnitureName, GameObject>();
        _otherObjsDic = new Dictionary<OtherObjsName, GameObject>();
        _playerDic = new Dictionary<PlayerName, GameObject>();
        //_audioDic = new Dictionary<AudioNameGamePlay, AudioClip>();
        //_menuDic = new Dictionary<MenuName, GameObject>();
        _customFurnitureDic = new Dictionary<CustomFurnitureName, GameObject>();
        _viewDishDic = new Dictionary<ViewDishName, GameObject>();

        // Инициализация списков
        _loadedRawIngredient = new List<GameObject>();
        _loadedCookedIngredient = new List<GameObject>();
        _loadedCookedFood = new List<GameObject>();
        //_loadedFurniture = new List<GameObject>();
        _loadedOtherObjs = new List<GameObject>();
        _loadedPlayer = new List<GameObject>();
        //_loadedClips = new List<AudioClip>();
        //_loadedMenu = new List<GameObject>();
        _loadedCustomFurniture = new List<GameObject>();
        _loadedViewDish = new List<GameObject>();
    }

    public async void Initialize()
    {
        await LoadAllAssetsAsync();
    }

    public void Dispose()
    {
        ReleaseAllAssets();
    }

    private async Task LoadAllAssetsAsync()
    {
        try
        {
            // Загрузка всех ассетов параллельно
            await Task.WhenAll(
                LoadRawIngredientsAsync(),
                LoadCookedIngredientsAsync(),
                LoadCookedFoodAsync(),
                //LoadFurnitureAsync(),
                LoadOtherObjectsAsync(),
                LoadPlayersAsync(),
                //LoadAudioAsync(),
                //LoadMenusAsync(),
                LoadCustomFurnitureAsync(),    // новая категория
                LoadViewDishAsync() 
            );
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading assets: {e.Message}");
        }
    }

    #region Load Methods
    private async Task LoadRawIngredientsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/RawFood/_Resources/Apple.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/RawFood/_Resources/Orange.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/RawFood/_Resources/Meat.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/RawFood/_Resources/Fish.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/RawFood/_Resources/RawCutlet.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/RawFood/_Resources/Strawberry.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/RawFood/_Resources/Lime.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/RawFood/_Resources/Cherry.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/RawFood/_Resources/Blueberry.prefab")
        };

        var results = await Task.WhenAll(loadTasks);
        
        // _rawIngredientDic.Add(RawIngredientName.Apple, results[0]);
        // _rawIngredientDic.Add(RawIngredientName.Orange, results[1]);
        // _rawIngredientDic.Add(RawIngredientName.Meat, results[2]);
        // _rawIngredientDic.Add(RawIngredientName.Fish, results[3]);
        // _rawIngredientDic.Add(RawIngredientName.RawCutlet, results[4]);
        // _rawIngredientDic.Add(RawIngredientName.Strawberry, results[5]);
        // _rawIngredientDic.Add(RawIngredientName.Lime, results[6]);
        // _rawIngredientDic.Add(RawIngredientName.Cherry, results[7]);
        // _rawIngredientDic.Add(RawIngredientName.Blueberry, results[8]);
    }

    private async Task LoadCookedIngredientsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedIngredient/Prefabs/BakedApple.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedIngredient/Prefabs/BakedOrange.prefab")
        };

        var results = await Task.WhenAll(loadTasks);
        
        // _cookedIngredientDic.Add(CookedIngredientName.BakedApple, results[0]);
        // _cookedIngredientDic.Add(CookedIngredientName.BakedOrange, results[1]);
    }

    private async Task LoadCookedFoodAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedFood/Prefabs/FreshSalad.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedFood/Prefabs/BakedSalad.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedFood/Prefabs/BakedMeat.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedFood/Prefabs/BakedFish.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedFood/Prefabs/BurnCutlet.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedFood/Prefabs/FreshnessCocktail.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedFood/Prefabs/FruitSalad.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedFood/Prefabs/MediumCutlet.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedFood/Prefabs/MixBakedFruit.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Food/CookedFood/Prefabs/WildBerryCocktail.prefab")
        };

        var results = await Task.WhenAll(loadTasks);
        
        // _cookedFoodDic.Add(CookedFoodName.FreshSalad, results[0]);
        // _cookedFoodDic.Add(CookedFoodName.BakedSalad, results[1]);
        // _cookedFoodDic.Add(CookedFoodName.BakedMeat, results[2]);
        // _cookedFoodDic.Add(CookedFoodName.BakedFish, results[3]);
        // _cookedFoodDic.Add(CookedFoodName.BurnCutlet, results[4]);
        // _cookedFoodDic.Add(CookedFoodName.FreshnessCocktail, results[5]);
        // _cookedFoodDic.Add(CookedFoodName.FruitSalad, results[6]);
        // _cookedFoodDic.Add(CookedFoodName.MediumCutlet, results[7]);
        // _cookedFoodDic.Add(CookedFoodName.MixBakedFruit, results[8]);
        // _cookedFoodDic.Add(CookedFoodName.WildBerryCocktail, results[9]);
    }

    // private async Task LoadFurnitureAsync()
    // {
    //     var loadTasks = new List<Task<GameObject>>
    //     {
    //         LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Blender.prefab"),
    //         LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/CuttingTable.prefab"),
    //         LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/GetTable.prefab"),
    //         LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/GiveTable.prefab"),
    //         LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Suvide.prefab"),
    //         LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Distribution.prefab"),
    //         LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Garbage.prefab"),
    //         LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Oven.prefab"),
    //         LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Stove.prefab")
    //     };
    //
    //     var results = await Task.WhenAll(loadTasks);
    //     
    //     _furnitureDic.Add(FurnitureName.Blender, results[0]);
    //     _furnitureDic.Add(FurnitureName.CuttingTable, results[1]);
    //     _furnitureDic.Add(FurnitureName.GetTable, results[2]);
    //     _furnitureDic.Add(FurnitureName.GiveTable, results[3]);
    //     _furnitureDic.Add(FurnitureName.Suvide, results[4]);
    //     _furnitureDic.Add(FurnitureName.Distribution, results[5]);
    //     _furnitureDic.Add(FurnitureName.Garbage, results[6]);
    //     _furnitureDic.Add(FurnitureName.Oven, results[7]);
    //     _furnitureDic.Add(FurnitureName.Stove, results[8]);
    // }

    private async Task LoadOtherObjectsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Environment/Prefabs/Floor.prefab")
        };

        var results = await Task.WhenAll(loadTasks);
        
        _otherObjsDic.Add(OtherObjsName.Floor, results[0]);
    }

    private async Task LoadPlayersAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Player/Prefabs/Player.prefab")
        };

        var results = await Task.WhenAll(loadTasks);
        
        _playerDic.Add(PlayerName.RobotPlayer, results[0]);
    }

    // private async Task LoadAudioAsync()
    // {
    //     var loadTasks = new List<Task<AudioClip>>
    //     {
    //         LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Gameplay/ClickButton.wav"),
    //         LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Gameplay/HoverButton.wav"),
    //         LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Gameplay/SwipePanel.mp3"),
    //         LoadAudioClipAsync("Assets/_ProjectRestaurant/Sounds/Gameplay/Background.mp3")
    //     };
    //
    //     var results = await Task.WhenAll(loadTasks);
    //     
    //     _audioDic.Add(AudioNameGamePlay.ClickButton, results[0]);
    //     _audioDic.Add(AudioNameGamePlay.HoverButton, results[1]);
    //     _audioDic.Add(AudioNameGamePlay.SwipePanel, results[2]);
    //     _audioDic.Add(AudioNameGamePlay.Background, results[3]);
    // }

    // private async Task LoadMenusAsync()
    // {
    //     var loadTasks = new List<Task<GameObject>>
    //     {
    //         LoadGameObjectAsync("Assets/_ProjectRestaurant/UI/Menus/DefaultMenu.prefab")
    //     };
    //
    //     var results = await Task.WhenAll(loadTasks);
    //     
    //     _menuDic.Add(MenuName.DefaultMenu, results[0]);
    // }
    
    private async Task LoadCustomFurnitureAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/View/Cross.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/View/Toys.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/View/Crockery.prefab")
        };

        var results = await Task.WhenAll(loadTasks);
        
        _customFurnitureDic.Add(CustomFurnitureName.TurnOff, results[0]);
        _customFurnitureDic.Add(CustomFurnitureName.NewYear, results[1]);
        _customFurnitureDic.Add(CustomFurnitureName.Crock, results[2]);
    }

    // Методы загрузки для ViewDish
    private async Task LoadViewDishAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/Dish/AppleView.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/Dish/OrangeView.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/Dish/MeatView.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/Dish/FishView.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/Dish/RawCutletView.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/Dish/StrawberryView.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/Dish/LimeView.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/Dish/CherryView.prefab"),
            LoadGameObjectAsync("Assets/_ProjectRestaurant/Prefabs/Furniture/Resources_moved/GetTable/Resources_moved/Dish/BlueberryView.prefab")
        };

        var results = await Task.WhenAll(loadTasks);
        
        // _viewDishDic.Add(ViewDishName.AppleDish, results[0]);
        // _viewDishDic.Add(ViewDishName.OrangeDish, results[1]);
        // _viewDishDic.Add(ViewDishName.MeatDish, results[2]);
        // _viewDishDic.Add(ViewDishName.FishDish, results[3]);
        // _viewDishDic.Add(ViewDishName.RawCutletDish, results[4]);
        // _viewDishDic.Add(ViewDishName.StrawberryDish, results[5]);
        // _viewDishDic.Add(ViewDishName.LimeDish, results[6]);
        // _viewDishDic.Add(ViewDishName.CherryDish, results[7]);
        // _viewDishDic.Add(ViewDishName.BlueberryDish, results[8]);
    }
    #endregion

    #region Helper Methods
    private async Task<GameObject> LoadGameObjectAsync(string address)
    {
        var operation = Addressables.LoadAssetAsync<GameObject>(address);
        var gameObject = await operation.Task;
        
        // Добавляем в соответствующий список в зависимости от типа
        if (address.Contains("RawFood")) _loadedRawIngredient.Add(gameObject);
        else if (address.Contains("CookedIngredient")) _loadedCookedIngredient.Add(gameObject);
        else if (address.Contains("CookedFood")) _loadedCookedFood.Add(gameObject);
        //else if (address.Contains("Furniture")) _loadedFurniture.Add(gameObject);
        else if (address.Contains("Environment")) _loadedOtherObjs.Add(gameObject);
        else if (address.Contains("Players")) _loadedPlayer.Add(gameObject);
        //else if (address.Contains("Menus")) _loadedMenu.Add(gameObject);
        else if (address.Contains("View")) _loadedCustomFurniture.Add(gameObject);
        else if (address.Contains("Dish")) _loadedViewDish.Add(gameObject);
        
        return gameObject;
    }

    private async Task<AudioClip> LoadAudioClipAsync(string address)
    {
        var operation = Addressables.LoadAssetAsync<AudioClip>(address);
        var audioClip = await operation.Task;
        //_loadedClips.Add(audioClip);
        return audioClip;
    }
    #endregion

    #region Release Methods
    private void ReleaseAllAssets()
    {
        ReleaseGameObjects(_loadedRawIngredient);
        ReleaseGameObjects(_loadedCookedIngredient);
        ReleaseGameObjects(_loadedCookedFood);
        //ReleaseGameObjects(_loadedFurniture);
        ReleaseGameObjects(_loadedOtherObjs);
        ReleaseGameObjects(_loadedPlayer);
        //ReleaseGameObjects(_loadedMenu);
        ReleaseGameObjects(_loadedCustomFurniture);
        ReleaseGameObjects(_loadedViewDish);
        //ReleaseAudioClips(_loadedClips);

        // Очистка словарей
        // _rawIngredientDic.Clear();
        // _cookedIngredientDic.Clear();
        // _cookedFoodDic.Clear();
        //_furnitureDic.Clear();
        _otherObjsDic.Clear();
        _playerDic.Clear();
        //_audioDic.Clear();
        //_menuDic.Clear();
        _customFurnitureDic.Clear();
        _viewDishDic.Clear();
    }

    private void ReleaseGameObjects(List<GameObject> gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject != null)
            {
                Addressables.Release(gameObject);
            }
        }
        gameObjects.Clear();
    }

    private void ReleaseAudioClips(List<AudioClip> audioClips)
    {
        foreach (var audioClip in audioClips)
        {
            if (audioClip != null)
            {
                Addressables.Release(audioClip);
            }
        }
        audioClips.Clear();
    }
    #endregion
}

