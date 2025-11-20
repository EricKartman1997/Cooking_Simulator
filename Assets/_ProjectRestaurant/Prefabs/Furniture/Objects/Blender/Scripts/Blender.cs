using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace BlenderFurniture
{ 
    public class Blender : MonoBehaviour, IUseFurniture
    {
        [SerializeField] private TimerView timerPref;
        [SerializeField] private float timeTimer;
        [SerializeField] private Transform pointUp;
        
        [SerializeField] private Transform firstPoint;
        [SerializeField] private Transform secondPoint;
        [SerializeField] private Transform thirdPoint;
        
        private Heroik _heroik = null;
        private BlenderPoints _blenderPoints;
        private BlenderView _blenderView;
        
        private GameObject _ingredient1 = null;
        private GameObject _ingredient2 = null;
        private GameObject _ingredient3 = null;
        private GameObject _result = null;
        private bool _isWork;
        private bool _isHeroikTrigger;
        
        private Outline _outline;
        private DecorationFurniture _decorationFurniture;
        private Animator _animator;
        
        private ProductsFactory _productsFactory;
        private FoodsForFurnitureContainer _foodsForFurnitureContainer;
        private RecipeService _recipeService;
        
        private List<Product> ListProduct => _foodsForFurnitureContainer.Blender.ListForFurniture;
        
    
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _outline = GetComponent<Outline>();
            _decorationFurniture = GetComponent<DecorationFurniture>();
        }
    
        private void Start()
        {
            //_animator.SetBool("Work", false);
            TimerFurniture _timerFurniture = new TimerFurniture(timerPref,timeTimer,pointUp);
            _blenderPoints = new BlenderPoints(firstPoint, secondPoint, thirdPoint, pointUp, pointUp);
            _blenderView = new BlenderView(_timerFurniture, _animator);
            
            Debug.Log("Blender Init");
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
        
        // private void OnEnable()
        // {
        //     EventBus.PressE += CookingProcess;
        // }
        //
        // private void OnDisable()
        // {
        //     EventBus.PressE -= CookingProcess;
        // }
        
        [Inject]
        private void ConstructZenject( 
            RecipeService recipeService,
            ProductsFactory productsFactory,
            FoodsForFurnitureContainer foodsForFurnitureContainer)
        {
            _productsFactory = productsFactory;
            _recipeService = recipeService;
            _foodsForFurnitureContainer = foodsForFurnitureContainer;
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
                Debug.Log("Объект не передался");
                return false;
            }
            if (_ingredient1 == null)
            {
                _ingredient1 = _productsFactory.GetProduct(acceptObj, _blenderPoints.FirstPoint.transform, _blenderPoints.ParentFood,true);
                _heroik.CleanObjOnHands();
                return true;
            }
            else if (_ingredient2 == null)
            {
                _ingredient2 = _productsFactory.GetProduct(acceptObj, _blenderPoints.SecondPoint.transform, _blenderPoints.ParentFood,true);
                _heroik.CleanObjOnHands();
                return true;
            }
            else if (_ingredient3 == null)
            {
                _ingredient3 = _productsFactory.GetProduct(acceptObj, _blenderPoints.ThirdPoint.transform, _blenderPoints.ParentFood,true);
                _heroik.CleanObjOnHands();
                return true;
            }
            else
            {
                Debug.LogWarning("В блендере нет места");
                return false;
            }
        }
        
        private void CreateResult()
        {
            var ingredients = new List<Product>()
            {
                _ingredient1.GetComponent<Product>(),
                _ingredient2.GetComponent<Product>(),
                _ingredient3.GetComponent<Product>()
            };

            IngredientName result = _recipeService.GetDish(FurnitureName.Blender, ingredients);

            if (result == IngredientName.Rubbish)
            {
                _result = _productsFactory.GetProduct(
                    result,
                    _blenderPoints.SecondPoint.transform,
                    _blenderPoints.ParentReadyFood
                );

                Debug.Log("произведен мусор");
                return;
            }

            _result = _productsFactory.GetProduct(
                result,
                _blenderPoints.SecondPoint.transform,
                _blenderPoints.ParentReadyFood
            );
        }

    
        private void TurnOn()
        {
            _ingredient1.SetActive(false);
            _ingredient2.SetActive(false);
            _ingredient3.SetActive(false);
            _isWork = true;
            //_blenderView.TurnOn();
        }
    
        private void TurnOff()
        {
            _isWork = false;
            Object.Destroy(_ingredient1);
            Object.Destroy(_ingredient2);
            Object.Destroy(_ingredient3);
            _ingredient1 = null;
            _ingredient2 = null;
            _ingredient3 = null;
            //_blenderView.TurnOff();
        }
        
        private void CookingProcess()
        {
            if (!CheckCookingProcess())
                return;

            // ===============================
            // РУКИ СВОБОДНЫ
            // ===============================
            if (_heroik.IsBusyHands == false)
            {
                if (_isWork)
                {
                    Debug.Log("Ждите, блендер готовится");
                    return;
                }

                // Есть готовый результат → забираем
                if (_result != null)
                {
                    if (_heroik.TryPickUp(GiveObj(_result)))
                        CleanObjOnTable(_result);
                    return;
                }

                // Нет результата и нет ингредиентов
                if (_ingredient1 == null && _ingredient2 == null)
                {
                    Debug.Log("Руки пусты, ингредиентов нет");
                    return;
                }

                // Забрать 1-й ингредиент
                if (_ingredient1 != null)
                {
                    if (_heroik.TryPickUp(GiveObj(_ingredient1)))
                        CleanObjOnTable(_ingredient1);
                    return;
                }

                // Забрать 2-й ингредиент
                if (_ingredient2 != null)
                {
                    if (_heroik.TryPickUp(GiveObj(_ingredient2)))
                        CleanObjOnTable(_ingredient2);
                    return;
                }

                return;
            }

            // ===============================
            // РУКИ ЗАНЯТЫ
            // ===============================
            if (_isWork)
            {
                Debug.Log("Ждите, блендер готовится");
                return;
            }

            // Если результат уже есть — кладём некуда
            if (_result != null)
            {
                Debug.Log("Руки полные, уберите предмет");
                return;
            }

            // Пытаемся принять предмет
            if (!AcceptObject(_heroik.TryGiveIngredient(ListProduct)))
            {
                Debug.Log("С предметом что-то пошло не так");
                return;
            }

            // Если все слоты заполнены — запускаем работу
            if (_ingredient1 != null && _ingredient2 != null && _ingredient3 != null)
            {
                TurnOn();
                ContinueWorkAsync().Forget(); 
            }
        }
        
        private bool CheckCookingProcess()
        {
            if(_isHeroikTrigger == false)
            {
                return false;
            }
            
            if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
            {
                Debug.LogWarning("Блендер не работает");
                return false;
            }
            
            if (CheckUseFurniture() == false)
            {
                return false;
            }
            return true;
        }

        private async UniTask ContinueWorkAsync()
        {
            await _blenderView.StartBlendAsync();
            CreateResult();
            TurnOff();
        }
        
        private void CleanObjOnTable(GameObject ingredient)
        {
            Destroy(ingredient);
        }
        
    }

}
