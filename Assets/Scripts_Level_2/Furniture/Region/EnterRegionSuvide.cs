using UnityEngine;

public class EnterRegionSuvide : MonoBehaviour
{
    private Animator _animator;
    private Outline _outline;
    private Suvide script;
    private Heroik _heroik;
    
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private GameObject switchTimePrefab;
    [SerializeField] private GameObject switchTemperPrefab;
    
    [SerializeField] private Timer_Prefab firstTimer;
    [SerializeField] private Timer_Prefab secondTimer;
    [SerializeField] private Timer_Prefab thirdTimer;
    
    [SerializeField] private GameObject[] food;
    [SerializeField] private GameObject[] readyFood;
    
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
            if (!GetComponent<Suvide>())
            {
                script = gameObject.AddComponent<Suvide>();
                script.HeroikIsTrigger();
                script.Initialize( _animator, _heroik,  readyFood, food, firstTimer, secondTimer,
                    thirdTimer,  switchTemperPrefab, switchTimePrefab, waterPrefab);
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
