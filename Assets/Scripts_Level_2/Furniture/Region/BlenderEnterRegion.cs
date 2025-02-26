using System.Collections.Generic;
using UnityEngine;

public class BlenderEnterRegion : MonoBehaviour
{
    private Blender _script;
    private Outline _outline;
    
    // для Blender
    private Animator _animator;
    private Heroik _heroik = null;
    private BlenderPoints _blenderPoints;
    private BlenderRecipes _blenderRecipes;
    private GameObject wildBerryCocktailCopy;
    private GameObject freshnessCocktailCopy;
    private GameObject rubbishCopy;
    
    // для Blender
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    [SerializeField] private GameObject[] objectOnTheTable;
    [SerializeField] private GameObject[] readyFoods;
    [SerializeField] private Transform parentFood;
    [SerializeField] private Transform readyFood;
    
    // для BlenderPoints
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;
    [SerializeField] private Transform thirdPoint;
    
    // для BlenderRecipes
    [SerializeField] private GameObject wildBerryCocktail;
    [SerializeField] private GameObject freshnessCocktail;
    [SerializeField] private GameObject rubbish;
    [SerializeField] private List<GameObject> requiredFreshnessCocktail;
    [SerializeField] private List<GameObject> requiredWildBerryCocktail;
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
            if (!GetComponent<BlenderRecipes>())
            {
                // узнаем есть ли этот объект, если нет то создаем
                if (wildBerryCocktailCopy == null)
                {
                    wildBerryCocktailCopy = Instantiate(wildBerryCocktail,_blenderPoints.GetSecondPoint(), Quaternion.identity, readyFood);
                    wildBerryCocktailCopy.SetActive(false);
                    wildBerryCocktailCopy.name = wildBerryCocktailCopy.name.Replace("(Clone)", "");
                }
                // узнаем есть ли этот объект, если нет то создаем
                if(freshnessCocktailCopy == null)
                {
                    freshnessCocktailCopy = Instantiate(freshnessCocktail,_blenderPoints.GetSecondPoint(), Quaternion.identity, readyFood);
                    freshnessCocktailCopy.SetActive(false);
                    freshnessCocktailCopy.name = freshnessCocktailCopy.name.Replace("(Clone)", "");
                } 
                // узнаем есть ли этот объект, если нет то создаем
                if (rubbishCopy == null)
                {
                    rubbishCopy = Instantiate(rubbish,_blenderPoints.GetSecondPoint(), Quaternion.identity, readyFood);
                    rubbishCopy.SetActive(false);
                    rubbishCopy.name = rubbishCopy.name.Replace("(Clone)", "");
                }
                // добавляем компонент BlenderRecipes
                _blenderRecipes = gameObject.AddComponent<BlenderRecipes>();
                // инициализируем BlenderRecipes
                _blenderRecipes.Initialize(wildBerryCocktailCopy,freshnessCocktailCopy,rubbishCopy,requiredFreshnessCocktail,requiredWildBerryCocktail);
            }
            if (!GetComponent<Blender>())
            {
                _script = gameObject.AddComponent<Blender>();
                _script.HeroikIsTrigger();
                _script.Initialize(timer, _heroik,  timerPoint, timerParent, objectOnTheTable, readyFoods,
                    _animator, _blenderPoints, parentFood,_blenderRecipes);
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
                Destroy(_blenderRecipes);
                Debug.Log("скрипты был удален");
            }
        }
    }
}
