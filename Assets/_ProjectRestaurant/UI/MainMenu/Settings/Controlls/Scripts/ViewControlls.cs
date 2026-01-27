using System;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class ViewControlls : MonoBehaviour
{
    private Action _resetedDefaultAction;
    [SerializeField] private List<ButtonManager> buttonsList;
    [SerializeField] private List<RebindControllsUI> defaulControllsList;
    [SerializeField] private ButtonManager defaulAllButton;
    [SerializeField] private RebindSaveLoaderControlls saveLoader; // Перетащи лоадер сюда в инспекторе!

    private ISoundsService _soundsService;

    [Inject]
    private void ConstructZenject(ISoundsService soundsService)
    {
        _soundsService = soundsService;
    }
    
    private void Start()
    {
        CreatDefaultAllButton();
        defaulAllButton.onClick.AddListener(ResetToDefault);
        EnableSoundSource();
    }

    private void ResetToDefault()
    {
        _resetedDefaultAction?.Invoke();
        
        // Сразу сохраняем дефолтное состояние
        if (saveLoader != null)
            saveLoader.SaveBindings();
    }

    private void EnableSoundSource()
    {
        if(defaulAllButton != null) defaulAllButton.soundSource = _soundsService.SourceSfx;
        foreach (var button in buttonsList)
        {
            if(button != null) button.soundSource = _soundsService.SourceSfx;
        }
    }

    private void CreatDefaultAllButton()
    {
        _resetedDefaultAction = null;
        foreach (var button in defaulControllsList)
        {
            if (button != null)
                _resetedDefaultAction += button.ResetToDefault;
        }
    }
}