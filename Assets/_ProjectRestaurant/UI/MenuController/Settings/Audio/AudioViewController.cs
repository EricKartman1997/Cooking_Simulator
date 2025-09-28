using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class AudioViewController : MonoBehaviour
{
    [SerializeField] private SliderManager masterSlider;
    [SerializeField] private SliderManager musicSlider;
    [SerializeField] private SliderManager sfxSlider;

    private SoundsServiceMainMenu _soundService;

    private SoundManager SoundManager => _soundService.SoundManager;

    [Inject]
    private void ConstructZenject(SoundsServiceMainMenu soundService)
    {
        _soundService = soundService;
    }
    private void Start()
    {
        // Загружаем текущие значения
        masterSlider.mainSlider.value = SoundManager.GetMasterVolume();
        musicSlider.mainSlider.value = SoundManager.GetMusicVolume();
        sfxSlider.mainSlider.value = SoundManager.GetSFXVolume();
        
        // Добавляем обработчики изменений
        masterSlider.mainSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.mainSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.mainSlider.onValueChanged.AddListener(SetSFXVolume);
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
