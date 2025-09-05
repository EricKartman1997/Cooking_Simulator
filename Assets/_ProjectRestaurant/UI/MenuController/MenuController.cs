using System;
using Michsky.MUIP;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private ButtonManager buttonStart;
    [SerializeField] private ModalWindowManager windowManager;
    [SerializeField] private WindowManager settingsWindowManager;

    private void Awake()
    {
        windowManager.onCancel.AddListener(ShowOnCancel);
        windowManager.onClose.AddListener(ShowOnClose);
        windowManager.onConfirm.AddListener(ShowOnConfirm);
        windowManager.onOpen.AddListener(ShowOnOpen);
        
        buttonStart.onClick.AddListener(ShowOnClick);
        buttonStart.onHover.AddListener(ShowOnHover);
        buttonStart.onLeave.AddListener(ShowOnLeave);
        buttonStart.onDoubleClick.AddListener(ShowOnDoubleClick);

        
        Debug.Log("Init Done");
    }

    private void ShowOnCancel()
    {
        Debug.Log("OnCancelWindow");
    }
    private void ShowOnClose()
    {
        Debug.Log("OnCloseWindow");
    }
    private void ShowOnConfirm()
    {
        Debug.Log("OnConfirmWindow");
    }
    private void ShowOnOpen()
    {
        Debug.Log("OnOpenWindow");
    }
    private void ShowOnClick()
    {
        Debug.Log("onClick");
        windowManager.Open();
    }
    
    private void ShowOnHover()
    {
        Debug.Log("onHover");
    }
    
    private void ShowOnLeave()
    {
        Debug.Log("onLeave");
    }
    
    private void ShowOnDoubleClick()
    {
        Debug.Log("onDoubleClick");
    }
}
