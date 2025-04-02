using UnityEngine;

public class BlenderEnterRegion : MonoBehaviour
{
    private Blender _script;
    private Outline _outline;
    
    // для Blender
    private Animator _animator;
    private Heroik _heroik = null;
    private BlenderPoints _blenderPoints;
    
    // для Blender
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    [SerializeField] private ProductsContainer productsContainer;
    
    // для BlenderPoints
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;
    [SerializeField] private Transform thirdPoint;
    [SerializeField] private Transform parentFood;
    [SerializeField] private Transform parentReadyFood;

    private bool _initBlenderPoints = false;

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
            
            if (!GetComponent<Blender>())
            {
                if (_initBlenderPoints == false)
                {
                    _blenderPoints = new BlenderPoints(firstPoint, secondPoint, thirdPoint, parentFood, parentReadyFood);
                    _initBlenderPoints = true;
                }
                
                _script = gameObject.AddComponent<Blender>();
                _script.HeroikIsTrigger();
                _script.Initialize(timer, _heroik,  timerPoint, timerParent, _animator, _blenderPoints,productsContainer);
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
                Debug.Log("скрипты был удален");
            }
        }
    }
}
