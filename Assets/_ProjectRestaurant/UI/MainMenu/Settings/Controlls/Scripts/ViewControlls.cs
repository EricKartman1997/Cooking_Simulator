using System;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class ViewControlls : MonoBehaviour
{
    private Action _resetedDefaultAction;
    [SerializeField] private List<RebindControllsUI> defaulControllsList;
    [SerializeField] private ButtonManager defaulAllButton;

    private void Start()
    {
        CreatDefaultAllButton();
        defaulAllButton.onClick.AddListener(ResetToDefault);
    }

    private void ResetToDefault()
    {
        _resetedDefaultAction?.Invoke();
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
