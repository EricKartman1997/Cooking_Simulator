using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Oven : MonoBehaviour, IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IIsAllowDestroy,IHeroikIsTrigger
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
    private Dictionary<RawFood, FoodReadyOven> _dictionaryProduct;
    
    private bool _isWork = false;
    private bool _heroikIsTrigger = false;
    private GameObject _result;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private float _timeCurrent = 0.17f;
    
    public void Initialize(GameObject glassOn, GameObject glassOff,GameObject switchFirst,GameObject switchSecond,GameObject[] foodOnTheOver,GameObject[] cookedFoodOnTheOver,GameObject timer,Transform timerPoint,Transform timerParent,Heroik _heroik,Dictionary<RawFood, FoodReadyOven> dictionaryProduct)
    {
        this.glassOn = glassOn;
        this.glassOff = glassOff;
        this.switchFirst = switchFirst;
        this.switchSecond = switchSecond;
        this.foodOnTheOver = foodOnTheOver;
        this.cookedFoodOnTheOver = cookedFoodOnTheOver;
        this.timer = timer;
        this.timerPoint = timerPoint;
        this.timerParent = timerParent;
        this._heroik = _heroik;
        _dictionaryProduct = dictionaryProduct;
    }
    
    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.E) && _heroikIsTrigger )
        {
            if (_timeCurrent >= 0.17f)
            {
                if (!Heroik.IsBusyHands) // руки не заняты
                {
                    if (_isWork)
                    {
                        //Debug.Log("ждите печка работает");
                    }
                    else
                    {
                        if (_result != null)
                        {
                            _heroik.ActiveObjHands(GiveObj(ref _result));
                        }
                        else
                        {
                            //Debug.Log("печка пуста руки тоже");
                        }
                    }
                }
                else // заняты
                {
                    if (_isWork)
                    {
                        //Debug.Log("ждите печка работает");
                    }
                    else
                    {
                        if (_result != null)
                        {
                            //Debug.Log("Сначала заберите предмет");
                        }
                        else
                        {
                            int count = 0;
                            foreach (GameObject obj in foodOnTheOver)
                            {
                                if (_heroik.GetCurentTakenObjects().name == obj.name)
                                {
                                    TurnOn();
                                    AcceptObject(_heroik.GiveObjHands());
                                    StartCookingProcessAsync(obj);
                                    break;
                                }
                                if(_heroik.GetCurentTakenObjects().name != obj.name)
                                {
                                    count++;
                                    if (count == foodOnTheOver.Length)
                                    {
                                        //Debug.LogError($"Из этого объекта {_heroik.GetCurentTakenObjects().name} ничего нельзя приготовить в духовке");
                                    }
                                }
                            }
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
        await Task.Delay(5000);
        TurnOff();
        CreateResult(obj);
    }

    private GameObject IssuanceOfTheResult(GameObject obj)
    {
        foreach (var cookedFood in cookedFoodOnTheOver)
        {
            if (obj.name == "Apple")
            {
                if (cookedFood.name == "BakedApple")
                {
                    cookedFood.SetActive(true);
                    return cookedFood;
                }
            }
            else if (obj.name == "Orange")
            {
                if (cookedFood.name == "BakedOrange")
                {
                    cookedFood.SetActive(true);
                    return cookedFood;
                }
            }
            else if (obj.name == "Fish")
            {
                if (cookedFood.name == "BakedFish")
                {
                    cookedFood.SetActive(true);
                    return cookedFood;
                }
            }
            else if (obj.name == "Meat")
            {
                if (cookedFood.name == "BakedMeat")
                {
                    cookedFood.SetActive(true);
                    return cookedFood;
                }
            }
        }
        Debug.Log($"из этого {obj.name} продукта ничего не приготовить //Ошибка");
        return null;
    }
    
    public GameObject GiveObj(ref GameObject obj)
    {
        Debug.Log("забираем предмет");
        obj.SetActive(false);
        var Cobj = obj;
        obj = null;
        return Cobj;
    }

    public void AcceptObject(GameObject obj)
    {

    }

    // public void CreateResult(string obj)
    // {
    //     if (obj == "Apple")
    //     {
    //         FindObject("BakedApple");
    //     }
    //     else if (obj == "Orange")
    //     {
    //         FindObject("BakedOrange");
    //     }
    //     else if (obj == "Fish")
    //     {
    //         FindObject("BakedFish");
    //     }
    //     else if (obj == "Meat")
    //     {
    //         FindObject("BakedMeat");
    //     }
    //     else
    //     {
    //         Debug.Log($"из этого {obj} продукта ничего не приготовить //Ошибка");
    //     }
    // }
    
    public void TurnOn()
    {
        _isWork = true;
        glassOff.SetActive(false);
        glassOn.SetActive(true);
        switchFirst.transform.rotation = Quaternion.Euler(0, 0, -90);
        switchSecond.transform.rotation = Quaternion.Euler(0, 0, -135);
        Instantiate(timer, timerPoint.position, Quaternion.identity,timerParent);
    }

    public void TurnOff()
    {
        _isWork = false;
        glassOff.SetActive(true);
        glassOn.SetActive(false);
        switchFirst.transform.rotation = Quaternion.Euler(0, 0, 0);
        switchSecond.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    private void FindObject(string obj)
    {
        foreach (var cookedFood in cookedFoodOnTheOver)
        {
            if (cookedFood.name == obj)
            {
                cookedFood.SetActive(true);
                _result = cookedFood;
            }
        }
    }
    
    public bool IsAllowDestroy()
    {
        if (!_isWork && _result == null)
        {
            return true;
        }
        return false;
    }
    public void HeroikIsTrigger()
    {
        _heroikIsTrigger = !_heroikIsTrigger;
    }

    public void CreateResult(GameObject obj)
    {
        RawFood rawFood = obj.GetComponent<RawFood>();
        if (_dictionaryProduct.TryGetValue(rawFood, out FoodReadyOven bakedObj))
        {
            // создать объект визуальный
            _result = bakedObj.gameObject;
        }
        else
        {
            Debug.Log("Объект нельзя приготовить");
        }
        
    }
}
