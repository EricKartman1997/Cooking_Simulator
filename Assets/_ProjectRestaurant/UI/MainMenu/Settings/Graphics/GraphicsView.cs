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
    private IStorageJson _jsonHandler;
    private ISoundsService _soundsService;

    private GraphicSettings _saveObj = new GraphicSettings();

    [Inject]
    private void ConstructZenject(Graphic graphic,IStorageJson jsonHandler,ISoundsService serviceMainMenu)
    {
        _graphic = graphic;
        _jsonHandler = jsonHandler;
        _soundsService = serviceMainMenu;
    }

    private void OnDestroy()
    {
        _saveObj.ResolutionSize = dropdown.selectedItemIndex;
        _saveObj.QualityLevel = horizontalSelector.index;
        _saveObj.IsFullScreen = toggle.toggleObject.isOn;
        _jsonHandler.Save(JsonPathName.GRAPHIC_SETTINGS_PATH,_saveObj);
    }
    
    private void Start()
    {
        EnabeleSoundSource();
        _graphic.InitializeResolutionDropdown(dropdown);
        
        toggle.toggleObject.onValueChanged.AddListener(SetFullScreen);
        
        horizontalSelector.items[0].onItemSelect.AddListener(SetQualityLow);
        horizontalSelector.items[1].onItemSelect.AddListener(SetQualityMedium);
        horizontalSelector.items[2].onItemSelect.AddListener(SetQualityHigh);
        horizontalSelector.items[3].onItemSelect.AddListener(SetQualityUltra);
        
        DownloadSettings();
    }

    private void DownloadSettings()
    {
        _jsonHandler.Load<GraphicSettings>(JsonPathName.GRAPHIC_SETTINGS_PATH, data =>
        {
            _graphic.SetFullScreen(data.IsFullScreen);
            toggle.toggleObject.isOn = data.IsFullScreen;
            toggle.UpdateState();
            _graphic.SetQuality((Quality)data.QualityLevel);
            horizontalSelector.index = data.QualityLevel;
            horizontalSelector.UpdateUI();
            dropdown.SetDropdownIndex(data.ResolutionSize);
        });
    }

    private void EnabeleSoundSource()
    {
        nextButton.soundSource = _soundsService.SourceSfx;
        backButton.soundSource = _soundsService.SourceSfx;
        dropdown.soundSource = _soundsService.SourceSfx;
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
