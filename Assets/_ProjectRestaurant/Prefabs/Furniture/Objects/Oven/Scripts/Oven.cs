using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace OvenFurniture
{
    public class Oven : MonoBehaviour, IUseFurniture
    {
        [SerializeField] private GameObject switchFirst;
        [SerializeField] private GameObject switchSecond;
        [SerializeField] private TimerView timerPref;
        [SerializeField] private float timeTimer;
        [SerializeField] private Transform pointUp;
        [SerializeField] private Transform positionIngredient;
        [SerializeField] private SoundsFurniture sounds;
        
        private const string ANIMATIONCLOSE = "Close";
        private const string ANIMATIONOPEN = "Open";
        
        private GameObject _ingredient;
        private GameObject _result;
        private bool _isWork;
        private bool _isHeroikTrigger;
        //private bool _isInit;
        private Heroik _heroik; // только для объекта героя, а надо и другие...
        private OvenView _ovenView;
        private OvenPoints _ovenPoints;
        private Animator _animator;
        private DecorationFurniture _decorationFurniture;
        private Outline _outline;
        private RecipeService _recipeService;
        private ProductsFactory _productsFactory;
        private FoodsForFurnitureContainer _foodsForFurnitureContainer;
        
        private IHandlerPause _pauseHandler;
        
        private List<Product> ListProduct => _foodsForFurnitureContainer.Oven.ListForFurniture;
        
        private void Awake()
        {
            _outline = GetComponent<Outline>();
            _animator = GetComponent<Animator>();
            _decorationFurniture = GetComponent<DecorationFurniture>();
        }
    
        private void Start()
        {
            TimerFurniture timerFurniture = new TimerFurniture(timerPref,timeTimer,pointUp,_pauseHandler);
            _ovenView = new OvenView(switchFirst, switchSecond,timerFurniture, _animator,_pauseHandler);
            _ovenPoints = new OvenPoints(pointUp,positionIngredient);
            _animator.SetBool(ANIMATIONCLOSE,false);
            _animator.SetBool(ANIMATIONOPEN,true);
            
            //Debug.Log("Oven Init");
        }
        
        private void OnTriggerEnter(Collider other)
        {

            if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
            {
                _heroik = other.GetComponent<Heroik>();
                EnterTrigger();
                return;
            }
            
            if (other.GetComponent<Heroik>())
            {
                _heroik = other.GetComponent<Heroik>();
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
        
        [Inject]
        private void ConstructZenject( 
            RecipeService recipeService,
            ProductsFactory productsFactory,
            FoodsForFurnitureContainer foodsForFurnitureContainer,
            IHandlerPause handlerPause)
        {
            _productsFactory = productsFactory;
            _recipeService = recipeService;
            _foodsForFurnitureContainer = foodsForFurnitureContainer;
            _pauseHandler = handlerPause;
        }
        
        // private void OnEnable()
        // {
        //     EventBus.PressE += CookingProcess;
        // }
        //
        // private void OnDisable()
        // {
        //     EventBus.PressE -= CookingProcess;
        // }
        private GameObject GiveObj(GameObject giveObj)
        {
            return giveObj;
        }
    
        private bool AcceptObject(GameObject acceptObj)
        {
            if (acceptObj == null)
            {
                Debug.Log("Объект не передался");
                return false;
            }
            _ingredient = _productsFactory.GetProduct(acceptObj,_ovenPoints.PositionIngredient,_ovenPoints.PositionIngredient, true);
            _heroik.CleanObjOnHands();
            return true;
        }
        
        private void TurnOn()
        {
            _isWork = true;
            //_ovenView.TurnOn();
        }
    
        private void TurnOff()
        {
            _isWork = false;
            //_ovenView.TurnOff();
            Destroy(_ingredient);
            _ingredient = null;
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
            _heroik.ToInteractAction.Subscribe(CookingProcess);
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
        
        private void CookingProcess()
        {
            if (CheckCookingProcess() == false)
            {
                return;
            }
            
            if (_isWork)
            {
                Debug.Log("ждите блюдо готовится");
                _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.NotWorkTableSound);
                return;
            }
            
            if (_heroik.IsBusyHands == false) // руки не заняты
            {
                if (_result != null)
                {
                    if (_heroik.TryPickUp(GiveObj(_result)))
                    {
                        sounds.PlayOneShotClip(AudioNameGamePlay.TakeOnTheTableSound);
                        CleanObjOnTable(_result);
                        return;
                    }
                    
                }
                
                _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.ForbiddenSound);
                Debug.Log("печка пуста руки тоже");
            }
            else // заняты
            {
                if (_result != null)
                {
                    Debug.Log("Сначала заберите предмет");
                    _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.NotWorkTableSound);
                    return;
                }
                
                if (AcceptObject(_heroik.TryGiveIngredient(ListProduct)))
                {
                    //sounds.PlayOneShotClip(AudioNameGamePlay.PutOnTheTableSound2);
                    TurnOn();
                    ContinueWorkAsync().Forget();
                    return;
                }
                
                Debug.Log("с предметом что-то пошло не так");
            }
        }
        private async UniTask ContinueWorkAsync()
        {
            sounds.PlayOneShotClip(AudioNameGamePlay.StartOvenSound);
            sounds.PlayClip(AudioNameGamePlay.OvenSecondSound);
            await _ovenView.StartOvenAsync();
            CreateResult();
            TurnOff();
            sounds.StopCurrentClip();
        }
        
        private bool CheckCookingProcess()
        {
            if(_isHeroikTrigger == false)
            {
                return false;
            }
            
            if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
            {
                _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.NotWorkTableSound);
                Debug.LogWarning("Печка не работает");
                return false;
            }
        
            if (CheckUseFurniture() == false)
            {
                return false;
            }

            return true;
        }
        
        // private void CreateResult()
        // {
        //     List<Product> listProducts = new List<Product>() {_ingredient.GetComponent<Product>()};
        //     Product readyObj = _recipeService.GetDish(StationType.Oven,listProducts);
        //     if (readyObj != null)
        //     {
        //         //TODO переделать на тип объкта
        //         _result = _productsFactory.GetProduct(readyObj.gameObject,_ovenPoints.PointUp, _ovenPoints.PointUp);
        //         //Destroy(readyObj);
        //     }
        //     else
        //     {
        //         Debug.LogError("Ошибка в CreateResult, такого ключа нет");
        //     }
        // }
        
        private void CreateResult()
        {
            var ingredients = new List<Product>()
            {
                _ingredient.GetComponent<Product>()
            };

            IngredientName result = _recipeService.GetDish(FurnitureName.Oven, ingredients);
            Debug.Log("result = " + result.ToString() + "");
            
            if (result == IngredientName.Rubbish)
            {
                _productsFactory.GetProduct(result, _ovenPoints.PointUp,_ovenPoints.PointUp);
                Debug.LogError("произведен мусор");
                return;
            }
            

            _result = _productsFactory.GetProduct(result, _ovenPoints.PointUp,_ovenPoints.PointUp);
        }
        
        private void CleanObjOnTable(GameObject ingredient)
        {
            Destroy(ingredient);
        }
        
    }

}
