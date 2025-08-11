using System;
using UnityEngine;

public class GameManager : IDisposable
{
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    
    //Container
    private RecipeContainer _recipeContainer;
    private ProductsContainer _productsContainer;
    private FoodsForFurnitureContainer _foodsForFurnitureContainer;
    private UIContainer _uiContainer;
    
    // Managers
    private BootstrapLVL2 _bootstrapLvl2;
    private GameManagerUpdate _gameManagerUpdate;
    private DataManager _dataManager;
    private UIManager _uiManager;
    
    // Scripts
    private Checks _checks;
    private Score _score;
    private UpdateChecks _updateChecks;
    private Orders _orders;
    private EventBus _eventBus;
    private TimeGame _timeGame;
    private RecipeService _recipeService;
    private GameOver _gameOver;
    
    // Factories
    private ViewFactory _viewFactory;
    private ProductsFactory _productsFactory;
    private HelperScriptFactory _helperScriptFactory;
    private ChecksFactory _checksFactory;

    // Other 
    
    public BootstrapLVL2 BootstrapLvl2 => _bootstrapLvl2;
    public ProductsContainer ProductsContainer => _productsContainer;
    
    public RecipeContainer RecipeContainer => _recipeContainer;
    
    public UIContainer UIContainer => _uiContainer;
    
    public FoodsForFurnitureContainer FoodsForFurnitureContainer => _foodsForFurnitureContainer;
    
    public DataManager DataManager => _dataManager;
    
    public UIManager UIManager => _uiManager;
    public Checks Checks => _checks;
    public Score Score => _score;
    public UpdateChecks UpdateChecks => _updateChecks;
    public Orders Orders => _orders;
    //public OrdersUI OrdersUI => _ordersUI;
    public EventBus EventBus => _eventBus;
    public TimeGame TimeGame => _timeGame;
    public RecipeService RecipeService => _recipeService;
    public GameOver GameOver => _gameOver;
    
    public ViewFactory ViewFactory => _viewFactory;
    public ProductsFactory ProductsFactory => _productsFactory;
    public HelperScriptFactory HelperScriptFactory => _helperScriptFactory;
    public ChecksFactory ChecksFactory => _checksFactory;

    public GameManager(BootstrapLVL2 bootstrapLvl2, ProductsContainer productsContainer, UIContainer uiContainer,
        GameManagerUpdate gameManagerUpdate, DataManager dataManager,UIManager uiManager,
        Checks checks, Score score, UpdateChecks updateChecks, Orders orders, EventBus eventBus,
        TimeGame timeGame,RecipeService recipeService, GameOver gameOver, ViewFactory viewFactory, ProductsFactory productsFactory,
        HelperScriptFactory helperScriptFactory,ChecksFactory checksFactory,
        FoodsForFurnitureContainer foodsForFurnitureContainer,RecipeContainer recipeContainer,
        CoroutineMonoBehaviour coroutineMonoBehaviour)
    {
        _bootstrapLvl2 = bootstrapLvl2;
        _productsContainer = productsContainer;
        _uiContainer = uiContainer;
        _foodsForFurnitureContainer = foodsForFurnitureContainer;
        _recipeContainer = recipeContainer;
        _gameManagerUpdate = gameManagerUpdate;
        _dataManager = dataManager;
        _uiManager = uiManager;
        
        _checks = checks;
        _score = score;
        _updateChecks = updateChecks;
        _orders = orders;
        _eventBus = eventBus;
        _timeGame = timeGame;
        _recipeService = recipeService;
        _gameOver = gameOver;
        
        _viewFactory = viewFactory;
        _productsFactory = productsFactory;
        _helperScriptFactory = helperScriptFactory;
        _checksFactory = checksFactory;
        _coroutineMonoBehaviour = coroutineMonoBehaviour; // End

        Init();
        Debug.Log("Создать объект: GameManager");
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : GameManager");
    }
    
    private void Init()
    {
        StaticManagerWithoutZenject.GameManager = this;
    }
}
