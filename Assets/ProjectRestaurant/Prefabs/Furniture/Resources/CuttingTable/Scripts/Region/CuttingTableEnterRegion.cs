using UnityEngine;

public class CuttingTableEnterRegion : MonoBehaviour
{
    // CuttingTableView
    
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
    private Animator _animator;
    private CuttingTable _cuttingTable;
    private bool _isCreateCuttingTable = false;
    

    
}
