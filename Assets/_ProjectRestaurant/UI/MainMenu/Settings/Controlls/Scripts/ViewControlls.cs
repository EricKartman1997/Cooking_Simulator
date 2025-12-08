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
    }

    private void EnableSoundSource()
    {
        defaulAllButton.soundSource = _soundsService.SourceSfx;
        
        foreach (var button in buttonsList)
        {
            button.soundSource = _soundsService.SourceSfx;
        }
    }

    private void CreatDefaultAllButton()
    {
        foreach (var button in defaulControllsList)
        {
            if (button.GetComponent<RebindControllsUI.IResetToDefault>() != null)
            {
                _resetedDefaultAction += button.GetComponent<RebindControllsUI.IResetToDefault>().ResetToDefault;
            }
            
        }
    }
}
