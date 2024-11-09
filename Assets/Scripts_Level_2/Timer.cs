using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image timerObject;
    [SerializeField] private float time;
    private float _timer = 0f;

    private void Start()
    {

    }

    void Update()
    {
        if (time >= _timer) 
        { 
            _timer += Time.deltaTime;
            timerObject.fillAmount = _timer / time;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
