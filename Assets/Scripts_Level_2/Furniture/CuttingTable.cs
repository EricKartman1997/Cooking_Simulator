using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CuttingTable : FurnitureAbstact
{
    private Animator _animator;
    
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    
    [SerializeField] private GameObject[] objectOnTheTable;
    [SerializeField] private GameObject[] readyFoods;
    private GameObject _firstFood = null;
    private GameObject _secondFood = null;
    
    private Outline _outline;
    private GameObject _result = null;
    private bool _isWork = false;
    
    private float _timeCurrent = 0.17f;
    private bool _onTrigger = false;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...


    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Work", false);
        _outline = GetComponent<Outline>();
    }

    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if (_onTrigger)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (_timeCurrent >= 0.17f)
                {
                    if(!Heroik.IsBusyHands) // руки не заняты
                    {
                        if (_isWork)
                        {
                            Debug.Log("ждите блюдо готовится");
                        }
                        else
                        {
                            if (_result == null)
                            {
                                if (ActiveObjectsOnTheTable() == 1) //один активный объект
                                {
                                    _heroik.ActiveObjHands(_firstFood);
                                    _firstFood.SetActive(false);
                                    _firstFood = null;
                                }
                                else// активного объекта нет
                                {
                                    Debug.Log("У вас пустые руки и прилавок пуст");
                                }
                            }
                            else // забрать предмет результат
                            {
                                _heroik.ActiveObjHands(GiveObj(ref _result));
                                //Debug.Log("Вы забрали конечный продукт"); 
                            }
                        }
                    }
                    else // заняты
                    {
                        if (_isWork)
                        {
                            Debug.Log("ждите блюдо готовится");
                        }
                        else
                        {
                            if (_result == null)
                            {
                                if (ActiveObjectsOnTheTable() == 1 )//один активный объект
                                {
                                    var nameBolud = _firstFood.GetComponent<Interactable>().IsMerge(_heroik._curentTakenObjects.GetComponent<Interactable>()) ;
                                    if (nameBolud != "None")
                                    {
                                        AcceptObject(_heroik.GiveObjHands(), 2);
                                        TurnOn();
                                        StartCookingProcess(nameBolud);
                                    }
                                    else
                                    {
                                        Debug.Log("Объект не подъходит для слияния");
                                    }
                                }
                                else// активного объекта нет
                                {
                                    if(_heroik._curentTakenObjects.GetComponent<Interactable>() && _heroik._curentTakenObjects.GetComponent<ObjsForCutting>())
                                    {
                                        AcceptObject(_heroik.GiveObjHands(), 1);
                                    }
                                    else
                                    {
                                        Debug.Log("с предметом нельзя взаимодействовать");
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("Сначала уберите предемет из рук");
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
    private byte ActiveObjectsOnTheTable()
    {
        if (_firstFood == null && _secondFood == null)
        {
            return 2;
        }
        else if (_firstFood == null && _secondFood != null || _firstFood != null && _secondFood == null)
        {
            return 1;
        }
        return 0; //  ошибка
    }
    
    protected override GameObject GiveObj(ref GameObject obj)
    {
        obj.SetActive(false);
        GameObject Cobj = obj;
        obj = null;
        return Cobj;
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
                    _firstFood = obj;
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
                    _secondFood = obj;
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
        _firstFood.SetActive(false);
        _secondFood.SetActive(false);
        _animator.SetBool("Work", true);
        Instantiate(timer, timerPoint.position, Quaternion.identity,timerParent);
    }
    protected override void TurnOff()
    {
        _isWork = false;
        _firstFood = null;
        _secondFood = null;
        _animator.SetBool("Work", false);
    }
    
    private async void StartCookingProcess(string obj)
    {
        await Task.Delay(3000);
        TurnOff();
        CreateResult(obj);
    }
    
}
