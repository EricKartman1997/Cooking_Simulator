using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuvideFurniture
{
    public class SuvideFurniture : MonoBehaviour
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
        private GameManager _gameManager;
        private ProductsContainer _productsContainer;
        
        private bool IsAllInit => _gameManager.BootstrapLvl2.IsAllInit;
        
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
                _outline.OutlineWidth = 0f;
                _isHeroikTrigger = false;
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
        
        private GameObject GiveObj(ref GameObject giveObj)
        {
            GameObject copy = giveObj;
            Destroy(giveObj);
            return copy;
        }
    
        private void AcceptObject(GameObject acceptObj,string TOKEN)
        {
            if (TOKEN == DISH1)
            {
                _dish1 = _gameManager.ProductsFactory.GetProduct(acceptObj, _suvidePoints.PointIngredient1, _suvidePoints.PointIngredient1, true,true);
                Destroy(acceptObj);
                _cookingdish1 = true;
                return;
            }
            
            if (TOKEN == DISH2)
            {
                _dish2 = _gameManager.ProductsFactory.GetProduct(acceptObj, _suvidePoints.PointIngredient2, _suvidePoints.PointIngredient2, true,true);
                Destroy(acceptObj);
                _cookingdish2 = true;
                return;
            }
            
            if (TOKEN == DISH3)
            {
                _dish3 = _gameManager.ProductsFactory.GetProduct(acceptObj, _suvidePoints.PointIngredient3, _suvidePoints.PointIngredient3, true,true);
                Destroy(acceptObj);
                _cookingdish3 = true;
                return;
            }
            
            Debug.Log("ошибка в AcceptObject");
        }
    
        private void CreateResult(GameObject obj,string TOKEN)
        {
            if (TOKEN == DISH1)
            {
                _productsContainer.RecipesForSuvide.TryGetValue(obj.name, out ObjsForDistribution readyObj);
                if (readyObj != null)
                {
                    Destroy(_dish1);
                    _dish1 = _gameManager.ProductsFactory.GetProduct(readyObj.gameObject, _suvidePoints.PointResult1, _suvidePoints.PointResult1, true,true);
                    return;
                }
                Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                return;
            }

            if (TOKEN == DISH2)
            {
                _productsContainer.RecipesForSuvide.TryGetValue(obj.name, out ObjsForDistribution readyObj);
                if (readyObj != null)
                {
                    Destroy(_dish2);
                    _dish2 = _gameManager.ProductsFactory.GetProduct(readyObj.gameObject, _suvidePoints.PointResult2, _suvidePoints.PointResult2, true,true);
                    return;
                }
                Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                return;
            }

            if (TOKEN == DISH3)
            {
                _productsContainer.RecipesForSuvide.TryGetValue(obj.name, out ObjsForDistribution readyObj);
                if (readyObj != null)
                {
                    Destroy(_dish3);
                    _dish3 = _gameManager.ProductsFactory.GetProduct(readyObj.gameObject, _suvidePoints.PointResult3, _suvidePoints.PointResult3, true,true);
                    return;
                }
                Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                return;
            }
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
                Debug.LogWarning("Сувид не работает");
                return;
            }
    
            if (_heroik.IsBusyHands == false)
            {
                if (_dish1 != null && _cookingdish1 == false)
                {
                    _heroik.TryPickUp(GiveObj(ref _dish1));
                    ChangeView();
                    return;
                }
                
                if (_dish2 != null && _cookingdish2 == false)
                {
                    _heroik.TryPickUp(GiveObj(ref _dish2));
                    ChangeView();
                    return;
                }
                
                if (_dish3 != null && _cookingdish3 == false)
                {
                    _heroik.TryPickUp(GiveObj(ref _dish3));
                    ChangeView();
                    return;
                }
                
                Debug.Log("Сувид пуст руки тоже");
                ChangeView();
                return;
            }
    
            if (_heroik.IsBusyHands == true)
            {
                if (!_heroik.CanGiveIngredient(new List<Type>() { typeof(ObjsForSuvide) }))
                {
                    Debug.Log("продукт не подходит для сувида");
                    return;
                }
    
                if (_dish1 == null)
                {
                    AcceptObject(_heroik.TryGiveIngredient(), DISH1);
                    TurnOn(DISH1);
                    StartCoroutine(ContinueWorkCoroutine(_dish1,DISH1));
                    ChangeView();
                    return;
                }
                
                if (_dish2 == null)
                {
                    AcceptObject(_heroik.TryGiveIngredient(), DISH2);
                    TurnOn(DISH2);
                    StartCoroutine(ContinueWorkCoroutine(_dish2,DISH2));
                    ChangeView();
                    return;
                }
                
                if (_dish3 == null)
                {
                    AcceptObject(_heroik.TryGiveIngredient(), DISH3);
                    TurnOn(DISH3);
                    StartCoroutine(ContinueWorkCoroutine(_dish3,DISH3));
                    ChangeView();
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
    }

}
