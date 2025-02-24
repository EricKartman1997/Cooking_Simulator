using System.Threading.Tasks;
using UnityEngine;

public class Blender : FurnitureAbstact
{
    
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    [SerializeField] private GameObject[] objectOnTheTable;
    [SerializeField] private GameObject[] readyFoods;
    
    private Animator _animator;
    [SerializeField]private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    [SerializeField]private BlenderPoints _blenderPoints;
    
    [SerializeField]private GameObject _ingredient1 = null;
    [SerializeField]private GameObject _ingredient2 = null;
    [SerializeField]private GameObject _ingredient3 = null;
    private GameObject _result = null;
    private bool _isWork = false;
    private float _timeCurrent = 0.17f;
    private bool _heroikIsTrigger = false;
    [SerializeField]private Transform _parentFood;

    // для удаления визуала объекта над блендером
    [SerializeField]private GameObject _cloneIngridient1; 
    [SerializeField]private GameObject _cloneIngridient2;
    [SerializeField]private GameObject _cloneIngridient3;
    
    public void Initialize(GameObject timer,Heroik _heroik, Transform timerPoint,Transform timerParent,
        GameObject[] objectOnTheTable,GameObject[] readyFoods,Animator _animator,BlenderPoints _blenderPoints,Transform _parentFood)
    {
        this.timer = timer;
        this._heroik = _heroik;
        this.timerPoint = timerPoint;
        this.timerParent = timerParent;
        this.objectOnTheTable = objectOnTheTable;
        this.readyFoods = readyFoods;
        this._animator = _animator;
        this._blenderPoints = _blenderPoints;
        this._parentFood = _parentFood;
    }
    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && _heroikIsTrigger)
        {
            if (_timeCurrent >= 0.17f)
            {
                if(!Heroik.IsBusyHands) // руки не заняты
                {
                    if (_isWork)
                    {
                        Debug.Log("ждите блэндер готовится");
                    }
                    else
                    {
                        if (_result == null)
                        {
                            if (_ingredient1 == null)
                            {
                                Debug.Log("Руки пусты ингридиентов нет");
                            }
                            else
                            {
                                if (_ingredient2 == null)
                                {
                                    _heroik.ActiveObjHands(GiveObj(ref _cloneIngridient1,ref _ingredient1));
                                }
                                else
                                {
                                    _heroik.ActiveObjHands(GiveObj(ref _cloneIngridient2,ref _ingredient2));
                                }
                            }
                        }
                        else
                        {
                            _heroik.ActiveObjHands(GiveObj(ref _result));
                        }
                        
                    }
                    
                }
                else // руки заняты
                {
                    if (_isWork)
                    {
                        Debug.Log("ждите блэндер готовится");
                    }
                    else
                    {
                        if (_result == null)
                        {
                            if (_ingredient1 == null)
                            {
                                if(_heroik._curentTakenObjects.GetComponent<Interactable>() && _heroik._curentTakenObjects.GetComponent<Fruit>())
                                {
                                    AcceptObject(_heroik.GiveObjHands(), 1);
                                    Debug.Log("Предмет первый положен в блендер");
                                }
                                else
                                {
                                    Debug.Log("с предметом нельзя взаимодействовать");
                                }
                            }
                            else
                            {
                                if (_ingredient2 == null)
                                {
                                    if(_heroik._curentTakenObjects.GetComponent<Interactable>() && _heroik._curentTakenObjects.GetComponent<Fruit>())
                                    {
                                        AcceptObject(_heroik.GiveObjHands(), 2);
                                        Debug.Log("Предмет второй положен в блендер");
                                    }
                                    else
                                    {
                                        Debug.Log("с предметом нельзя взаимодействовать");
                                    }
                                }
                                else
                                {
                                    if(_heroik._curentTakenObjects.GetComponent<Interactable>() && _heroik._curentTakenObjects.GetComponent<Fruit>())
                                    {
                                        AcceptObject(_heroik.GiveObjHands(), 3);
                                        Debug.Log("Предмет третий положен в блендер");
                                        TurnOn(); 
                                        var objdish = FindReadyFood(_ingredient1,_ingredient2,_ingredient3);
                                        StartCookingProcessAsync(objdish);
                                        //yield return new WaitForSeconds(4f);
                                        //TurnOff();
                                        //CreateResult(objdish.name);
                                    }
                                    else
                                    {
                                        Debug.Log("с предметом нельзя взаимодействовать");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Руки полные уберите предмет");
                        }
                    }
                }
                _timeCurrent = 0f;
            }
            else
            {
                Debug.LogWarning("Ждите перезарядки кнопки");
            }
        }
    }

    private async void StartCookingProcessAsync(GameObject obj)
    {
        await Task.Delay(4000);
        TurnOff();
        CreateResult(obj.name);
    }
    
    private GameObject FindReadyFood(GameObject ingedient1, GameObject ingedient2, GameObject ingedient3)
    {
        string Apple = "Apple";
        string Orange = "Orange";
        string Cherry = "Cherry";
        string Strawberry = "Strawberry";
        string Lime = "Lime";
        string Blueberry = "Blueberry";
        
        if (ingedient1.name == Apple)
        {
            if (ingedient2.name == Lime)
            {
                if (ingedient3.name == Strawberry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient2.name == Strawberry)
            {
                if (ingedient3.name == Lime)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        else if (ingedient1.name == Orange)
        {
            if (ingedient2.name == Cherry)
            {
                if (ingedient3.name == Blueberry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient2.name == Blueberry)
            {
                if (ingedient3.name == Cherry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        else if (ingedient1.name == Cherry)
        {
            if (ingedient2.name == Orange)
            {
                if (ingedient3.name == Blueberry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient2.name == Blueberry)
            {
                if (ingedient3.name == Orange)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        else if (ingedient1.name == Strawberry)
        {
            if (ingedient2.name == Lime)
            {
                if (ingedient3.name == Apple)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient2.name == Apple)
            {
                if (ingedient3.name == Lime)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        else if (ingedient1.name == Lime)
        {
            if (ingedient2.name == Strawberry)
            {
                if (ingedient3.name == Apple)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient2.name == Apple)
            {
                if (ingedient3.name == Strawberry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        else if (ingedient1.name == Blueberry)
        {
            if (ingedient2.name == Orange)
            {
                if (ingedient3.name == Cherry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient2.name == Cherry)
            {
                if (ingedient3.name == Orange)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        foreach (var obj in readyFoods)
        {
            if (obj.name == "Rubbish")
            {
                return obj; 
            }
        } 
        return null;
    }
    protected override GameObject GiveObj(ref GameObject obj)
    {
        obj.SetActive(false);
        var cObj = obj;
        //obj = null;
        Destroy(obj);
        return cObj;
    }
    private  GameObject GiveObj(ref GameObject obj, ref GameObject ingredient) // добавить интерфейс на этот метод (SOLID)
    {
        obj.SetActive(false);
        var cObj = obj;
        ingredient = null;
        Destroy(obj);
        return cObj;
    }

    // protected override void AcceptObject(GameObject acceptObj, byte numberObj)
    // {
    //     if (numberObj == 1)
    //     {
    //         foreach (var obj in objectOnTheTable)
    //         {
    //             if (obj.name == acceptObj.name)
    //             {
    //                 obj.SetActive(true);
    //                 _ingredient1 = obj;
    //             }
    //         }
    //     }
    //     else if (numberObj == 2)
    //     {
    //         foreach (var obj in objectOnTheTable)
    //         {
    //             if (obj.name == acceptObj.name)
    //             {
    //                 obj.SetActive(true);
    //                 _ingredient2 = obj;
    //             }
    //         }
    //     }
    //     else if (numberObj == 3)
    //     {
    //         foreach (var obj in objectOnTheTable)
    //         {
    //             if (obj.name == acceptObj.name)
    //             {
    //                 obj.SetActive(true);
    //                 _ingredient3 = obj;
    //             }
    //         }
    //     }
    //     else
    //     {
    //         Debug.Log("Ошибка");
    //     }
    // }

    protected override void AcceptObject(GameObject acceptObj, byte numberObj )
    {
        if (_ingredient1 == null)
        {
            _ingredient1 = acceptObj;
            _cloneIngridient1 = Instantiate(_ingredient1, _blenderPoints.GetFirstPoint(), Quaternion.identity, _parentFood);
            _cloneIngridient1.name = _cloneIngridient1.name.Replace("(Clone)", "");
            _cloneIngridient1.SetActive(true);
        }
        else if (_ingredient2 == null)
        {
            _ingredient2 = acceptObj;
            _cloneIngridient2 = Instantiate(_ingredient2, _blenderPoints.GetSecondPoint(), Quaternion.identity, _parentFood);
            _cloneIngridient2.name = _cloneIngridient2.name.Replace("(Clone)", "");
            _cloneIngridient2.SetActive(true);
        }
        else if (_ingredient3 == null)
        {
            _ingredient3 = acceptObj;
            _cloneIngridient3 = Instantiate(_ingredient3, _blenderPoints.GetThirdPoint(), Quaternion.identity, _parentFood);
            _cloneIngridient3.name = _cloneIngridient3.name.Replace("(Clone)", "");
            _cloneIngridient3.SetActive(true);
        }
        else
        {
            Debug.LogWarning("В блендере нет места");
        }
    }

    protected override void CreateResult(string nameBolud)
    {
        foreach (var obj in readyFoods)
        {
            if (obj.name == nameBolud)
            {
                obj.SetActive(true);
                _result = obj; 
            }
        }
    }

    protected override void TurnOn()
    {
        _isWork = true;
        Destroy(_cloneIngridient1);
        Destroy(_cloneIngridient2);
        Destroy(_cloneIngridient3);
        _animator.SetBool("Work", true);
        Instantiate(timer, timerPoint.position, Quaternion.identity,timerParent);
    }

    protected override void TurnOff()
    {
        _isWork = false;
        _ingredient1 = null;
        _ingredient2 = null;
        _ingredient3 = null;
        _animator.SetBool("Work", false);
    }
    
    public bool IsAllowDestroy()
    {
        if (_ingredient1 == null && _ingredient2 == null && _ingredient3 == null && _result == null && !_isWork)
        {
            return true;
        }
        return false;
    }

    public void HeroikIsTrigger()
    {
        _heroikIsTrigger = !_heroikIsTrigger;
    }
}
