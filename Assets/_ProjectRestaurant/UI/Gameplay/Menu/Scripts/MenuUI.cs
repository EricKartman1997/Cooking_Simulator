using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public event Action ContinueAction;
    public event Action SettingsAction;
    public event Action ShowAction;
    public event Func<UniTask> ExitAction;
    
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    void Start()
    {
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
        SettingsAction?.Invoke();
    }
    
    private void ExitButton()
    {
        // загрузить левел меню
        ExitAction?.Invoke().Forget();
    }
    
    public void Show()
    {
        ShowAction?.Invoke();
        gameObject.SetActive(true);
        
    }


   
}
