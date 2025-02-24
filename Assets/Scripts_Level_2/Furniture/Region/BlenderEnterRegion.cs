using UnityEngine;

public class BlenderEnterRegion : MonoBehaviour
{

    private Animator _animator;
    private Outline _outline;
    private Heroik _heroik = null;
    private Blender _script;
    private BlenderPoints _blenderPoints;
    
    [SerializeField] private Transform _parentFood;
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;
    [SerializeField] private Transform thirdPoint;
    
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    [SerializeField] private GameObject[] objectOnTheTable;
    [SerializeField] private GameObject[] readyFoods;
    void Start()
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
            if (!GetComponent<BlenderPoints>())
            {
                _blenderPoints = gameObject.AddComponent<BlenderPoints>();
                _blenderPoints.Initialize(firstPoint,secondPoint,thirdPoint);
            }
            if (!GetComponent<Blender>())
            {
                _script = gameObject.AddComponent<Blender>();
                _script.HeroikIsTrigger();
                _script.Initialize(timer, _heroik,  timerPoint, timerParent, objectOnTheTable, readyFoods,
                    _animator, _blenderPoints, _parentFood);
            }
            else
            {
                _script.HeroikIsTrigger();
                Debug.Log("Новый скрипт создан не был");
            }
            
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
                Destroy(_blenderPoints);
                Debug.Log("скрипты был удален");
            }
        }
    }
}
