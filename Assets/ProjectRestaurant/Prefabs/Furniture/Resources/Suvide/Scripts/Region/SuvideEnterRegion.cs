using System;
using UnityEngine;

public class SuvideEnterRegion : MonoBehaviour
{
    private float _timer = 0f;
    private float _updateInterval = 0.1f;
    private bool _isCreateSuvide = false;
    private Outline _outline;
    private Suvide _suvide;
    private DecorationFurniture _decorationFurniture;
    
    // Initialize SuvideView
    private Animator _animator;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private GameObject switchTimePrefab;
    [SerializeField] private GameObject switchTemperPrefab;
    [SerializeField] private HelperTimer firstTimer;
    [SerializeField] private HelperTimer secondTimer;
    [SerializeField] private HelperTimer thirdTimer;
    
    // Initialize Suvide
    
    [SerializeField] private ProductsContainer productsContainer;
    private SuvidePoints _suvidePoints;
    private SuvideView _suvideView;
    private Heroik _heroik;
    
    // Initialize SuvidePoints
    [SerializeField] private Transform firstPointIngredient;
    [SerializeField] private Transform secondPointIngredient;
    [SerializeField] private Transform thirdPointIngredient;
    [SerializeField] private Transform firstPointResult;
    [SerializeField] private Transform secondPointResult;
    [SerializeField] private Transform thirdPointResult;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        //_animator.SetBool("Work", false);
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    
        if (_timer >= _updateInterval)
        {
            if (_suvide == null)
                return;
            
            _timer = 0f;
            _suvide.Update();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            if (_isCreateSuvide == false)
            {
                _suvidePoints = new SuvidePoints(firstPointIngredient, secondPointIngredient, thirdPointIngredient,
                    firstPointResult, secondPointResult, thirdPointResult);
                _suvideView = new SuvideView(waterPrefab, switchTimePrefab, switchTemperPrefab, firstTimer, secondTimer,
                    thirdTimer, _animator);
                _suvide = new Suvide(_suvideView, _suvidePoints, _heroik, productsContainer);
                _isCreateSuvide = true;
            }
            else
            {
                Debug.Log("Новый скрипт создан не был");
            }
            _suvide.HeroikIsTrigger();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _suvide.HeroikIsTrigger();
            _heroik = null;
            _outline.OutlineWidth = 0f;
            if (_suvide.IsAllowDestroy())
            {
                _suvide.Dispose();
                _suvide = null;
            
                _suvidePoints.Dispose();
                _suvidePoints = null;
            
                _suvideView.Dispose();
                _suvideView = null;
                
                _isCreateSuvide = false;
            }
        }
    }


}
