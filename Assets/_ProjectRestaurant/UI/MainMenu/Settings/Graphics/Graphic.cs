using System;
using Michsky.MUIP;
using UnityEngine;
using System.Linq;

public class Graphic : IDisposable
{
    private Resolution[] _resolutions;
    
    public void Dispose()
    {
        Debug.Log("Dispose Graphic");
    }
    
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
    public void SetQuality(Quality quality)
    {
        QualitySettings.SetQualityLevel((int)quality);
    }
    
    public void InitializeResolutionDropdown(CustomDropdown resolutionDropdown)
    {
        resolutionDropdown.items.Clear();
        
        var uniqueResolutions = Screen.resolutions
            .GroupBy(res => new { res.width, res.height })
            .Select(group => group.Last()) 
            .ToList();

        int currentResolutionIndex = 0;

        for (int i = 0; i < uniqueResolutions.Count; i++)
        {
            string option = $"{uniqueResolutions[i].width} x {uniqueResolutions[i].height}";
            
            var newItem = new CustomDropdown.Item
            {
                itemName = option,
                itemIcon = null,
                itemIndex = i
            };

            resolutionDropdown.items.Add(newItem);
            
            if (uniqueResolutions[i].width == Screen.currentResolution.width &&
                uniqueResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        _resolutions = uniqueResolutions.ToArray();

        resolutionDropdown.selectedItemIndex = currentResolutionIndex;
        resolutionDropdown.SetupDropdown();
        
        resolutionDropdown.onValueChanged.RemoveAllListeners();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }
    
    private void SetResolution(int resolutionIndex)
    {
        if (_resolutions == null || resolutionIndex < 0 || resolutionIndex >= _resolutions.Length)
        {
            Debug.LogWarning("SetResolution: Некорректный индекс разрешения.");
            return;
        }
    
        Resolution resolution = _resolutions[resolutionIndex];
        
        Screen.SetResolution(
            resolution.width, 
            resolution.height, 
            Screen.fullScreenMode,
            resolution.refreshRateRatio
        );

        Debug.Log($"Разрешение установлено: {resolution.width}x{resolution.height} @ {resolution.refreshRateRatio.value:F2}Hz");
    }
}

public enum Quality
{
    Low = 0,
    Medium = 1,
    High = 2,
    Ultra = 3
}
