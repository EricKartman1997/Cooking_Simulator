using System;
using UnityEngine;

public class SuvideEnterRegion : MonoBehaviour
{
    private float _timer = 0f;
    private float _updateInterval = 0.1f;
    private bool _isCreateSuvide = false;
    private Outline _outline;
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

    // private void Update()
    // {
    //     _timer += Time.deltaTime;
    //
    //     if (_timer >= _updateInterval)
    //     {
    //         if (_suvide == null)
    //             return;
    //         
    //         _timer = 0f;
    //         _suvide.Update();
    //     }
    // }




}
