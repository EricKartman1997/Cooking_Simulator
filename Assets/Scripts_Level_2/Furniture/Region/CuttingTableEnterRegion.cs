using UnityEngine;

public class CuttingTableEnterRegion : MonoBehaviour
{
    private Animator _animator;
    private Outline _outline;
    private Heroik _heroik = null;
    private CuttingTable script;
    
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    
    [SerializeField] private GameObject[] objectOnTheTable;
    [SerializeField] private GameObject[] readyFoods;
    
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
                script.Initialize(_animator,_heroik, readyFoods, objectOnTheTable, timer, timerPoint, timerParent);
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
