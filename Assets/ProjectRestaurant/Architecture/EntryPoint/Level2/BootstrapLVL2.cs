using System;
using System.Collections;
using UnityEngine;

public class BootstrapLVL2 : MonoBehaviour
{
    private FieldsForScriptContainer _fieldsContainer;
    private ProductsContainer _productsContainer;
    private CheckContainer _checkContainer;
    private GameManagerUpdate _gameManagerUpdate;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    
    private GameManager _gameManager;
    private UIManager _uiManager; //
    private DataManager _dataManager; //
    private Checks _checks;
    private Score _score;
    private UpdateChecks _updateChecks;
    private Orders _orders;
    private OrdersUI _ordersUI;
    private EventBus _eventBus;
    private TimeGame _timeGame;
    private GameOver _gameOver;
    
    private ViewFactory _viewFactory;
    private ProductsFactory _productsFactory;
    private HelperScriptFactory _helperScriptFactory;
    
    private void Awake()
    {
        StartCoroutine(Initialize());// переделать
    }

    private IEnumerator Initialize()// переделать
    {
        _fieldsContainer = GetComponent<FieldsForScriptContainer>();
        _productsContainer = GetComponent<ProductsContainer>();
        _checkContainer = GetComponent<CheckContainer>();
        _gameManagerUpdate = GetComponent<GameManagerUpdate>();
        _coroutineMonoBehaviour = GetComponent<CoroutineMonoBehaviour>();

        _uiManager = new UIManager(_coroutineMonoBehaviour);
        //_dataManager = new DataManager();
        
        _eventBus = new EventBus();
        _checks = new Checks(_checkContainer,_coroutineMonoBehaviour);
        _updateChecks = new UpdateChecks(_checks,3f,_coroutineMonoBehaviour);
        _orders = new Orders(_coroutineMonoBehaviour);
        _ordersUI = new OrdersUI(_orders,_coroutineMonoBehaviour);
        _timeGame = new TimeGame(_coroutineMonoBehaviour);
        _score = new Score(_coroutineMonoBehaviour);
        _gameOver = new GameOver(_coroutineMonoBehaviour);
        
        _viewFactory = new ViewFactory(_productsContainer,_coroutineMonoBehaviour);
        _productsFactory = new ProductsFactory(_productsContainer,_coroutineMonoBehaviour);
        _helperScriptFactory = new HelperScriptFactory(_coroutineMonoBehaviour);
        
        _gameManager = new GameManager(this,_productsContainer,_checkContainer,_fieldsContainer,_gameManagerUpdate,_dataManager,_uiManager,_checks,_score,_updateChecks,_orders,_ordersUI,_eventBus,_timeGame,_gameOver,_viewFactory,_productsFactory,_helperScriptFactory,_coroutineMonoBehaviour);
        
        yield return new WaitUntil(() => _timeGame.IsInit);// переделать
        yield return new WaitUntil(() => _updateChecks.IsInit);// переделать
        _gameManagerUpdate.IsWork = true;// переделать
        
        Debug.Log("Создать объект: BootstrapLVL2");
    }
    
}
