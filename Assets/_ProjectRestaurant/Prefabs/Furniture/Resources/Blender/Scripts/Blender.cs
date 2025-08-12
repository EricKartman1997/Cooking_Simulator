using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BlenderFurniture
{
    public class Blender : MonoBehaviour,IGiveObj, IAcceptObject, ITurnOffOn
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
        private bool _isInit;
        
        private Outline _outline;
        private DecorationFurniture _decorationFurniture;
        private Animator _animator;
        private ProductsContainer _productsContainer;
        private FoodsForFurnitureContainer _foodsForFurnitureContainer;
        private GameManager _gameManager;
        private RecipeService _recipeService;
        
        private bool IsAllInit => _gameManager.BootstrapLvl2.IsAllInit;
        
        private List<Product> ListProduct => _foodsForFurnitureContainer.Blender.ListForFurniture;
        
    
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
            
            //_animator.SetBool("Work", false);
            TimerFurniture _timerFurniture = new TimerFurniture(timerPref,timeTimer,pointUp);
            _blenderPoints = new BlenderPoints(firstPoint, secondPoint, thirdPoint, pointUp, pointUp);
            _blenderView = new BlenderView(_timerFurniture, _animator);
            
            _isInit = true;
            Debug.Log("Blender Init");

        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_isInit == false)
            {
                Debug.Log("Инициализация не закончена");
                return;
            }
            
            if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
            {
                _outline.OutlineWidth = 2f;
                _isHeroikTrigger = true;
                return;
            }
            
            if (other.GetComponent<Heroik>())
            {
                _heroik = other.GetComponent<Heroik>();
                _outline.OutlineWidth = 2f;
                _isHeroikTrigger = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (_isInit == false)
            {
                Debug.Log("Инициализация не закончена");
                return;
            }
            
            if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
            {
                _isHeroikTrigger = false;
                _outline.OutlineWidth = 0f;
                return;
            }
            
            if (other.GetComponent<Heroik>())
            {
                _heroik = null;
                _isHeroikTrigger = false;
                _outline.OutlineWidth = 0f;
            }
        }
        
        private void OnEnable()
        {
            EventBus.PressE += CookingProcess;
        }
    
        private void OnDisable()
        {
            EventBus.PressE -= CookingProcess;
        }
        
        public GameObject GiveObj(ref GameObject giveObj) 
        {
            return giveObj;
        }
    
        public void AcceptObject(GameObject acceptObj)
        {
            if (_ingredient1 == null)
            {
                _ingredient1 = _gameManager.ProductsFactory.GetProduct(acceptObj, _blenderPoints.FirstPoint.transform, _blenderPoints.ParentFood,true);
            }
            else if (_ingredient2 == null)
            {
                _ingredient2 = _gameManager.ProductsFactory.GetProduct(acceptObj, _blenderPoints.SecondPoint.transform, _blenderPoints.ParentFood,true);
            }
            else if (_ingredient3 == null)
            {
                _ingredient3 = _gameManager.ProductsFactory.GetProduct(acceptObj, _blenderPoints.ThirdPoint.transform, _blenderPoints.ParentFood,true);
            }
            else
            {
                Debug.LogWarning("В блендере нет места");
            }
            Object.Destroy(acceptObj);
        }
        
        public void CreateResult()
        {
            List<Product> listProducts = new List<Product>() {_ingredient1.GetComponent<Product>(),_ingredient2.GetComponent<Product>(),_ingredient3.GetComponent<Product>(),};
            Product readyObj = _recipeService.GetDish(StationType.Blender,listProducts);
            _result = _gameManager.ProductsFactory.GetProduct(readyObj.gameObject, _blenderPoints.SecondPoint.transform, _blenderPoints.ParentReadyFood,true);
        }
    
        public void TurnOn()
        {
            _ingredient1.SetActive(false);
            _ingredient2.SetActive(false);
            _ingredient3.SetActive(false);
            _isWork = true;
            _blenderView.TurnOn();
        }
    
        public void TurnOff()
        {
            _isWork = false;
            Object.Destroy(_ingredient1);
            Object.Destroy(_ingredient2);
            Object.Destroy(_ingredient3);
            _ingredient1 = null;
            _ingredient2 = null;
            _ingredient3 = null;
            _blenderView.TurnOff();
        }
        
        private void CookingProcess()
        {
            if (_isInit == false)
            {
                Debug.Log("Инициализация не закончена");
                return;
            }
            
            if(_isHeroikTrigger == false)
            {
                return;
            }
            
            if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
            {
                Debug.LogWarning("Блендер не работает");
                return;
            }
            
            if(_heroik.IsBusyHands == false) // руки не заняты
            {
                if (_isWork)
                {
                    Debug.Log("ждите блендер готовится");
                }
                else
                {
                    if (_result == null)
                    {
                        if (_ingredient1 == null)
                        {
                            Debug.Log("Руки пусты ингредиентов нет");
                        }
                        else
                        {
                            if (_ingredient2 == null)
                            {
                                _heroik.TryPickUp(GiveObj(ref _ingredient1));
                                _ingredient1 = null;
                            }
                            else
                            {
                                _heroik.TryPickUp(GiveObj(ref _ingredient2));
                                _ingredient2 = null;
                            }
                        }
                    }
                    else
                    {
                        _heroik.TryPickUp(GiveObj(ref _result));
                    }
                            
                }
                        
            }
            else // руки заняты
            {
                if (_isWork)
                {
                    Debug.Log("ждите блендер готовится");
                }
                else
                {
                    if (_result == null)
                    {
                        if (_ingredient1 == null)
                        {
                            if(_heroik.CanGiveIngredient(ListProduct))
                            {
                                AcceptObject(_heroik.TryGiveIngredient());
                                Debug.Log("Предмет первый положен в блендер");
                            }
                            else
                            {
                                Debug.Log("с предметом нельзя взаимодействовать");
                            }
                        }
                        else
                        {
                            if (_ingredient2 == null)
                            {
                                if(_heroik.CanGiveIngredient(ListProduct))
                                {
                                    AcceptObject(_heroik.TryGiveIngredient());
                                    Debug.Log("Предмет второй положен в блендер");
                                }
                                else
                                {
                                    Debug.Log("с предметом нельзя взаимодействовать");
                                }
                            }
                            else
                            {
                                if(_heroik.CanGiveIngredient(ListProduct))
                                {
                                    AcceptObject(_heroik.TryGiveIngredient());
                                    TurnOn(); 
                                    //GameObject objdish = FindReadyFood();
                                    //StartCookingProcessAsync(objdish);
                                    StartCoroutine(ContinueWorkCoroutine());
                                }
                                else
                                {
                                    Debug.Log("с предметом нельзя взаимодействовать");
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Руки полные уберите предмет");
                    }
                }
            }
        }
        
        private IEnumerator ContinueWorkCoroutine()
        {
            StartCoroutine(_blenderView.Timer.StartTimer());
            yield return new WaitWhile(() => _blenderView.Timer.IsWork);
            CreateResult();
            TurnOff();
        }
        
    }

}
