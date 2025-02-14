using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingTable : Furniture
{
    [SerializeField] private GameObject[] objectOnTheTable;

    private Outline _outline;
    
    private bool _onTrigger = false;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private float _timeCurrent = 0.17f;
    void Start()
    {
        _outline = GetComponent<Outline>();
        foreach (var obj in objectOnTheTable)
        {
            obj.SetActive(false);
        }
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
                        if (ActiveObjectOnTheTable()) // ни одного активного объекта
                        {
                            Debug.Log("У вас пустые руки и прилавок пуст");
                        }
                        else // активного объект есть
                        {
                            foreach (var obj in objectOnTheTable)
                            {
                                if (!obj.activeInHierarchy)
                                {
                                
                                }
                                else
                                {
                                    _heroik.ActiveObjHands(obj);
                                    obj.SetActive(false);
                                    break;
                                }
                            }
                        }
                    }
                    else // заняты
                    {
                        if (ActiveObjectOnTheTable())// ни одного активного объекта
                        {
                            int count = 0;
                            foreach (var obj in objectOnTheTable)
                            {
                                if (_heroik._curentTakenObjects.name == obj.name)
                                {
                                    GameObject objHeroik = _heroik.GiveObjHands();
                                    obj.SetActive(true); // принять объект
                                    break;
                                }
                                else if(_heroik._curentTakenObjects.name != obj.name)
                                {
                                    count++;
                                    if (count == objectOnTheTable.Length)
                                    {
                                        Debug.Log($"объекта с именем {_heroik._curentTakenObjects.name} нет в списке");
                                    }
                                }
                            }
                        }
                        else// активного объект есть
                        {
                            Debug.Log("У вас полные руки и прилавок полон");
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
    
    private bool ActiveObjectOnTheTable()
    {
        int count = 0;
        foreach (var obj in objectOnTheTable)
        {
            if (!obj.activeInHierarchy)
            {
                count++;
                if(objectOnTheTable.Length == count)
                {
                    return true; // ни одного активного оъекта
                }
            }
            else
            {
                return false; // один активного оъекта
            }
        }
        return false; // один активного оъекта - ошибка
    }
}
