using System.Collections;
using UnityEngine;

public class BootstrapLVL2 : MonoBehaviour
{
    //ScriptableObjects
    [SerializeField] private ChecksFactory checksFactory;
    [SerializeField] private FoodsForFurnitureContainer foodsForFurnitureContainer;
    [SerializeField] private RecipeContainer recipeContainer;
    
    //[SerializeField] private RecipeService _recipeService;
    
    private UIContainer _uiContainer;
    private ProductsContainer _productsContainer;
    private GameManagerUpdate _gameManagerUpdate;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    
    private GameManager _gameManager;
    private UIManager _uiManager; 
    private DataManager _dataManager; //
    private Checks _checks;
    private Score _score;
    private UpdateChecks _updateChecks;
    private Orders _orders;
    private EventBus _eventBus;
    private TimeGame _timeGame;
    private GameOver _gameOver;
    private RecipeService _recipeService;
    
    private ViewFactory _viewFactory;
    private ProductsFactory _productsFactory;
    private HelperScriptFactory _helperScriptFactory;
    private bool _isAllInit;
    private bool _isAllInitScriptableObject;

    public bool IsAllInitScriptableObject => _isAllInitScriptableObject;
    public bool IsAllInit => _isAllInit;
    

    private void Awake()
    {
        StartCoroutine(Initialize());// переделать
    }

    private IEnumerator Initialize()// переделать
    {
        StartCoroutine(AllInitScriptableObject());
        yield return new WaitUntil(() => _isAllInitScriptableObject);
        
        _eventBus = new EventBus();
        _uiContainer = GetComponent<UIContainer>();
        _productsContainer = GetComponent<ProductsContainer>();
        _gameManagerUpdate = GetComponent<GameManagerUpdate>();
        _coroutineMonoBehaviour = GetComponent<CoroutineMonoBehaviour>();

        _uiManager = new UIManager(_coroutineMonoBehaviour);
        //_dataManager = new DataManager();
        
        _checks = new Checks(_coroutineMonoBehaviour);
        _updateChecks = new UpdateChecks(_checks,3f,_coroutineMonoBehaviour);
        _orders = new Orders(_coroutineMonoBehaviour);
        _timeGame = new TimeGame(_coroutineMonoBehaviour);
        _score = new Score(_coroutineMonoBehaviour);
        _recipeService = new RecipeService(recipeContainer,_productsContainer);
        _gameOver = new GameOver(_coroutineMonoBehaviour);
        
        _viewFactory = new ViewFactory(_productsContainer,_coroutineMonoBehaviour);
        _productsFactory = new ProductsFactory(_productsContainer,_coroutineMonoBehaviour);
        _helperScriptFactory = new HelperScriptFactory(_coroutineMonoBehaviour);
        
        _gameManager = new GameManager(this,_productsContainer,_uiContainer,_gameManagerUpdate,
            _dataManager,_uiManager,_checks,_score,_updateChecks,_orders,
            _eventBus,_timeGame,_recipeService,_gameOver,_viewFactory,_productsFactory,
            _helperScriptFactory,checksFactory,
            foodsForFurnitureContainer,recipeContainer,_coroutineMonoBehaviour);
        
        StartCoroutine(AllInitServices());
        yield return new WaitUntil(() => _isAllInit);
        
        Debug.Log("Инициализация Завершена");
        //_isAllInit = true;
    }

    private IEnumerator AllInitServices()
    {
        yield return new WaitUntil(() => _eventBus.IsInit);
        yield return new WaitUntil(() => _checks.IsInit);
        yield return new WaitUntil(() => _updateChecks.IsInit);
        yield return new WaitUntil(() => _orders.IsInit);
        yield return new WaitUntil(() => _timeGame.IsInit);
        yield return new WaitUntil(() => _score.IsInit);
        yield return new WaitUntil(() => _gameOver.IsInit);
        yield return new WaitUntil(() => _uiManager.IsInit);
        yield return new WaitUntil(() => _viewFactory.IsInit);
        yield return new WaitUntil(() => _productsFactory.IsInit);
        yield return new WaitUntil(() => _helperScriptFactory.IsInit);
        yield return new WaitUntil(() => _recipeService.IsInit);
        _isAllInit = true;
    }

    private IEnumerator AllInitScriptableObject()
    {
        yield return new WaitUntil(() => recipeContainer.IsInit);
        yield return new WaitUntil(() => checksFactory.IsInit);
        yield return new WaitUntil(() => foodsForFurnitureContainer.IsInit);
        _isAllInitScriptableObject = true;
    }
    
}
