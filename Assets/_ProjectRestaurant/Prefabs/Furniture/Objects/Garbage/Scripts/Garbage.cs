using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Garbage : MonoBehaviour,IUseFurniture
{
    [SerializeField] private SoundsFurniture sounds;
    [SerializeField] private Transform pointNotif;
    private Heroik _heroik;
    private bool _isHeroikTrigger;
    private GameObject _obj;
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    
    private FoodsForFurnitureContainer _foodsForFurnitureContainer;
    private INotificationGetter _notificationManager;
    
    private List<Product> ListProduct => _foodsForFurnitureContainer.Garbage.ListForFurniture;

    
    [Inject]
    private void ConstructZenject(FoodsForFurnitureContainer foodsForFurnitureContainer,
        INotificationGetter notificationManager)
    {
        _foodsForFurnitureContainer = foodsForFurnitureContainer;
        _notificationManager = notificationManager;
    }

    private void Awake()
    {
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
        _obj = acceptObj;
        //_heroik.CleanObjOnHands();
        return true;
    }
    
    private void CookingProcess()
    {
        if (CheckCookingProcess() == false)
        {
            return;
        }

        if (_heroik.IsBusyHands == true)
        {
            if (AcceptObject(_heroik.TryGiveIngredient(ListProduct)))
            {
                sounds.PlayOneShotClip(AudioNameGamePlay.RubbishSound);
                DeleteObj();
                return;
            }
            
            InvokeNotification().Forget();
            Debug.Log("с предметом что-то пошло не так");
            return;
        }
        
        InvokeNotification().Forget();
        Debug.Log("Вам нечего выкидывать");

    }
    
    private void DeleteObj()
    {
        _obj.SetActive(false);
        Destroy(_obj);
        _obj = null;
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
            Debug.LogWarning("Мусорка не работает");
            return false;
        }
        
        if (CheckUseFurniture() == false)
        {
            return false;
        }

        return true;
    }
    
    private async UniTask InvokeNotification(bool isReady = false)
    {
        await _notificationManager.GetNotification(pointNotif, isReady);
        _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.NotWorkTableSound);
    }
    
}
