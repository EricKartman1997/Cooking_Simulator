using Michsky.MUIP;
using UnityEngine;

public class AudioViewController : MonoBehaviour
{
    [SerializeField] private SliderManager masterSlider;
    [SerializeField] private SliderManager musicSlider;
    [SerializeField] private SliderManager sfxSlider;
    
    private void Start()
    {
        // Загружаем текущие значения
        masterSlider.mainSlider.value = SoundManager.Instance.GetMasterVolume();
        musicSlider.mainSlider.value = SoundManager.Instance.GetMusicVolume();
        sfxSlider.mainSlider.value = SoundManager.Instance.GetSFXVolume();
        
        // Добавляем обработчики изменений
        masterSlider.mainSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.mainSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.mainSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    
    private void SetMasterVolume(float volume)
    {
        SoundManager.Instance.SetMasterVolume(volume);
    }
    
    private void SetMusicVolume(float volume)
    {
        SoundManager.Instance.SetMusicVolume(volume);
        //Debug.Log("Изменилось");
    }
    
    private void SetSFXVolume(float volume)
    {
        SoundManager.Instance.SetSFXVolume(volume);
    }
}
