using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehavior : MonoBehaviour
{
    [SerializeField] private NotificationReady notificationReady;
    private Animator _animator;

    private void Awake()
    {
        _animator = notificationReady.gameObject.GetComponent<Animator>();
    }
    
    public void AnimButton()
    {
        _animator.SetTrigger("FinishFast");
    }
}
