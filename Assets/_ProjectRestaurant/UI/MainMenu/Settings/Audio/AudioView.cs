using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class AudioView: MonoBehaviour
{
    [SerializeField] private SliderManager masterSlider;
    [SerializeField] private SliderManager musicSlider;
    [SerializeField] private SliderManager sfxSlider;

    private ISoundsService _soundService;
    private JsonHandler _jsonHandler;

    private AudioSettings _saveObj = new AudioSettings();

    private SoundManager SoundManager => _soundService.SoundManager;

    [Inject]
    private void ConstructZenject(ISoundsService soundService, JsonHandler jsonHandler)
    {
        _soundService = soundService;
        _jsonHandler = jsonHandler;
    }

    private void OnDestroy()
    {
        _saveObj.MasterVolume = masterSlider.mainSlider.value;
        _saveObj.MusicVolum = musicSlider.mainSlider.value;
        _saveObj.SFXVolum = sfxSlider.mainSlider.value;
        _jsonHandler.Save(JsonPathName.AUDIO_SETTINGS_PATH,_saveObj);
        //Debug.Log("(сохранение) Json Аудио");
        //_saveObj.ShowValue();
        //Debug.Log("OnDestroy GraphicsView");
    }
    private void Start()
    {
        // Добавляем обработчики изменений
        masterSlider.mainSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.mainSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.mainSlider.onValueChanged.AddListener(SetSFXVolume);
        
        DownloadSettings();

    }
    
    private void DownloadSettings()
    {
        _jsonHandler.Load<AudioSettings>(JsonPathName.AUDIO_SETTINGS_PATH, data =>
        {
            masterSlider.mainSlider.value = data.MasterVolume;// не работает
            musicSlider.mainSlider.value = data.MusicVolum;
            sfxSlider.mainSlider.value = data.SFXVolum;

            //Debug.Log("(загрузка) Json Аудио");
            //data.ShowValue();
        });
    }
    
    private void SetMasterVolume(float volume)
    {
        SoundManager.SetMasterVolume(volume);
        //Debug.Log("Движение Master");
    }
    
    private void SetMusicVolume(float volume)
    {
        SoundManager.SetMusicVolume(volume);
        //Debug.Log("Движение Music");
    }
    
    private void SetSFXVolume(float volume)
    {
        SoundManager.SetSFXVolume(volume);
        //Debug.Log("Движение SFX");
    }


}
