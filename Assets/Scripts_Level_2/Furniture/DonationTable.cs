using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonationTable : MonoBehaviour
{
    [SerializeField] private GameObject objectOnTheTable;
    private Outline _outline;
    void Start()
    {
        _outline = GetComponent<Outline>();
        objectOnTheTable.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _outline.OutlineWidth = 2f;
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (!Heroik.IsBusyHands) //объект есть на столе, руки незаняты
                {
                    other.GetComponent<Heroik>().ActiveObjHands(objectOnTheTable);
                    
                    
                }
                else// объект есть на столе,руки заняты
                {
                    if(objectOnTheTable.name != other.GetComponent<Heroik>()._curentTakenObjects.name)
                    {
                        Debug.Log($"Объект есть на столе: {objectOnTheTable.name}, руки заняты:{Heroik.IsBusyHands}");
                        Debug.Log($"вы пытаетесь взять объект {objectOnTheTable.name}, когда у вас в руках {other.GetComponent<Heroik>()._curentTakenObjects.name}");
                    }
                    else
                    {
                        Debug.Log($"Объект есть на столе: {objectOnTheTable.name}, руки заняты:{Heroik.IsBusyHands}");
                        Debug.Log($"вы пытаетесь взять один и тот же объект");
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
}
