using Michsky.MUIP;
using UnityEngine;
using Zenject;

public class AudioViewController : MonoBehaviour
{
    [SerializeField] private SliderManager masterSlider;
    [SerializeField] private SliderManager musicSlider;
    [SerializeField] private SliderManager sfxSlider;

    private SoundManager _soundManager;

    [Inject]
    private void ConstructZenject(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }
    private void Start()
    {
        // Загружаем текущие значения
        masterSlider.mainSlider.value = _soundManager.GetMasterVolume();
        musicSlider.mainSlider.value = _soundManager.GetMusicVolume();
        sfxSlider.mainSlider.value = _soundManager.GetSFXVolume();
        
        // Добавляем обработчики изменений
        masterSlider.mainSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.mainSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.mainSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    
    private void SetMasterVolume(float volume)
    {
        _soundManager.SetMasterVolume(volume);
    }
    
    private void SetMusicVolume(float volume)
    {
        _soundManager.SetMusicVolume(volume);
        //Debug.Log("Изменилось");
    }
    
    private void SetSFXVolume(float volume)
    {
        _soundManager.SetSFXVolume(volume);
    }


}
