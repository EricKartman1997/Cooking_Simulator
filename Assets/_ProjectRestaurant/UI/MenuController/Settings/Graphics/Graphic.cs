using System;
using Michsky.MUIP;
using UnityEngine;

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
        //_jsonHandler.JsonGraphicSettingsFile.IsFullScreen = isFullScreen;
        //Debug.Log("SetFullScreen");
    }
    
    public void SetQuality(Quality quality)
    {
        QualitySettings.SetQualityLevel((int)quality);
        //_jsonHandler.JsonGraphicSettingsFile.QualityLevel = (int)quality;
        //Debug.Log("SetQuality " + quality);
    }
    
    public void InitializeResolutionDropdown(CustomDropdown resolutionDropdown)
    {
        // Очищаем существующие элементы dropdown
        resolutionDropdown.items.Clear();

        // Получение доступных разрешений
        _resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        // Формирование и добавление вариантов разрешений
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = $"{_resolutions[i].width} x {_resolutions[i].height} {_resolutions[i].refreshRate}Hz";
        
            // Добавляем элемент непосредственно в список items
            var newItem = new CustomDropdown.Item
            {
                itemName = option,
                itemIcon = null,
                itemIndex = i
            };
        
            resolutionDropdown.items.Add(newItem);

            // Проверка на текущее разрешение
            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Устанавливаем выбранный индекс
        resolutionDropdown.selectedItemIndex = currentResolutionIndex;

        // Обновляем dropdown (он уже инициализирован, так как существует в сцене)
        resolutionDropdown.SetupDropdown();

        // Добавляем обработчик изменений
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }
    
    private void SetResolution(int resolutionIndex)
    {
        if (_resolutions == null || resolutionIndex < 0 || resolutionIndex >= _resolutions.Length)
        {
            Debug.LogWarning("Ошибка разрешения");
            return;
        }
            
        
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //_jsonHandler.JsonGraphicSettingsFile.ResolutionSize = resolutionIndex;
    }
}

public enum Quality
{
    Low = 0,
    Medium = 1,
    High = 2,
    Ultra = 3
}
