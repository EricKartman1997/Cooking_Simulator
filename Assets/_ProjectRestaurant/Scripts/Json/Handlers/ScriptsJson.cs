using System.Collections.Generic;
using UnityEngine;

public class ScriptsJson
{
        
}

[System.Serializable]
public class GraphicSettings
{
    public bool IsFullScreen;
    public int QualityLevel;
    public int ResolutionSize;
    
    public GraphicSettings()
    {
        IsFullScreen = true;
        QualityLevel = 1;
        ResolutionSize = 0;
    }

    public void ShowValue()
    {
        Debug.Log($"IsFullScreen = {IsFullScreen}, QualityLevel = {QualityLevel}, ResolutionSize = {ResolutionSize}");
    }
}

[System.Serializable]
public class AudioSettings
{
    public float MasterVolume;
    public float MusicVolum;
    public float SFXVolum;
    
    public AudioSettings()
    {
        MasterVolume = 80f;
        MusicVolum = 80f;
        SFXVolum = 80f;
    }

    public void ShowValue()
    {
        Debug.Log($"MasterVolume = {MasterVolume}, MusicVolum = {MusicVolum}, SFXVolum = {SFXVolum}");
    }
}

[System.Serializable]
public class BindingSettings
{
    public string Rebinds;
    
    public BindingSettings()
    {
        Rebinds = "";
    }
    
    public BindingSettings(string rebinds)
    {
        Rebinds = rebinds;
    }
}

public class FurnitureItems
{
    public List<FurnitureItemData> ItemsFurnitureList;
    
    public FurnitureItems()
    {
        ItemsFurnitureList = new List<FurnitureItemData>();
    }
    
    public FurnitureItems(List<FurnitureItemData> itemsFurnitureList)
    {
        ItemsFurnitureList = itemsFurnitureList;
    }
    
}

public class FurnitureTrainingItems
{
    public List<FurnitureItemData> ItemsFurnitureList;
    
    public FurnitureTrainingItems()
    {
        ItemsFurnitureList = new List<FurnitureItemData>();
    }
    
    public FurnitureTrainingItems(List<FurnitureItemData> itemsFurnitureList)
    {
        ItemsFurnitureList = itemsFurnitureList;
    }
    
}

public class EnvironmentItems
{
    public List<EnvironmentItemData> ItemsEnvironmentList;
    
    public EnvironmentItems()
    {
        ItemsEnvironmentList = new List<EnvironmentItemData>();
    }
    
    public EnvironmentItems(List<EnvironmentItemData> itemsEnvironmentList)
    {
        ItemsEnvironmentList = itemsEnvironmentList;
    }
    
}