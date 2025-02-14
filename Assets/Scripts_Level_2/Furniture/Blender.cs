using System.Threading.Tasks;
using UnityEngine;

public class Blender : FurnitureAbstact
{
    private Animator _animator;
    
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    
    private Outline _outline;
    
    [SerializeField] private GameObject[] objectOnTheTable;
    [SerializeField] private GameObject[] readyFoods;
    
    private GameObject _ingedient1 = null;
    private GameObject _ingedient2 = null;
    private GameObject _ingedient3 = null;
    private GameObject _result = null;
    private bool _isWork = false;
    private bool _onTrigger = false;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private float _timeCurrent = 0.17f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        //_animator.SetBool("Work", false);
        _outline = GetComponent<Outline>();
    }
    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if (_onTrigger)
        {
            if (Input.GetKey(KeyCode.E))
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
                                if (_ingedient1 == null)
                                {
                                    Debug.Log("Руки пусты ингридиентов нет");
                                }
                                else
                                {
                                    if (_ingedient2 == null)
                                    {
                                        _heroik.ActiveObjHands(GiveObj(ref _ingedient1));
                                    }
                                    else
                                    {
                                        _heroik.ActiveObjHands(GiveObj(ref _ingedient2));
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
                                if (_ingedient1 == null)
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
                                    if (_ingedient2 == null)
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
                                            var objdish = FindReadyFood(_ingedient1,_ingedient2,_ingedient3);
                                            StartCookingProcess(objdish);
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
    }

    private async void StartCookingProcess(GameObject obj)
    {
        await Task.Delay(4000);
        TurnOff();
        CreateResult(obj.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            _onTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = null;
            _outline.OutlineWidth = 0f;
            _onTrigger = false;
        }
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
        obj = null;
        return cObj;
    }

    protected override void AcceptObject(GameObject acceptObj, byte numberObj)
    {
        if (numberObj == 1)
        {
            foreach (var obj in objectOnTheTable)
            {
                if (obj.name == acceptObj.name)
                {
                    obj.SetActive(true);
                    _ingedient1 = obj;
                }
            }
        }
        else if (numberObj == 2)
        {
            foreach (var obj in objectOnTheTable)
            {
                if (obj.name == acceptObj.name)
                {
                    obj.SetActive(true);
                    _ingedient2 = obj;
                }
            }
        }
        else if (numberObj == 3)
        {
            foreach (var obj in objectOnTheTable)
            {
                if (obj.name == acceptObj.name)
                {
                    obj.SetActive(true);
                    _ingedient3 = obj;
                }
            }
        }
        else
        {
            Debug.Log("Ошибка");
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
        _ingedient1.SetActive(false);
        _ingedient2.SetActive(false);
        _ingedient3.SetActive(false);
        _animator.SetBool("Work", true);
        Instantiate(timer, timerPoint.position, Quaternion.identity,timerParent);
    }

    protected override void TurnOff()
    {
        _isWork = false;
        _ingedient1 = null;
        _ingedient2 = null;
        _ingedient3 = null;
        _animator.SetBool("Work", false);
    }
}
