using UnityEngine;
using UnityEngine.Audio;

public class SoundManager//: IDisposable, IInitializable
{
    private AudioMixer _audioMixer;
    
    
    public AudioMixerGroup MusicGroup { get; private set; }
    public AudioMixerGroup SFXGroup { get; private set; }
    public AudioMixerGroup MasterGroup { get; private set; }
    public AudioMixer AudioMixer => _audioMixer;

    public SoundManager(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;

        // Инициализация групп при создании
        InitGroups();
    }

    private void InitGroups()
    {
        var music = _audioMixer.FindMatchingGroups("Music");
        var sfx   = _audioMixer.FindMatchingGroups("SFX");
        var master = _audioMixer.FindMatchingGroups("Master");

        if (music.Length > 0) MusicGroup = music[0];
        if (sfx.Length > 0) SFXGroup = sfx[0];
        if (master.Length > 0) MasterGroup = master[0];
    }
    
    private float VolumeToDecibelLogarithmic(float volume)
    {
        volume = Mathf.Clamp(volume, 1f, 100f);
        
        if (volume <= 1f) return -80f;
        
        // Преобразуем в 0-1 диапазон
        float normalized = (volume - 1f) / 99f;
        // Логарифмическое масштабирование
        float logarithmic = Mathf.Log10(normalized * 9f + 1f);
        
        return logarithmic * 20f - 20f; // -20dB до 0dB
        
    }
    
    // Установка общей громкости
    // public void SetMasterVolume(float volume)
    // {
    //     // Сохраняем значение
    //     //PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    //     // Применяем к микшеру
    //     _audioMixer.SetFloat("Master", VolumeToDecibelLogarithmic(volume));
    // }
    
    public void SetMasterVolume(float volume)
    {
        float normalized = Mathf.Clamp(volume / 100f, 0.0001f, 1f);
        float dB = Mathf.Log10(normalized) * 20f;
        
        bool result = _audioMixer.SetFloat("Master", dB);
        if (!result) Debug.LogError("Микшер отклонил установку громкости Master! Проверьте Exposed Parameters.");
        //Debug.Log($"SoundManager Master (пришло){volume} -> (SetFloat){dB}");
    }
    
    // Установка громкости музыки
    public void SetMusicVolume(float volume)
    {
        //PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        _audioMixer.SetFloat("Music", VolumeToDecibelLogarithmic(volume));
    }
    
    // Установка громкости эффектов
    public void SetSFXVolume(float volume)
    {
        //PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
        _audioMixer.SetFloat("SFX", VolumeToDecibelLogarithmic(volume));
    }
    
    // // Загрузка сохраненных настроек
    // private void LoadVolumeSettings()
    // {
    //     SetMasterVolume(PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, DEFAULT_VOLUME));
    //     SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, DEFAULT_VOLUME));
    //     SetSFXVolume(PlayerPrefs.GetFloat(SFX_VOLUME_KEY, DEFAULT_VOLUME));
    //
    // }
    
    // // Получение текущих значений громкости
    // public float GetMasterVolume()
    // {
    //     return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, DEFAULT_VOLUME);
    // }
    //
    // public float GetMusicVolume()
    // {
    //     return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, DEFAULT_VOLUME);
    // }
    //
    // public float GetSFXVolume()
    // {
    //     return PlayerPrefs.GetFloat(SFX_VOLUME_KEY, DEFAULT_VOLUME);
    // }


}
