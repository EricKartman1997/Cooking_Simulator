using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public event Action ContinueAction;
    public event Action SettingsAction;
    public event Func<UniTask> ExitAction;
    
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private SettingsViewController settingsViewController;
    
    private ICloseOpenSettingsPanel _settingsViewController;
    public bool IsOpen => _settingsViewController.IsOpen;
    void Start()
    {
        _settingsViewController = settingsViewController;
        continueButton.onClick.AddListener(ContinueButton);
        settingsButton.onClick.AddListener(SettingsButton);
        exitButton.onClick.AddListener(ExitButton);
    }

    private void ContinueButton()
    {
        // выкл паузу
        ContinueAction?.Invoke();
        
        // скрыть меню
        gameObject.SetActive(false);
    }
    
    private void SettingsButton()
    {
        // открыть настройки
        OpenSettingsPanel();
        SettingsAction?.Invoke();
    }
    
    private void ExitButton()
    {
        // загрузить левел меню
        ExitAction?.Invoke().Forget();
    }
    
    private void OpenSettingsPanel()
    {
        _settingsViewController.OpenPanel();
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        settingsViewController.FinishCloseAnim();
        gameObject.SetActive(false);
    }
    
    public void HideSettingsPanel()
    {
        _settingsViewController.ClosePanel();
    }
    
    
    public bool IsOpenSettingsPanel()
    {
        return IsOpen;
    }
    
}
