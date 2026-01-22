using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Distribution : MonoBehaviour, IUseFurniture, IPause
{ 
    private const string AnimAcceptDish = "AcceptDish";
    
    [SerializeField] private Transform pointDish;
    [SerializeField] private Transform pointNotif;
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
    private INotificationGetter _notificationManager;

    private List<Product> ListProduct => _foodsForFurnitureContainer.Distribution.ListForFurniture;

    
    [Inject]
    private void ConstructZenject( 
        ProductsFactory productsFactory,
        ICheckTheCheck checkCheck,
        IDeleteCheck checkDelete,
        FoodsForFurnitureContainer foodsForFurnitureContainer,
        IHandlerPause handlerPause,
        INotificationGetter notificationManager)
    {
        _productsFactory = productsFactory;
        _checkCheck = checkCheck;
        _checkDelete = checkDelete;
        _foodsForFurnitureContainer = foodsForFurnitureContainer;
        _pauseHandler = handlerPause;
        _pauseHandler.Add(this);
        _notificationManager = notificationManager;
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
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
    
    private void OnDisable()
    {
        _pauseHandler.Remove(this);
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
            InvokeNotification().Forget();
            return;
        }
        
        if(_heroik.IsBusyHands == false) // руки не заняты
        {
            InvokeNotification().Forget();
            Debug.Log("У вас пустые руки");
            return;
        }
        
        _currentCheck = _checkCheck.CheckTheCheck(_heroik.CurrentTakenObjects);// руки заняты
        if (_currentCheck!= null)
        {
            if (AcceptObject(_heroik.TryGiveIngredient(ListProduct)))
            {
                InvokeNotification(true).Forget();
                sounds.PlayOneShotClip(AudioNameGamePlay.PutOnTheTableSound2);
                TurnOn();
            }
            else
            {
                InvokeNotification().Forget();
                Debug.Log("с предметом что-то пошло не так");
            }
            return;
        }
        
        InvokeNotification().Forget();
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
            InvokeNotification().Forget();
            Debug.LogWarning("Раздача не работает");
            return false;
        }
        
        if (CheckUseFurniture() == false)
        {
            return false;
        }

        return true;
    }

    public void EndCook() // для вызова аниматором
    {
        Debug.Log("EndCook");
        TakeToTheHall();
        TurnOff();
        sounds.PlayOneShotClip(AudioNameGamePlay.DistributionSound);
    }
    
    private async UniTask InvokeNotification(bool isReady = false)
    {
        await _notificationManager.GetNotification(pointNotif, isReady);
        _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.NotWorkTableSound);
    }
}
