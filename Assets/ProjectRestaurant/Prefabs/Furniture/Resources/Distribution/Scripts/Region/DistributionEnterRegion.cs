using UnityEngine;

public class DistributionEnterRegion : MonoBehaviour
{
    private Distribution _script;
    private Outline _outline;
    
    // Initialize Distribution
    [SerializeField] private Checks _checks;
    [SerializeField] private Transform pointDish;
    private Heroik _heroik = null;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _outline = GetComponent<Outline>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            if (!GetComponent<Distribution>())
            {
                _script = gameObject.AddComponent<Distribution>();
                _script.Initialize(_heroik,pointDish,_animator,_checks);
            }
            else
            {
                //Debug.Log("Новый скрипт создан не был");
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
                //Debug.Log("скрипты был удален");
            }
        }
    }
}
