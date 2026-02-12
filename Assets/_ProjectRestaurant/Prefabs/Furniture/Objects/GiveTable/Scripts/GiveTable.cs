using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GiveTable : MonoBehaviour,IUseFurniture
{
    [SerializeField] private Transform ingredientPoint;
    [SerializeField] private Transform parentFood;
    [SerializeField] private Transform pointNotif;
    [SerializeField] private SoundsFurniture sounds;
    
    private GameObject _ingredient;
    private bool _isHeroikTrigger;
    private Heroik _heroik;
    
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    
    private List<Product> _productsList;
    private ProductsFactory _productsFactory;
    private INotificationGetter _notificationManager;
    private GiveTableTutorialDecorator _tutorialDecorator;

    [Inject]
    private void ConstructZenject(
        FoodsForFurnitureContainer foodsForFurnitureContainer,
        ProductsFactory productsFactory,
        INotificationGetter notificationManager)
    {
        _productsFactory = productsFactory;
        _productsList = foodsForFurnitureContainer.GiveTable.ListForFurniture;
        _notificationManager = notificationManager;
    }

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
        _tutorialDecorator = GetComponent<GiveTableTutorialDecorator>();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
        {
            _outline.OutlineWidth = 2f;
            _isHeroikTrigger = true;
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
            _outline.OutlineWidth = 0f;
            _isHeroikTrigger = false;
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik.ToInteractAction.Unsubscribe(CookingProcess);
            ExitTrigger();
        }
    }
    
    private bool AcceptObject(GameObject acceptObj)
    {
        if (acceptObj == null)
            return false;

        _ingredient = _productsFactory.GetProduct(
            acceptObj,
            ingredientPoint,
            parentFood,
            true);

        _ingredient.transform.localRotation = Quaternion.identity;
        _ingredient.transform.localScale = Vector3.one;
        _ingredient.transform.localPosition = Vector3.zero;

        // ---- ДОБАВЛЕНО ----
        if (_tutorialDecorator != null)
        {
            var product = _ingredient.GetComponent<Product>();
            _tutorialDecorator.SetCurrentIngredient(product.Name);
        }
        // -------------------

        _heroik.CleanObjOnHands();
        return true;
    }

    
    private GameObject GiveObj(GameObject giveObj)
    {
        return giveObj;
    }
    
    public void UpdateCondition()
    {
        if (CheckUseFurniture() == false)
        {
            //_heroik = null;
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
    
    private void CookingProcess()
    {
        if (CheckCookingProcess() == false)
            return;

        // -----------------------------
        // РУКИ ПУСТЫ
        // -----------------------------
        if (_heroik.IsBusyHands == false)
        {
            if (_ingredient == null)
            {
                InvokeNotification().Forget();
                return;
            }

            if (_heroik.TryPickUp(GiveObj(_ingredient)))
            {
                sounds.PlayOneShotClip(AudioNameGamePlay.TakeOnTheTableSound);
                CleanObjOnTable(_ingredient);
                return;
            }

            InvokeNotification().Forget();
            return;
        }

        // -----------------------------
        // РУКИ ЗАНЯТЫ
        // -----------------------------
        if (_ingredient == null)
        {
            // ------ Tutorial ограничение ------
            if (_tutorialDecorator != null)
            {
                IngredientName ingredientName = _heroik.CurrentTakenObjects.GetComponent<Product>().Name;

                if (_tutorialDecorator.CanAccept(ingredientName) == false)
                {
                    InvokeNotification().Forget();
                    return;
                }

                _tutorialDecorator.PutSalatAction?.Invoke();
            }
            // ----------------------------------

            if (!AcceptObject(_heroik.TryGiveIngredient(_productsList)))
            {
                InvokeNotification().Forget();
                return;
            }

            sounds.PlayOneShotClip(AudioNameGamePlay.PutOnTheTableSound2);
            return;
        }

        InvokeNotification().Forget();
    }


    private void CleanObjOnTable(GameObject ingredient)
    {
        if (_tutorialDecorator != null)
            _tutorialDecorator.ClearCurrentIngredient();

        Destroy(ingredient);
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
            Debug.LogWarning("Стол не работает");
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
