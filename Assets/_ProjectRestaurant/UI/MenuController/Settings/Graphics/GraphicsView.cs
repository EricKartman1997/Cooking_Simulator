using System;
using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class GraphicsView : MonoBehaviour
{
    [SerializeField] private CustomToggle toggle;
    [SerializeField] private CustomDropdown dropdown;
    [SerializeField] private HorizontalSelector horizontalSelector;

    private Graphic _graphic;
    private ISaveReadGraphicSettings _jsonHandler;

    private GraphicSettings Settings => _jsonHandler.ReadOnlyGraphicSettings;

    [Inject]
    private void ConstructZenject(Graphic graphic,ISaveReadGraphicSettings jsonHandler)
    {
        _graphic = graphic;
        _jsonHandler = jsonHandler;
    }

    private void OnDestroy()
    {
        _jsonHandler.SaveGraphicSettings(toggle.toggleObject.isOn,horizontalSelector.index,dropdown.selectedItemIndex);
    }

    private void Start()
    {
        _graphic.InitializeResolutionDropdown(dropdown);
        
        toggle.toggleObject.onValueChanged.AddListener(SetFullScreen);
        
        horizontalSelector.items[0].onItemSelect.AddListener(SetQualityLow);
        horizontalSelector.items[1].onItemSelect.AddListener(SetQualityMedium);
        horizontalSelector.items[2].onItemSelect.AddListener(SetQualityHigh);
        horizontalSelector.items[3].onItemSelect.AddListener(SetQualityUltra);

        //JsonDownload
        DownloadSettings();
    }

    private void DownloadSettings()
    {
        toggle.toggleObject.isOn = Settings.IsFullScreen;
        toggle.UpdateState();
        horizontalSelector.index = Settings.QualityLevel;
        horizontalSelector.UpdateUI();
        dropdown.SetDropdownIndex(Settings.ResolutionSize);
    }
    
    private void SetFullScreen(bool isFullScreen)
    {
        _graphic.SetFullScreen(isFullScreen);
    }
    
    private void SetQualityLow()
    {
        _graphic.SetQuality(Quality.Low);
    }
    
    private void SetQualityMedium()
    {
        _graphic.SetQuality(Quality.Medium);
    }
    
    private void SetQualityHigh()
    {
        _graphic.SetQuality(Quality.High);
    }
    
    private void SetQualityUltra()
    {
        _graphic.SetQuality(Quality.Ultra);
    }
    
}
