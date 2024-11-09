using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingTable : MonoBehaviour
{
    [SerializeField] private GameObject[] objectOnTheTable;

    private Outline _outline;
    void Start()
    {
        _outline = GetComponent<Outline>();
        foreach (var obj in objectOnTheTable)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _outline.OutlineWidth = 2f;
            if(Input.GetKeyDown(KeyCode.E))
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
                                other.GetComponent<Heroik>().ActiveObjHands(obj);
                                obj.SetActive(false);
                            }
                        }
                        Heroik.IsBusyHands = true;
                    }
                }
                else // заняты
                {
                    if (ActiveObjectOnTheTable())// ни одного активного объекта
                    {
                        int count = 0;
                        foreach (var obj in objectOnTheTable)
                        {
                            if (other.GetComponent<Heroik>()._curentTakenObjects.name == obj.name)
                            {
                                obj.SetActive(true);
                                //Debug.Log($"Объект: {other.GetComponent<Heroik>()._curentTakenObjects} перемещен на стол ");
                                other.GetComponent<Heroik>()._curentTakenObjects.SetActive(false);
                                Heroik.IsBusyHands = false;
                                other.GetComponent<Heroik>()._curentTakenObjects = null;
                                break;
                            }
                            else if(other.GetComponent<Heroik>()._curentTakenObjects.name != obj.name)
                            {
                                count++;
                                if (count == objectOnTheTable.Length)
                                {
                                    Debug.Log($"объекта с именем {other.GetComponent<Heroik>()._curentTakenObjects.name} нет в списке");
                                }
                            }
                        }
                    }
                    else// активного объект есть
                    {
                        Debug.Log("У вас полные руки и прилавок полон");
                    }
                }
            }
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _outline.OutlineWidth = 0f;
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
