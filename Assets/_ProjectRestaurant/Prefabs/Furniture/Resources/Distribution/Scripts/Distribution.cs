using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distribution : MonoBehaviour, IUseFurniture
{ 
    private const string AnimAcceptDish = "AcceptDish";
    
    [SerializeField] private Transform pointDish;

    private Checks _checks;
    private Check _currentCheck;
    private Animator _animator;
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    private Heroik _heroik;
    private bool _isWork;
    private bool _isHeroikTrigger;
    private bool _isInit;
    private GameObject _currentDish;
    private GameManager _gameManager;
    private FoodsForFurnitureContainer _foodsForFurnitureContainer;
    
    private bool IsAllInit => _gameManager.BootstrapLvl2.IsAllInit;
    private List<Product> ListProduct => _foodsForFurnitureContainer.Distribution.ListForFurniture;

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
            
        while (_checks == null)
        {
            _checks = _gameManager.Checks;
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
        
        _isInit = true;
        Debug.Log("Distribution Init");
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
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
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

    private bool AcceptObject(GameObject acceptObj)
    {
        if (acceptObj == null)
        {
            Debug.Log("Объект не передался");
            return false;
        }
        _currentDish = _gameManager.ProductsFactory.GetProduct(acceptObj, pointDish, pointDish,true);
        _heroik.CleanObjOnHands();
        return true;
    }

    private void TurnOff()
    {
        _isWork = false;
    }

    private void TurnOn()
    {
        _animator.SetTrigger(AnimAcceptDish);
        _isWork = true;
        _currentCheck.StopUpdateTime();
    }
    
    private void CookingProcess()
    {
        if (CheckCookingProcess() == false)
        {
            return;
        }
        
        if(_heroik.IsBusyHands == false) // руки не заняты
        {
            Debug.Log("У вас пустые руки");
        }
        else// руки заняты
        {
            if (_isWork)
            {
                Debug.Log("Ждите блюдо еще не забрали");
            }
            else
            {
                _currentCheck = _checks.CheckTheCheck(_heroik.CurrentTakenObjects);
                if (_currentCheck!= null)
                {
                    if (AcceptObject(_heroik.TryGiveIngredient(ListProduct)))
                    {
                        TurnOn();
                        StartCoroutine(ContinueWorkCoroutine());
                    }
                    else
                    {
                        Debug.Log("с предметом что-то пошло не так");
                    }
                }
                else
                {
                    Debug.Log("Этого блюдо нет в чеках");
                }
            }
        }
    }
    
    private void TakeToTheHall()
    {
        _currentDish.SetActive(false);
        _checks.DeleteCheck(_currentCheck);
        Destroy(_currentDish);
        _currentDish = null;
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
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            Debug.LogWarning("Раздача не работает");
            return false;
        }
        
        if (CheckUseFurniture() == false)
        {
            return false;
        }

        return true;
    }
    
    private IEnumerator ContinueWorkCoroutine()
    {
        while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimAcceptDish))
        {
            yield return null;
        }
        
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }
        TakeToTheHall();
        TurnOff();
    }
    
}
