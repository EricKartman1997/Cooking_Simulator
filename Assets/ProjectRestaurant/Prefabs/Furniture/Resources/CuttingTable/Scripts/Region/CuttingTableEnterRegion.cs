using UnityEngine;

public class CuttingTableEnterRegion : MonoBehaviour
{
    // CuttingTableView
    private Animator _animator;
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;

    // CuttingTablePoints
    [SerializeField] private Transform positionIngredient1; 
    [SerializeField] private Transform positionIngredient2; 
    [SerializeField] private Transform parentIngredient;    
    [SerializeField] private Transform positionResult;      
    [SerializeField] private Transform parentResult;
    
    // CuttingTable
    private CuttingTablePoints _cuttingTablePoints;
    private CuttingTableView _cuttingTableView;
    [SerializeField] private ProductsContainer productsContainer;
    private Heroik _heroik;
    
    private DecorationFurniture _decorationFurniture;
    private Outline _outline;
    private CuttingTable _cuttingTable;
    private bool _isCreateCuttingTable = false;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Work", false);
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
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
            
            if (_isCreateCuttingTable == false)
            {
                _cuttingTablePoints = new CuttingTablePoints(positionIngredient1,positionIngredient2,parentIngredient,positionResult,parentResult);
                _cuttingTableView = new CuttingTableView(_animator,timer,timerPoint,timerParent);
                _cuttingTable = new CuttingTable(_cuttingTablePoints,_cuttingTableView,productsContainer,_heroik);
                _isCreateCuttingTable = true;
            }
            else
            {
                Debug.Log("Новый скрипт создан не был");
            }
            _cuttingTable.HeroikIsTrigger();
            
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
            _cuttingTable.HeroikIsTrigger();
            _heroik = null;
            _outline.OutlineWidth = 0f;
            if (_cuttingTable.IsAllowDestroy())
            {
                _cuttingTable.Dispose();
                _cuttingTable = null;
            
                _cuttingTablePoints.Dispose();
                _cuttingTablePoints = null;
            
                _cuttingTableView.Dispose();
                _cuttingTableView = null;
                
                _isCreateCuttingTable = false;
            }
        }
    }
}
