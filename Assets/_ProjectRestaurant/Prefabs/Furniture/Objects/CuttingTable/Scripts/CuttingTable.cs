using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CuttingTableFurniture
{
    public class CuttingTable : MonoBehaviour, IUseFurniture
    {
        [SerializeField] private TimerView timerPref;
        [SerializeField] private float timeTimer;
        [SerializeField] private Transform positionIngredient1; 
        [SerializeField] private Transform positionIngredient2; 
        [SerializeField] private Transform positionResult;
        [SerializeField] private Transform pointNotif;
        [SerializeField] private SoundsFurniture sounds;
        
        private bool _isWork = false;
        private bool _isHeroikTrigger = false;
        private GameObject _ingredient1 = null;
        private GameObject _ingredient2 = null;
        private GameObject _result = null;
        
        private Heroik _heroik = null;
        private DecorationFurniture _decorationFurniture;
        private Outline _outline;
        private Animator _animator;
        private CuttingTablePoints _cuttingTablePoints;
        private CuttingTableView _cuttingTableView;
        
        private FoodsForFurnitureContainer _foodsForFurnitureContainer;
        private RecipeService _recipeService;
        private ProductsFactory _productsFactory;
        
        private IHandlerPause _pauseHandler;
        private INotificationGetter _notificationManager;
        
        private List<Product> ListProduct => _foodsForFurnitureContainer.CuttingTable.ListForFurniture;
    
        
        [Inject]
        private void ConstructZenject(FoodsForFurnitureContainer foodsForFurnitureContainer,RecipeService recipeService,
            ProductsFactory productsFactory,IHandlerPause pauseHandler,INotificationGetter notificationManager)
        {
            _foodsForFurnitureContainer = foodsForFurnitureContainer;
            _recipeService = recipeService;
            _productsFactory = productsFactory;
            _pauseHandler = pauseHandler;
            _notificationManager = notificationManager;
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _outline = GetComponent<Outline>();
            _decorationFurniture = GetComponent<DecorationFurniture>();
        }
    
        private void Start()
        {
            _animator.SetBool("Work", false);
            TimerFurniture timerFurniture = new TimerFurniture(timerPref,timeTimer,positionResult,_pauseHandler);
            _cuttingTablePoints = new CuttingTablePoints(positionIngredient1,positionIngredient2,positionResult);
            _cuttingTableView = new CuttingTableView(_animator,timerFurniture);
            
            //Debug.Log("CuttingTable Init");
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
            {
                _heroik = other.GetComponent<Heroik>();
                _heroik.ToInteractAction.Subscribe(CookingProcess);
                EnterTrigger();
                return;
            }
            
            if (other.GetComponent<Heroik>())
            {
                _heroik = other.GetComponent<Heroik>();
                _heroik.ToInteractAction.Subscribe(CookingProcess);
                EnterTrigger();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
            {
                _heroik.ToInteractAction.Unsubscribe(CookingProcess);
                ExitTrigger();
                return;
            }
            
            if (other.GetComponent<Heroik>())
            {
                _heroik.ToInteractAction.Unsubscribe(CookingProcess);
                ExitTrigger();
            }
        }
        
        public void UpdateCondition()
        {
            if (CheckUseFurniture() == false)
            {
                _outline.OutlineWidth = 0f;
            }
            
        }
    
        private void EnterTrigger()
        {
            _outline.OutlineWidth = 2f;
            _isHeroikTrigger = true;
            _heroik.CurrentUseFurniture = this;
        }
    
        private void ExitTrigger()
        {
            _outline.OutlineWidth = 0f;
            _isHeroikTrigger = false;
        }
    
        private bool CheckUseFurniture()
        {
            if (ReferenceEquals(_heroik.CurrentUseFurniture, this))
            {
                return true;
            }

            return false;
        }
        
        private GameObject GiveObj(GameObject giveObj)
        {
            return giveObj;
        }
        
        private bool AcceptObject(GameObject acceptObj)
        {
            if (acceptObj == null)
            {
                InvokeNotification().Forget();
                Debug.Log("Объект не передался");
                return false;
            }
            if (_ingredient1 == null)
            {
                _ingredient1 = _productsFactory.GetProduct(acceptObj, _cuttingTablePoints.PositionIngredient1,
                    _cuttingTablePoints.PositionIngredient1,true);
                _heroik.CleanObjOnHands();
                return true;
            }
            else if (_ingredient2 == null)
            {
                 _ingredient2 = _productsFactory.GetProduct(acceptObj, _cuttingTablePoints.PositionIngredient2,
                    _cuttingTablePoints.PositionIngredient2,true);
                 _heroik.CleanObjOnHands();
                 return true;
            }
            else
            {
                Debug.LogWarning("На нарезочном столе нет места");
                return false; 
            }
            
        }
        
        private void TurnOn()
        {
            _isWork = true;
            _ingredient1.SetActive(false);
            _ingredient2.SetActive(false);
            //_cuttingTableView.TurnOn();
        }
        
        private void TurnOff()
        {
            _isWork = false;
            Object.Destroy(_ingredient1);
            Object.Destroy(_ingredient2);
            _ingredient1 = null;
            _ingredient2 = null;
            //_cuttingTableView.TurnOff();
        }
        
        private void CreateResult()
        {
            var ingredients = new List<Product>()
            {
                _ingredient1.GetComponent<Product>(),
                _ingredient2.GetComponent<Product>()
            };

            // Новый универсальный вызов
            IngredientName result = _recipeService.GetDish(FurnitureName.CuttingTable, ingredients);

            if (result == IngredientName.Rubbish)
            {
                _productsFactory.GetProduct(
                    result,
                    _cuttingTablePoints.PositionResult,
                    _cuttingTablePoints.PositionResult
                );

                Debug.Log("произведен мусор");
            }

            _result = _productsFactory.GetProduct(
                result,
                _cuttingTablePoints.PositionResult,
                _cuttingTablePoints.PositionResult
            );
        }
        
        private void CookingProcess()
        {
            if (CheckCookingProcess() == false)
            {
                return;
            }
            
            if (_isWork)
            {
                Debug.Log("ждите блюдо готовится");
                InvokeNotification().Forget();
                return;
            }
            
            if(_heroik.IsBusyHands == false) // руки не заняты
            {
                if (_result == null)
                {
                    if (_ingredient1 == null)
                    {
                        Debug.Log("У вас пустые руки и прилавок пуст");
                        InvokeNotification().Forget();
                        return;
                    }
                         
                    if (_heroik.TryPickUp(GiveObj(_ingredient1))) //есть первый ингредиент // забираете первый ингредиент 
                    {
                        sounds.PlayOneShotClip(AudioNameGamePlay.TakeOnTheTableSound);
                        CleanObjOnTable(_ingredient1);
                    }
                    return;
                }
                   
                if (_heroik.TryPickUp(GiveObj(_result)))  //есть результат // забрать результат
                {
                    sounds.PlayOneShotClip(AudioNameGamePlay.TakeOnTheTableSound);
                    CleanObjOnTable(_result);
                }
                Debug.Log("Вы забрали конечный продукт");
            }
            else // заняты
            {
                if (_result == null)
                {
                    if (_ingredient1 == null)// ингредиентов нет
                    {
                        AcceptObject(_heroik.TryGiveIngredient(ListProduct));
                        sounds.PlayOneShotClip(AudioNameGamePlay.PutTheBerryBlender);
                        return;
                    }
                    
                    if (AcceptObject(_heroik.TryGiveIngredient(ListProduct))) // есть первый ингредиент
                    {
                        sounds.PlayOneShotClip(AudioNameGamePlay.PutTheBerryBlender);
                        TurnOn(); 
                        ContinueWorkAsync().Forget();
                        return;
                    }
                    
                    InvokeNotification().Forget();
                    Debug.Log("с предметом что-то пошло не так");
                    return;
                }
                
                InvokeNotification().Forget();
                Debug.Log("Сначала уберите предмет из рук");
                        
            }
        }
        
        private async UniTask ContinueWorkAsync()
        {
            sounds.PlayClip(AudioNameGamePlay.CuttingTableSound);
            await _cuttingTableView.StartCuttingTableAsync();
            CreateResult();
            TurnOff();
            sounds.StopCurrentClip();
        }
        
        private void CleanObjOnTable(GameObject ingredient)
        {
            Destroy(ingredient);
        }
        
        private bool CheckCookingProcess()
        {
            if(_isHeroikTrigger == false)
            {
                return false;
            }
        
            if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
            {
                Debug.Log("Стол не работает");
                InvokeNotification().Forget();
                return false;
            }
        
            if (CheckUseFurniture() == false)
            {
                //Debug.Log("Зашел4");
                return false;
            }

            return true;
        }
        
        private async UniTask InvokeNotification(bool isReady = false)
        {
            await _notificationManager.GetNotification(pointNotif, isReady);
            _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.NotWorkTableSound);
        }
        
    }
    
}
