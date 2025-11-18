using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuvideFurniture
{
    public class SuvideFurniture : MonoBehaviour, IUseFurniture
    {
        private const string DISH1 = "DISH1";
        private const string DISH2 = "DISH2";
        private const string DISH3 = "DISH3";
        
        [SerializeField] private GameObject waterPrefab;
        [SerializeField] private GameObject switchTimePrefab;
        [SerializeField] private GameObject switchTemperPrefab;
        [SerializeField] private TimerView timerPref;
        [SerializeField] private float timeTimer;
        
        [SerializeField] private Transform pointTimer1;
        [SerializeField] private Transform pointTimer2;
        [SerializeField] private Transform pointTimer3;
        [SerializeField] private Transform pointIngredient1;
        [SerializeField] private Transform pointIngredient2;
        [SerializeField] private Transform pointIngredient3;
        [SerializeField] private Transform pointResult1;
        [SerializeField] private Transform pointResult2;
        [SerializeField] private Transform pointResult3;
        
        private GameObject _dish1;
        private GameObject _dish2;
        private GameObject _dish3;
        
        private bool _isHeroikTrigger;
        private bool _cookingdish1;
        private bool _cookingdish2;
        private bool _cookingdish3;
        private bool _isInit;
    
        private Outline _outline;
        private DecorationFurniture _decorationFurniture;
        private SuvideView _suvideView;
        private SuvidePoints _suvidePoints;
        private Heroik _heroik;
        private Animator _animator;
        private FoodsForFurnitureContainer _foodsForFurnitureContainer;
        private GameManager _gameManager;
        private ProductsContainer _productsContainer;
        private RecipeService _recipeService;
        
        private bool IsAllInit => _gameManager.BootstrapLvl2.IsAllInit;
        
        private List<Product> ListProduct => _foodsForFurnitureContainer.Suvide.ListForFurniture;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _outline = GetComponent<Outline>();
            _decorationFurniture = GetComponent<DecorationFurniture>();
        }
        
        private IEnumerator Start()
        {
            while (_gameManager == null)
            {
                _gameManager = StaticManagerWithoutZenject.GameManager;
                yield return null;
            }
            
            while (_productsContainer == null)
            {
                _productsContainer = _gameManager.ProductsContainer;
                yield return null;
            }
            
            while (_foodsForFurnitureContainer== null)
            {
                _foodsForFurnitureContainer = _gameManager.FoodsForFurnitureContainer;
                yield return null;
            }
            
            while (_recipeService== null)
            {
                _recipeService = _gameManager.RecipeService;
                yield return null;
            }
            
            while (IsAllInit == false)
            {
                yield return null;
            }
            
            TimerFurniture timerFurniture1 = new TimerFurniture(timerPref,timeTimer,pointTimer1);
            TimerFurniture timerFurniture2 = new TimerFurniture(timerPref,timeTimer,pointTimer2);
            TimerFurniture timerFurniture3 = new TimerFurniture(timerPref,timeTimer,pointTimer3);
            _suvidePoints = new SuvidePoints(pointIngredient1, pointIngredient2, pointIngredient3, pointResult1, pointResult2, pointResult3);
            _suvideView = new SuvideView(waterPrefab, switchTimePrefab, switchTemperPrefab, timerFurniture1, timerFurniture2, timerFurniture3, _animator);

            _isInit = true;
            Debug.Log("SuvideFurniture Init");
        }
        private void OnTriggerEnter(Collider other)
        {
            if (_isInit == false)
            {
                Debug.Log("Инициализация не закончена");
                return;
            }
            
            if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
            {
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
            if (_isInit == false)
            {
                Debug.Log("Инициализация не закончена");
                return;
            }
            
            if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
            {
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
    
        private bool AcceptObject(GameObject acceptObj,string TOKEN)
        {
            if (acceptObj == null)
            {
                Debug.Log("Объект не передался");
                return false;
            }
            if (TOKEN == DISH1)
            {
                _dish1 = _gameManager.ProductsFactory.GetProduct(acceptObj, _suvidePoints.PointIngredient1, _suvidePoints.PointIngredient1, true,true);
                _heroik.CleanObjOnHands();
                _cookingdish1 = true;
                return true;
            }
            
            if (TOKEN == DISH2)
            {
                _dish2 = _gameManager.ProductsFactory.GetProduct(acceptObj, _suvidePoints.PointIngredient2, _suvidePoints.PointIngredient2, true,true);
                _heroik.CleanObjOnHands();
                _cookingdish2 = true;
                return true;
            }
            
            if (TOKEN == DISH3)
            {
                _dish3 = _gameManager.ProductsFactory.GetProduct(acceptObj, _suvidePoints.PointIngredient3, _suvidePoints.PointIngredient3, true,true);
                _heroik.CleanObjOnHands();
                _cookingdish3 = true;
                return true;
            }
            Debug.Log("ошибка в AcceptObject");
            return false;
        }
    
        private void CreateResult(GameObject obj, string TOKEN)
        {
            var ingredients = new List<Product>()
            {
                obj.GetComponent<Product>()
            };

            IngredientName result = _recipeService.GetDish(FurnitureName.Suvide, ingredients);

            // --- определение позиции и ссылки на ячейку ---
            Transform spawnPoint = null;
            ref GameObject targetDish = ref _dish1;

            if (TOKEN == DISH1)
            {
                spawnPoint = _suvidePoints.PointResult1;
                targetDish = ref _dish1;
            }
            else if (TOKEN == DISH2)
            {
                spawnPoint = _suvidePoints.PointResult2;
                targetDish = ref _dish2;
            }
            else if (TOKEN == DISH3)
            {
                spawnPoint = _suvidePoints.PointResult3;
                targetDish = ref _dish3;
            }
            else
            {
                Debug.LogError("Некорректный TOKEN в CreateResult");
                return;
            }

            // --- если нет рецепта — создаём мусор ---
            if (result == IngredientName.Rubbish)
            {
                _gameManager.ProductsFactory.GetProduct(result, spawnPoint, spawnPoint);
                Debug.LogError("Произведён мусор");
                return;
            }

            // --- создаём новый правильный продукт ---
            if (targetDish != null)
                Destroy(targetDish);

            targetDish = _gameManager.ProductsFactory.GetProduct(result, spawnPoint, spawnPoint);
        }

    
        private void TurnOff(string TOKEN)
        {
            if (TOKEN == DISH1)
            {
                _cookingdish1 = false;
                _suvideView.TurnOff();
                return;
            }
            
            if (TOKEN == DISH2)
            {
                _cookingdish2 = false;
                _suvideView.TurnOff();
                return;
            }
            
            if (TOKEN == DISH3)
            {
                _cookingdish3 = false;
                _suvideView.TurnOff();
                return;
            }
            
            Debug.LogError("Ошибка в TurnOff");
        }
    
        private void TurnOn(string TOKEN)
        {
            if (TOKEN == DISH1)
            {
                _suvideView.TurnOn();
                ChangeView(); 
                return;
            }
            
            if (TOKEN == DISH2)
            {
                _suvideView.TurnOn();
                ChangeView(); 
                return;
            }
            
            if (TOKEN == DISH3)
            {
                _suvideView.TurnOn();
                ChangeView(); 
                return;
            }
            Debug.LogError("Ошибка в TurnOn");
        }
    
        private void CookingProcess()
        {
            if (CheckCookingProcess() == false)
            {
                return;
            }
    
            if (_heroik.IsBusyHands == false)
            {
                if (_dish1 != null && _cookingdish1 == false)
                {
                    if (_heroik.TryPickUp(GiveObj(_dish1)))
                    {
                        CleanObjOnTable(_dish1);
                    }
                    ChangeView();
                    return;
                }
                
                if (_dish2 != null && _cookingdish2 == false)
                {
                    if (_heroik.TryPickUp(GiveObj(_dish2)))
                    {
                        CleanObjOnTable(_dish2);
                    }
                    ChangeView();
                    return;
                }
                
                if (_dish3 != null && _cookingdish3 == false)
                {
                    if (_heroik.TryPickUp(GiveObj(_dish3)))
                    {
                        CleanObjOnTable(_dish3);
                    }
                    ChangeView();
                    return;
                }
                
                Debug.Log("Сувид пуст руки тоже");
                ChangeView();
                return;
            }
    
            if (_heroik.IsBusyHands == true)
            {
                if (_dish1 == null)
                {
                    if (AcceptObject(_heroik.TryGiveIngredient(ListProduct), DISH1))
                    {
                        TurnOn(DISH1);
                        StartCoroutine(ContinueWorkCoroutine(_dish1,DISH1));
                        ChangeView();
                    }
                    else
                    {
                        Debug.Log("с предметом что-то пошло не так");
                    }
                    return;
                }
                
                if (_dish2 == null)
                {
                    if (AcceptObject(_heroik.TryGiveIngredient(ListProduct), DISH2))
                    {
                        TurnOn(DISH2);
                        StartCoroutine(ContinueWorkCoroutine(_dish2,DISH2));
                        ChangeView();
                    }
                    else
                    {
                        Debug.Log("с предметом что-то пошло не так");
                    }
                    return;
                }
                
                if (_dish3 == null)
                {
                    if (AcceptObject(_heroik.TryGiveIngredient(ListProduct), DISH3))
                    {
                        TurnOn(DISH3);
                        StartCoroutine(ContinueWorkCoroutine(_dish3,DISH3));
                        ChangeView();
                    }
                    else
                    {
                        Debug.Log("с предметом что-то пошло не так");
                    }
                    return;
                }
                
                Debug.LogWarning("сувид заполнен");
                ChangeView();
                return;
            }
        }
        
        private IEnumerator ContinueWorkCoroutine(GameObject obj, string TOKEN)
        {
            if (TOKEN == DISH1)
            {
                StartCoroutine(_suvideView.Timer1.StartTimer());
                yield return new WaitWhile(() => _suvideView.Timer1.IsWork);
                TurnOff(TOKEN);
                CreateResult(obj,TOKEN);
                ChangeView();
            }

            if (TOKEN == DISH2)
            {
                StartCoroutine(_suvideView.Timer2.StartTimer());
                yield return new WaitWhile(() => _suvideView.Timer2.IsWork);
                TurnOff(TOKEN);
                CreateResult(obj,TOKEN);
                ChangeView();
            }

            if (TOKEN == DISH3)
            {
                StartCoroutine(_suvideView.Timer3.StartTimer());
                yield return new WaitWhile(() => _suvideView.Timer3.IsWork);
                TurnOff(TOKEN);
                CreateResult(obj,TOKEN);
                ChangeView();
            }
        }
        
        private void ChangeView()
        {
            if (_cookingdish1 == true || _cookingdish2 == true || _cookingdish3 == true )
            {
                _suvideView.WorkingSuvide();
            }
            else
            {
                _suvideView.NotWorkingSuvide();
            }
        }

        private bool CheckCookingProcess()
        {
            if (_isInit == false)
            {
                Debug.Log("Инициализация не закончена");
                return false;
            }
            
            if(_isHeroikTrigger == false)
            {
                return false;
            }
                
            if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
            {
                Debug.LogWarning("Сувид не работает");
                return false;
            }
        
            if (CheckUseFurniture() == false)
            {
                return false;
            }
            return true;
        }
        
        private void CleanObjOnTable(GameObject ingredient)
        {
            Destroy(ingredient);
        }
    }

}
