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
        
        private List<Product> ListProduct => _foodsForFurnitureContainer.CuttingTable.ListForFurniture;
    
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _outline = GetComponent<Outline>();
            _decorationFurniture = GetComponent<DecorationFurniture>();
        }
    
        private void Start()
        {
            _animator.SetBool("Work", false);
            TimerFurniture timerFurniture = new TimerFurniture(timerPref,timeTimer,positionResult);
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
        
        [Inject]
        private void ConstructZenject(FoodsForFurnitureContainer foodsForFurnitureContainer,RecipeService recipeService,ProductsFactory productsFactory)
        {
            _foodsForFurnitureContainer = foodsForFurnitureContainer;
            _recipeService = recipeService;
            _productsFactory = productsFactory;
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

                Debug.LogError("произведен мусор");
                return;
            }

            _result = _productsFactory.GetProduct(
                result,
                _cuttingTablePoints.PositionResult,
                _cuttingTablePoints.PositionResult
            );
        }

        
        private void CookingProcess()
        {
            if(_isHeroikTrigger == false)
            {
                return;
            }
            
            if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
            {
                Debug.LogWarning("Стол не работает");
                return;
            }
            
            if (CheckUseFurniture() == false)
            {
                return;
            }
            
            if(_heroik.IsBusyHands == false) // руки не заняты
            {
                if (_isWork)
                {
                    Debug.Log("ждите блюдо готовится");
                }
                else
                {
                    if (_result == null)
                    {
                        if (_ingredient1 == null)
                        {
                            Debug.Log("У вас пустые руки и прилавок пуст");
                        }
                        else //есть первый ингредиент // забираете первый ингредиент 
                        {
                            if (_heroik.TryPickUp(GiveObj(_ingredient1)))
                            {
                                CleanObjOnTable(_ingredient1);
                            }
                        }
                    }
                    else //есть результат // забрать результат
                    {
                        if (_heroik.TryPickUp(GiveObj(_result)))
                        {
                            CleanObjOnTable(_result);
                        }
                        Debug.Log("Вы забрали конечный продукт"); 
                    }
                }
            }
            else // заняты
            {
                if (_isWork)
                {
                    Debug.Log("ждите блюдо готовится");
                }
                else
                {
                    if (_result == null)
                    {
                        if (_ingredient1 == null)// ингредиентов нет
                        {
                            AcceptObject(_heroik.TryGiveIngredient(ListProduct));
                        }
                        else// есть первый ингредиент
                        {
                            if (AcceptObject(_heroik.TryGiveIngredient(ListProduct)))
                            {
                                TurnOn(); 
                                ContinueWorkAsync().Forget();
                            }
                            else
                            {
                                Debug.Log("с предметом что-то пошло не так");
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Сначала уберите предмет из рук");
                    }
                            
                }
                        
            }
        }
        
        private async UniTask ContinueWorkAsync()
        {
            await _cuttingTableView.StartCuttingTableAsync();
            CreateResult();
            TurnOff();
        }
        
        private void CleanObjOnTable(GameObject ingredient)
        {
            Destroy(ingredient);
        }
        
    }
}
