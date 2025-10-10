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
    private SoundsServiceMainMenu _soundsService;

    private GraphicSettings _saveObj = new GraphicSettings();

    [Inject]
    private void ConstructZenject(Graphic graphic,IStorageJson jsonHandler,SoundsServiceMainMenu serviceMainMenu)
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
        Debug.Log("полный экран (сохранение) = " + _saveObj.IsFullScreen);
        Debug.Log("OnDestroy GraphicsView");
    }

    private void Start()
    {
        _graphic.InitializeResolutionDropdown(dropdown);
        
        toggle.toggleObject.onValueChanged.AddListener(SetFullScreen);
        
        horizontalSelector.items[0].onItemSelect.AddListener(SetQualityLow);
        horizontalSelector.items[1].onItemSelect.AddListener(SetQualityMedium);
        horizontalSelector.items[2].onItemSelect.AddListener(SetQualityHigh);
        horizontalSelector.items[3].onItemSelect.AddListener(SetQualityUltra);
        //dropdown.onValueChanged.AddListener(SetResolution);

        //JsonDownload
        DownloadSettings();
        EnabeleSounds();
    }

    private void DownloadSettings()
    {
        _jsonHandler.Load<GraphicSettings>(JsonPathName.GRAPHIC_SETTINGS_PATH, data =>
        {
            _graphic.SetFullScreen(data.IsFullScreen);
            //toggle.toggleObject.isOn = data.IsFullScreen;
            //Debug.Log("полный экран (загрузка) = " + data.IsFullScreen);
            toggle.UpdateState();
            //horizontalSelector.index = data.QualityLevel;
            _graphic.SetQuality((Quality)data.QualityLevel);
            horizontalSelector.UpdateUI();
            dropdown.SetDropdownIndex(data.ResolutionSize);
            //_graphic.
        });
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
        //_saveObj.IsFullScreen = toggle.toggleObject.isOn;
    }
    
    private void SetQualityLow()
    {
        _graphic.SetQuality(Quality.Low);
        //_saveObj.QualityLevel = horizontalSelector.index;
    }
    
    private void SetQualityMedium()
    {
        _graphic.SetQuality(Quality.Medium);
        //_saveObj.QualityLevel = horizontalSelector.index;
    }
    
    private void SetQualityHigh()
    {
        _graphic.SetQuality(Quality.High);
        //_saveObj.QualityLevel = horizontalSelector.index;
    }
    
    private void SetQualityUltra()
    {
        _graphic.SetQuality(Quality.Ultra);
        //_saveObj.QualityLevel = horizontalSelector.index;
    }

    private void SetResolution(int index)
    {
        //_saveObj.ResolutionSize = index;
    }
    
}
