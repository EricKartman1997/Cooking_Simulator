using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutlineManager: IPause, IDisposable
{
    private List<Outline> _outlinesObjects;
    
    private IHandlerPause _pauseHandler;

    public OutlineManager(IHandlerPause pauseHandler)
    {
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
    }
    
    private void TurnOnOutline()
    {
        foreach (var obj in _outlinesObjects)
        {
            obj.enabled = true;
        }
    }
    
    private void TurnOffOutline()
    {
        foreach (var obj in _outlinesObjects)
        {
            obj.enabled = false;
        }
    }
    
    public void FindObjs(GameObject parent)
    {
        _outlinesObjects = parent.GetComponentsInChildren<Outline>().ToList();
    }

    public void SetPause(bool isPaused)
    {
        if (isPaused == true)
        {
            TurnOffOutline();
        }
        else
        {
            TurnOnOutline();
        }
    }

    public void Dispose()
    {
        _pauseHandler.Remove(this);
    }
}
