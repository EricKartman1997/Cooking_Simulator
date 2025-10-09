using System;
using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class GraphicsView : MonoBehaviour
{
    [SerializeField] private CustomToggle toggle;
    [SerializeField] private CustomDropdown dropdown;
    [SerializeField] private HorizontalSelector horizontalSelector;
    [SerializeField] private ButtonManager nextButton;
    [SerializeField] private ButtonManager backButton;

    private Graphic _graphic;
    private ISaveReadGraphicSettings _jsonHandler;
    private SoundsServiceMainMenu _soundsService;

    private GraphicSettings Settings => _jsonHandler.ReadOnlyGraphicSettings;

    [Inject]
    private void ConstructZenject(Graphic graphic,ISaveReadGraphicSettings jsonHandler,SoundsServiceMainMenu serviceMainMenu)
    {
        _graphic = graphic;
        _jsonHandler = jsonHandler;
        _soundsService = serviceMainMenu;
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
        EnabeleSounds();
    }

    private void DownloadSettings()
    {
        toggle.toggleObject.isOn = Settings.IsFullScreen;
        toggle.UpdateState();
        horizontalSelector.index = Settings.QualityLevel;
        horizontalSelector.UpdateUI();
        dropdown.SetDropdownIndex(Settings.ResolutionSize);
    }

    private void EnabeleSounds()
    {
        //nextButton.soundSource = _soundsService.SFX;
        nextButton.hoverSound = _soundsService.AudioDictionary[AudioNameMainMenu.HoverButton];
        nextButton.clickSound = _soundsService.AudioDictionary[AudioNameMainMenu.ClickButton];
        //backButton.soundSource = _soundsService.SFX;
        backButton.hoverSound = _soundsService.AudioDictionary[AudioNameMainMenu.HoverButton] ;
        backButton.clickSound = _soundsService.AudioDictionary[AudioNameMainMenu.ClickButton];

        //dropdown.soundSource = _soundsService.SFX;
        dropdown.hoverSound = _soundsService.AudioDictionary[AudioNameMainMenu.HoverButton];
        dropdown.clickSound = _soundsService.AudioDictionary[AudioNameMainMenu.ClickButton];
        
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
