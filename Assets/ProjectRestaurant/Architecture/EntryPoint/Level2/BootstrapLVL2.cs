using System;
using UnityEngine;

public class BootstrapLVL2 : MonoBehaviour
{
    [SerializeField] private FieldsForScriptContainer fieldsContainer;
    [SerializeField] private ProductsContainer productsContainer;
    [SerializeField] private CheckContainer checkContainer;
    [SerializeField] private GameManagerUpdate gameManagerUpdate;
    
    private GameManager _gameManager;
    private DataManager _dataManager;
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
        _eventBus = new EventBus();
        _checks = new Checks(fieldsContainer.CheckContainer, fieldsContainer.Content);
        _updateChecks = new UpdateChecks(_checks,3f);
        _orders = new Orders();
        _ordersUI = new OrdersUI(_orders,fieldsContainer.Scoretext);
        _timeGame = new TimeGame(fieldsContainer.TimeText);
        _score = new Score(_timeGame);
        _gameOver = new GameOver(fieldsContainer.WindowScore,fieldsContainer.ScoreNumbersText,fieldsContainer.TimeNumbersText,fieldsContainer.AssignmentNumbersTimeText,fieldsContainer.ContinueButton);
        
        _viewFactory = new ViewFactory(productsContainer);
        _productsFactory = new ProductsFactory(productsContainer);
        _helperScriptFactory = new HelperScriptFactory();
        
        _gameManager = new GameManager(this,productsContainer,checkContainer,fieldsContainer,gameManagerUpdate,_dataManager,_checks,_score,_updateChecks,_orders,_ordersUI,_eventBus,_timeGame,_gameOver,_viewFactory,_productsFactory,_helperScriptFactory);
        
        Debug.Log("Создать объект: BootstrapLVL2");
    }
    
}
