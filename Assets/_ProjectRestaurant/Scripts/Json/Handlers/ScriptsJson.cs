using System.Collections.Generic;
using UnityEngine;

public class ScriptsJson
{
        
}

public class GraphicSettings
{
    public bool IsFullScreen;
    public int QualityLevel;
    public int ResolutionSize;
    
    public GraphicSettings()
    {
        // Значения по умолчанию
        IsFullScreen = true;
        QualityLevel = 1;
        ResolutionSize = 0;
    }

    public void ShowValue()
    {
        Debug.Log($"IsFullScreen = {IsFullScreen}, QualityLevel = {QualityLevel}, ResolutionSize = {ResolutionSize}");
    }
}

public class AudioSettings
{
    public float MasterVolume;
    public float MusicVolum;
    public float SFXVolum;
    
    public AudioSettings()
    {
        // Значения по умолчанию
        MasterVolume = 80f;
        MusicVolum = 80f;
        SFXVolum = 80f;
    }

    public void ShowValue()
    {
        Debug.Log($"MasterVolume = {MasterVolume}, MusicVolum = {MusicVolum}, SFXVolum = {SFXVolum}");
    }
}

public class BindingSettings
{
    public string Rebinds;
    
    public BindingSettings()
    {
        // Значения по умолчанию
        Rebinds = "";
    }
    
    public BindingSettings(string rebinds)
    {
        Rebinds = rebinds;
    }

    // public void ShowValue()
    // {
    //     Debug.Log($"Rebinds = {Rebinds}");
    // }
}

public class EnvironmentItems
{
    public List<FurnitureItemData> ItemsEnvironmentList;
    
    public EnvironmentItems()
    {
        // Значения по умолчанию
        ItemsEnvironmentList = new List<FurnitureItemData>();
    }
    
    public EnvironmentItems(List<FurnitureItemData> itemsEnvironmentList)
    {
        ItemsEnvironmentList = itemsEnvironmentList;
    }
    
}