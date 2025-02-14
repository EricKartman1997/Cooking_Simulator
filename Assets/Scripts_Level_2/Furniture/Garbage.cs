using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    private Outline _outline;
    
    private bool _onTrigger = false;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private float _timeCurrent = 0.17f;
    void Start()
    {
        _outline = GetComponent<Outline>();
    }

    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if (_onTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(_timeCurrent >= 0.17f)
                {
                    try
                    {
                        GameObject discardedObj = _heroik.GiveObjHands();
                        Debug.Log($"Руки пустые: {Heroik.IsBusyHands},объект под названием {discardedObj.name} выкинут");
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Вам нечего выкидывать" + e);
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
