using UnityEngine;

public class OvenEnterRegion : MonoBehaviour
{
    [SerializeField] private GameObject glassOn;
    [SerializeField] private GameObject glassOff;
    [SerializeField] private GameObject switchFirst;
    [SerializeField] private GameObject switchSecond;
    [SerializeField] private GameObject[] foodOnTheOver;
    [SerializeField] private GameObject[] cookedFoodOnTheOver;
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    
    private Heroik _heroik = null;
    private Outline _outline;
    private Oven script;

    void Start()
    {
        _outline = GetComponent<Outline>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            if (!GetComponent<Oven>())
            {
                script = gameObject.AddComponent<Oven>();
                script.HeroikIsTrigger();
                script.Initialize( glassOn,  glassOff, switchFirst, switchSecond, foodOnTheOver, cookedFoodOnTheOver, timer, timerPoint, timerParent,_heroik);
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
