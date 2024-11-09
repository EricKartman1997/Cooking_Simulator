using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    private Outline _outline;
    void Start()
    {
        _outline = GetComponent<Outline>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _outline.OutlineWidth = 2f;
            if(Input.GetKeyDown(KeyCode.E))
            {
                try
                {
                    var discardedObj = other.GetComponent<Heroik>()._curentTakenObjects;
                    discardedObj.SetActive(false);
                    Heroik.IsBusyHands = false;
                    other.GetComponent<Heroik>()._curentTakenObjects = null;
                    Debug.Log($"Руки пустые: {Heroik.IsBusyHands},объект под названием {discardedObj.name} выкинут");
                }
                catch (Exception e)
                {
                    Debug.Log("Вам нечего выкидывать" + e);
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
