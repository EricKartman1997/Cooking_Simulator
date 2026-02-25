using UnityEngine;
using UnityEngine.Audio;

public class SoundManager
{
    private AudioMixer _audioMixer;
    
    public AudioMixerGroup MusicGroup { get; private set; }
    public AudioMixerGroup SFXGroup { get; private set; }
    public AudioMixerGroup MasterGroup { get; private set; }
    public AudioMixer AudioMixer => _audioMixer;

    public SoundManager(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
        
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
        
        float normalized = (volume - 1f) / 99f;
        float logarithmic = Mathf.Log10(normalized * 9f + 1f);
        
        return logarithmic * 20f - 20f; // -20dB до 0dB
        
    }
    
    public void SetMasterVolume(float volume)
    {
        float normalized = Mathf.Clamp(volume / 100f, 0.0001f, 1f);
        float dB = Mathf.Log10(normalized) * 20f;
        
        bool result = _audioMixer.SetFloat("Master", dB);
        if (!result) Debug.LogError("Микшер отклонил установку громкости Master! Проверьте Exposed Parameters.");
    }
    
    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("Music", VolumeToDecibelLogarithmic(volume));
    }
    
    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("SFX", VolumeToDecibelLogarithmic(volume));
    }
}
