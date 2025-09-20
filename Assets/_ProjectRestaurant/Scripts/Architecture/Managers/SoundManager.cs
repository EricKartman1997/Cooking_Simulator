using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SoundManager: IDisposable
{
    public AudioClip ClickButtonSound;
    public AudioClip SelectButtonSound;
    public AudioClip SwipePanelSound;
    public AudioClip BackgroundSound;

    public SoundManager()
    {
        InitMenuAudio();
    }
    
    public void Dispose()
    {
        ReleaseMenuAudio();
    }

    private void InitMenuAudio()
    {
        ClickButtonSound = Addressables.LoadAssetAsync<AudioClip>("Assets/_ProjectRestaurant/Sounds/Menu/Click-Second.wav").Result;
        SelectButtonSound = Addressables.LoadAssetAsync<AudioClip>("Assets/_ProjectRestaurant/Sounds/Menu/ClickFlags.wav").Result ;
        SwipePanelSound = Addressables.LoadAssetAsync<AudioClip>("Assets/_ProjectRestaurant/Sounds/Menu/Swipe.mp3").Result;
        BackgroundSound = Addressables.LoadAssetAsync<AudioClip>("Assets/_ProjectRestaurant/Sounds/Menu/Background.mp3").Result ;
    }
    
    private void ReleaseMenuAudio()
    {
        Addressables.Release("Assets/_ProjectRestaurant/Sounds/Menu/Click-Second.wav");
        Addressables.Release("Assets/_ProjectRestaurant/Sounds/Menu/ClickFlags.wav");
        Addressables.Release("Assets/_ProjectRestaurant/Sounds/Menu/Swipe.mp3");
        Addressables.Release("Assets/_ProjectRestaurant/Sounds/Menu/Background.mp3");
    }
}
