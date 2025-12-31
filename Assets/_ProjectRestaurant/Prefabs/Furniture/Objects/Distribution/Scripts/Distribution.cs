using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Distribution : MonoBehaviour, IUseFurniture, IPause
{ 
    private const string AnimAcceptDish = "AcceptDish";
    
    [SerializeField] private Transform pointDish;
    [SerializeField] private SoundsFurniture sounds;
    [SerializeField] private Animator _animator;
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    
    private GameObject _currentDish;
    private Check _currentCheck;
    private Heroik _heroik;
    private bool _isWork;
    private bool _isHeroikTrigger;
    
    private ProductsFactory _productsFactory;
    private FoodsForFurnitureContainer _foodsForFurnitureContainer;
    private ICheckTheCheck _checkCheck;
    private IDeleteCheck _checkDelete;
    private IHandlerPause _pauseHandler;

    private List<Product> ListProduct => _foodsForFurnitureContainer.Distribution.ListForFurniture;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
    }

    private void Start()
    {
        //Debug.Log("Distribution Init");
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
    private void OnDisable()
    {
        _pauseHandler.Remove(this);
    }

    [Inject]
    private void ConstructZenject( 
        ProductsFactory productsFactory,
        ICheckTheCheck checkCheck,
        IDeleteCheck checkDelete,
        FoodsForFurnitureContainer foodsForFurnitureContainer,
        IHandlerPause handlerPause)
    {
        _productsFactory = productsFactory;
        _checkCheck = checkCheck;
        _checkDelete = checkDelete;
        _foodsForFurnitureContainer = foodsForFurnitureContainer;
        _pauseHandler = handlerPause;
        _pauseHandler.Add(this);
    }
    
    public void UpdateCondition()
    {
        if (CheckUseFurniture() == false)
        {
            _outline.OutlineWidth = 0f;
        }
    }
    
    public void SetPause(bool isPaused)
    {
        // if (_animator == null)
        // {
        //     Debug.Log("Ошибка в SetPause");
        //     return;
        // }
        if (isPaused == true)
        {
            _animator.speed = 0f;
            return;
        }
        _animator.speed = 1;
            
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
        _currentDish = _productsFactory.GetProduct(acceptObj, pointDish, pointDish,true);
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
        _currentCheck.IsStop = true;
    }
    
    private void CookingProcess()
    {
        if (CheckCookingProcess() == false)
        {
            return;
        }
        
        if (_isWork)
        {
            Debug.Log("Ждите блюдо еще не забрали");
            _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.NotWorkTableSound);
            return;
        }
        
        if(_heroik.IsBusyHands == false) // руки не заняты
        {
            _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.ForbiddenSound);
            Debug.Log("У вас пустые руки");
            return;
        }
        
        _currentCheck = _checkCheck.CheckTheCheck(_heroik.CurrentTakenObjects);// руки заняты
        if (_currentCheck!= null)
        {
            if (AcceptObject(_heroik.TryGiveIngredient(ListProduct)))
            {
                sounds.PlayOneShotClip(AudioNameGamePlay.PutOnTheTableSound2);
                TurnOn();
                //StartCoroutine(ContinueWorkCoroutine());
            }
            else
            {
                Debug.Log("с предметом что-то пошло не так");
            }
            return;
        }
        
        _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.NotWorkTableSound);
        Debug.Log("Этого блюдо нет в чеках");
    }
    
    private void TakeToTheHall()
    {
        _currentDish.SetActive(false);
        _checkDelete.DeleteCheck(_currentCheck);
        Destroy(_currentDish);
        _currentDish = null;
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
            Debug.LogWarning("Раздача не работает");
            return false;
        }
        
        if (CheckUseFurniture() == false)
        {
            return false;
        }

        return true;
    }
    
    // private IEnumerator ContinueWorkCoroutine()
    // {
    //     while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimAcceptDish))
    //     {
    //         yield return null;
    //     }
    //     
    //     while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
    //     {
    //         yield return null;
    //     }
    //     TakeToTheHall();
    //     TurnOff();
    // }

    public void EndCook() // для вызова аниматором
    {
        Debug.Log("EndCook");
        TakeToTheHall();
        TurnOff();
        sounds.PlayOneShotClip(AudioNameGamePlay.DistributionSound);
    }
    
    
}
