using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    private Animator _animator;
    private Outline _outline;
    private StovePoints _stovePoints;
    private StoveView _stoveView;
    void Start()
    {
        _outline = GetComponent<Outline>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _outline.OutlineWidth = 2f;
            _stovePoints = StaticManagerWithoutZenject.HelperScriptFactory.GetStovePoint();
            _stoveView = StaticManagerWithoutZenject.HelperScriptFactory.GetStoveView();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _outline.OutlineWidth = 0f;
        }
    }

    private void OnEnable()
    {
        EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        EventBus.PressE -= CookingProcess;
    }

    private void CookingProcess()
    {

    }
}
