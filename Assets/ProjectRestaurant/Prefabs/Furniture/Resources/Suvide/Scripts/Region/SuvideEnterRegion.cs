using System;
using System.Collections.Generic;
using UnityEngine;

public class SuvideEnterRegion : MonoBehaviour
{
    [SerializeField] private ProductsContainer productsContainer;
    private Animator _animator;
    private Outline _outline;
    private Suvide _script;
    
    // Initialize Suvide
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private GameObject switchTimePrefab;
    [SerializeField] private GameObject switchTemperPrefab;
    [SerializeField] private HelperTimer firstTimer;
    [SerializeField] private HelperTimer secondTimer;
    [SerializeField] private HelperTimer thirdTimer;
    private SuvidePoints _suvidePoints;
    private Heroik _heroik;
    private Dictionary<string, ObjsForDistribution> _recipes;
    
    // Initialize SuvidePoints
    [SerializeField] private Transform firstPointIngredient;
    [SerializeField] private Transform secondPointIngredient;
    [SerializeField] private Transform thirdPointIngredient;
    [SerializeField] private Transform firstPointResult;
    [SerializeField] private Transform secondPointResult;
    [SerializeField] private Transform thirdPointResult;

    private void Awake()
    {
        _recipes = new Dictionary<string, ObjsForDistribution>()
        {
            { productsContainer.Meat.name, productsContainer.BakedMeat.GetComponent<ObjsForDistribution>() },
            { productsContainer.Fish.name, productsContainer.BakedFish.GetComponent<ObjsForDistribution>()}
        };
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        //_animator.SetBool("Work", false);
        _outline = GetComponent<Outline>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            if (!GetComponent<SuvidePoints>())
            {
                _suvidePoints = gameObject.AddComponent<SuvidePoints>();
                _suvidePoints.Initialize(firstPointIngredient, secondPointIngredient, thirdPointIngredient, firstPointResult, secondPointResult, thirdPointResult);
            }
            if (!GetComponent<Suvide>())
            {
                _script = gameObject.AddComponent<Suvide>();
                _script.Initialize( _animator, _heroik, firstTimer, secondTimer, thirdTimer,  switchTemperPrefab, switchTimePrefab, waterPrefab,_suvidePoints,_recipes);
            }
            else
            {
                Debug.Log("Новый скрипт создан не был");
            }
            _script.HeroikIsTrigger();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _script.HeroikIsTrigger();
            _heroik = null;
            _outline.OutlineWidth = 0f;
            if (_script.IsAllowDestroy())
            {
                Destroy(_script);
                Destroy(_suvidePoints);
                Debug.Log("скрипт был удален");
            }
        }
    }


}
