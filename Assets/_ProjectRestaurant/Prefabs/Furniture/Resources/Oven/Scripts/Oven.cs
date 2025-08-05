using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OvenFurniture
{
    public class Oven : MonoBehaviour, IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn
    {
        [SerializeField] private GameObject switchFirst;
        [SerializeField] private GameObject switchSecond;
        [SerializeField] private TimerView timerPref;
        [SerializeField] private float timeTimer;
        [SerializeField] private Transform pointUp;
        [SerializeField] private Transform positionIngredient;
        
        
        private const string ANIMATIONCLOSE = "Close";
        private const string ANIMATIONOPEN = "Open";
        
        private GameObject _ingredient;
        private GameObject _result;
        private bool _isWork;
        private bool _isHeroikTrigger;
        private bool _isInit;
        private Heroik _heroik; // только для объекта героя, а надо и другие...
        private OvenView _ovenView;
        private OvenPoints _ovenPoints;
        private Animator _animator;
        private DecorationFurniture _decorationFurniture;
        private Outline _outline;
        private GameManager _gameManager;
        private ProductsContainer _productsContainer;
        private FoodsForFurnitureContainer _foodsForFurnitureContainer;

        private bool IsAllInit => _gameManager.BootstrapLvl2.IsAllInit;
        
        private List<Product> ListProduct => _foodsForFurnitureContainer.Oven.ListForFurniture;
        
        private void Awake()
        {
            _outline = GetComponent<Outline>();
            _animator = GetComponent<Animator>();
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
            
            while (IsAllInit == false)
            {
                yield return null;
            }
            
            TimerFurniture timerFurniture = new TimerFurniture(timerPref,timeTimer,pointUp);
            _ovenView = new OvenView(switchFirst, switchSecond,timerFurniture, _animator);
            _ovenPoints = new OvenPoints(pointUp,positionIngredient);
            _animator.SetBool(ANIMATIONCLOSE,false);
            _animator.SetBool(ANIMATIONOPEN,true);
            
            _isInit = true;
            Debug.Log("Oven Init");
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
                _outline.OutlineWidth = 0f;
                _isHeroikTrigger = false;
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
    
        public void AcceptObject(GameObject obj)
        {
            _ingredient = _gameManager.ProductsFactory.GetProduct(obj,_ovenPoints.PositionIngredient,_ovenPoints.PositionIngredient, true);
            Object.Destroy(obj);
        }
        
        public void CreateResult(GameObject obj)
        {
            try
            {
                _productsContainer.RecipesForOven.TryGetValue(obj.name, out Product bakedObj);
                if (bakedObj != null)
                {
                    _result = _gameManager.ProductsFactory.GetProduct(bakedObj.gameObject,_ovenPoints.PointUp, _ovenPoints.PointUp,true );
                }
                else
                {
                    Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                }
                
            }
            catch (Exception e)
            {
                Debug.Log("ошибка приготовления в духовке" + e);
            }
        }
        
        public void TurnOn()
        {
            _isWork = true;
            _ovenView.TurnOn();
        }
    
        public void TurnOff()
        {
            _isWork = false;
            _ovenView.TurnOff();
            Destroy(_ingredient);
            _ingredient = null;
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
                Debug.LogWarning("Печка не работает");
                return;
            }
            
            if (_heroik.IsBusyHands == false) // руки не заняты
            {
                if (_isWork)
                {
                    Debug.Log("ждите печка работает");
                }
                else
                {
                    if (_result != null)
                    {
                        _heroik.TryPickUp(GiveObj(ref _result));
                    }
                    else
                    {
                        Debug.Log("печка пуста руки тоже");
                    }
                }
            }
            else // заняты
            {
                if (_isWork)
                {
                    Debug.Log("ждите печка работает");
                }
                else
                {
                    if (_result != null)
                    {
                        Debug.Log("Сначала заберите предмет");
                    }
                    else
                    {
                        if (_heroik.CanGiveIngredient(ListProduct))
                        {
                            AcceptObject(_heroik.TryGiveIngredient());
                            TurnOn();
                            StartCoroutine(ContinueWorkCoroutine(_ingredient));
                        }
                    }
                }
            }
        }
        private IEnumerator ContinueWorkCoroutine(GameObject obj)
        {
            StartCoroutine(_ovenView.Timer.StartTimer());
            yield return new WaitWhile(() => _ovenView.Timer.IsWork);
            TurnOff();
            CreateResult(obj);
        }
        
    }

}
