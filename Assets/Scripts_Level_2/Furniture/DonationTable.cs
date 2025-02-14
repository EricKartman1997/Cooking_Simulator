using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonationTable : Furniture
{
    [SerializeField] private GameObject objectOnTheTable;
    private Outline _outline;
    
    private bool _onTrigger = false;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private float _timeCurrent = 0.17f;
    void Start()
    {
        _outline = GetComponent<Outline>();
        objectOnTheTable.SetActive(true);
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
                    if (!Heroik.IsBusyHands) //объект есть на столе, руки незаняты
                    {
                        _heroik.ActiveObjHands(objectOnTheTable);
                    }
                    else// объект есть на столе,руки заняты
                    {
                        if(objectOnTheTable.name != _heroik._curentTakenObjects.name)
                        {
                            Debug.Log($"Объект есть на столе: {objectOnTheTable.name}, руки заняты:{Heroik.IsBusyHands}");
                            Debug.Log($"вы пытаетесь взять объект {objectOnTheTable.name}, когда у вас в руках {_heroik._curentTakenObjects.name}");
                        }
                        else
                        {
                            Debug.Log($"Объект есть на столе: {objectOnTheTable.name}, руки заняты:{Heroik.IsBusyHands}");
                            Debug.Log($"вы пытаетесь взять один и тот же объект");
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
}
