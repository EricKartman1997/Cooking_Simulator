using System;
using Michsky.MUIP;
using UnityEngine;

public class MenuViewController : MonoBehaviour
{
    [SerializeField] private ButtonManager buttonStart;
    [SerializeField] private ButtonManager buttonChoiceLevel;
    [SerializeField] private ButtonManager buttonExit;
    [SerializeField] private ButtonManager buttonSettings;
    [SerializeField] private ButtonManager buttonSocialNetworks;
    
    [SerializeField] private ModalWindowManager warringWindow;

    [SerializeField] private SettingsViewController settingsViewController;
    //[SerializeField] private WindowManager settingsWindowManager;

    private void OnEnable()
    {
        buttonSettings.onClick.AddListener(settingsViewController.OpenPanel);
        buttonStart.onClick.AddListener(ShowOnClick);
    }

    private void OnDisable()
    {
        buttonSettings.onClick.RemoveListener(settingsViewController.OpenPanel);
        buttonStart.onClick.RemoveListener(ShowOnClick);
    }

    private void Awake()
    {
        
        
        
        warringWindow.onCancel.AddListener(ShowOnCancel);
        warringWindow.onClose.AddListener(ShowOnClose);
        warringWindow.onConfirm.AddListener(ShowOnConfirm);
        warringWindow.onOpen.AddListener(ShowOnOpen);
        
        
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
        warringWindow.Open();
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
