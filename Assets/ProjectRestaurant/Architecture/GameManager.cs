using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IDisposable
{
    // Managers
    private BootstrapLVL2 _bootstrapLvl2;
    private ProductsContainer _productsContainer;
    private CheckContainer _checkContainer;
    private FieldsForScriptContainer _fieldsContainer;
    private GameManagerUpdate _gameManagerUpdate;
    private DataManager _dataManager;
    
    // Scripts
    private Checks _checks;
    private Score _score;
    private UpdateChecks _updateChecks;
    private Orders _orders;
    private OrdersUI _ordersUI;
    private EventBus _eventBus;
    private TimeGame _timeGame;
    private GameOver _gameOver;
    
    // Factories
    private ViewFactory _viewFactory;
    private ProductsFactory _productsFactory;
    private HelperScriptFactory _helperScriptFactory;

    public BootstrapLVL2 BootstrapLvl2 => _bootstrapLvl2;
    public ProductsContainer ProductsContainer => _productsContainer;
    public CheckContainer CheckContainer => _checkContainer;
    public FieldsForScriptContainer FieldsContainer => _fieldsContainer;
    public DataManager DataManager => _dataManager;

    public Checks Checks => _checks;
    public Score Score => _score;
    public UpdateChecks UpdateChecks => _updateChecks;
    public Orders Orders => _orders;
    public OrdersUI OrdersUI => _ordersUI;
    public EventBus EventBus => _eventBus;
    public TimeGame TimeGame => _timeGame;
    public GameOver GameOver => _gameOver;
    
    public ViewFactory ViewFactory => _viewFactory;
    public ProductsFactory ProductsFactory => _productsFactory;
    public HelperScriptFactory HelperScriptFactory => _helperScriptFactory;

    public GameManager(BootstrapLVL2 bootstrapLvl2, ProductsContainer productsContainer, CheckContainer checkContainer, FieldsForScriptContainer fieldsContainer, GameManagerUpdate gameManagerUpdate, DataManager dataManager, Checks checks, Score score, UpdateChecks updateChecks, Orders orders, OrdersUI ordersUI, EventBus eventBus, TimeGame timeGame, GameOver gameOver, ViewFactory viewFactory, ProductsFactory productsFactory, HelperScriptFactory helperScriptFactory)
    {
        _bootstrapLvl2 = bootstrapLvl2;
        _productsContainer = productsContainer;
        _checkContainer = checkContainer;
        _fieldsContainer = fieldsContainer;
        _gameManagerUpdate = gameManagerUpdate;
        _dataManager = dataManager;
        _checks = checks;
        _score = score;
        _updateChecks = updateChecks;
        _orders = orders;
        _ordersUI = ordersUI;
        _eventBus = eventBus;
        _timeGame = timeGame;
        _gameOver = gameOver;
        _viewFactory = viewFactory;
        _productsFactory = productsFactory;
        _helperScriptFactory = helperScriptFactory;

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
