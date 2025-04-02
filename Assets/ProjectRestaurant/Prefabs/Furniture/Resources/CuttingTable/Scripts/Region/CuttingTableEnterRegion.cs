using UnityEngine;

public class CuttingTableEnterRegion : MonoBehaviour
{
    private Animator _animator;
    private Outline _outline;
    private Heroik _heroik;
    
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    [SerializeField] private Transform positionIngredient1; // сделать отдельный класс
    [SerializeField] private Transform positionIngredient2; // сделать отдельный класс
    [SerializeField] private Transform parentIngredient;    // сделать отдельный класс
    [SerializeField] private Transform positionResult;      // сделать отдельный класс
    [SerializeField] private Transform parentResult;
    [SerializeField] private ProductsContainer productsContainer;
    
    private CuttingTable script;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Work", false);
        _outline = GetComponent<Outline>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            if (!GetComponent<CuttingTable>())
            {
                script = gameObject.AddComponent<CuttingTable>();
                script.HeroikIsTrigger();
                script.Initialize(_animator,_heroik, timer, timerPoint, timerParent,positionIngredient1,positionIngredient2,parentIngredient,positionResult,parentResult,productsContainer);
            }
            else
            {
                script.HeroikIsTrigger();
                Debug.Log("Новый скрипт создан не был");
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            script.HeroikIsTrigger();
            _heroik = null;
            _outline.OutlineWidth = 0f;
            if (script.IsAllowDestroy())
            {
                Destroy(script);
                Debug.Log("скрипт был удален");
            }
        }
    }
}
