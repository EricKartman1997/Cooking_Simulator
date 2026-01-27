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
        // Очищаем существующие элементы
        resolutionDropdown.items.Clear();

        // 1. Получаем все разрешения
        // 2. Группируем их по ширине и высоте
        // 3. Из каждой группы берем только одно (последнее обычно с максимальной герцовкой)
        var uniqueResolutions = Screen.resolutions
            .GroupBy(res => new { res.width, res.height })
            .Select(group => group.Last()) 
            .ToList();

        int currentResolutionIndex = 0;

        for (int i = 0; i < uniqueResolutions.Count; i++)
        {
            string option = $"{uniqueResolutions[i].width} x {uniqueResolutions[i].height}";

            // Создаем элемент для вашего CustomDropdown
            var newItem = new CustomDropdown.Item
            {
                itemName = option,
                itemIcon = null,
                itemIndex = i
            };

            resolutionDropdown.items.Add(newItem);

            // Проверяем, является ли это разрешение текущим
            if (uniqueResolutions[i].width == Screen.currentResolution.width &&
                uniqueResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Сохраняем отфильтрованный список в массив, чтобы потом использовать в SetResolution
        _resolutions = uniqueResolutions.ToArray();

        resolutionDropdown.selectedItemIndex = currentResolutionIndex;
        resolutionDropdown.SetupDropdown();
    
        // Важно: убедитесь, что слушатель не добавляется повторно при каждом вызове
        resolutionDropdown.onValueChanged.RemoveAllListeners();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }
    
    private void SetResolution(int resolutionIndex)
    {
        // Проверка на корректность индекса
        if (_resolutions == null || resolutionIndex < 0 || resolutionIndex >= _resolutions.Length)
        {
            Debug.LogWarning("SetResolution: Некорректный индекс разрешения.");
            return;
        }
    
        Resolution resolution = _resolutions[resolutionIndex];

        // Применяем разрешение. 
        // Используем resolution.refreshRateRatio, чтобы сохранить частоту обновления, 
        // выбранную системой как оптимальную для этого разрешения.
        Screen.SetResolution(
            resolution.width, 
            resolution.height, 
            Screen.fullScreenMode, // Используем текущий режим (окно/полный экран)
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
