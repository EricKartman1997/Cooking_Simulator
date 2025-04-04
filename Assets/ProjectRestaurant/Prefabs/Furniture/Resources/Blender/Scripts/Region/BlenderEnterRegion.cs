using UnityEngine;

public class BlenderEnterRegion : MonoBehaviour
{
    private Blender _blender;
    private Outline _outline;
    
    // для Blender
    private Heroik _heroik = null;
    private BlenderPoints _blenderPoints;
    private BlenderView _blenderView;
    [SerializeField] private ProductsContainer productsContainer;
    
    // для BlenderView
    private Animator _animator;
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    
    // для BlenderPoints
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;
    [SerializeField] private Transform thirdPoint;
    [SerializeField] private Transform parentFood;
    [SerializeField] private Transform parentReadyFood;

    private bool _isCreateBlender = false;

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
            
            if (_isCreateBlender == false)
            {
                _blenderPoints = new BlenderPoints(firstPoint, secondPoint, thirdPoint, parentFood, parentReadyFood);
                _blenderView = new BlenderView(timer, timerPoint, timerParent, _animator);
                _blender = new (_heroik, _blenderPoints, _blenderView, productsContainer);
                _isCreateBlender = true;
            }
            else
            {
                Debug.Log("Новый скрипт создан не был");
            }
            _blender.HeroikIsTrigger();
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _blender.HeroikIsTrigger();
            _heroik = null;
            _outline.OutlineWidth = 0f;
            if (_blender.IsAllowDestroy())
            {
                _blender.Dispose();
                _blender = null;
            
                _blenderPoints.Dispose();
                _blenderPoints = null;
            
                _blenderView.Dispose();
                _blenderView = null;
                
                _isCreateBlender = false;
            }
        }
    }
}
