using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadReleaseMusic : MonoBehaviour
{
    private Dictionary<AudioName, AudioClip> _audioDic = new Dictionary<AudioName, AudioClip>();
    
    public IReadOnlyDictionary<AudioName, AudioClip> AudioDic
    {
        get
        {
            IReadOnlyDictionary<AudioName, AudioClip> readOnlyDict = _audioDic;
            return readOnlyDict;
        }
    }

    private void Awake()
    {
        InitMenuAudio();
    }

    private void InitMenuAudio()
    {
        var clickButtonSound = Addressables.LoadAssetAsync<AudioClip>("Assets/_ProjectRestaurant/Sounds/Menu/Click-Second.wav").Result;
        var selectButtonSound = Addressables.LoadAssetAsync<AudioClip>("Assets/_ProjectRestaurant/Sounds/Menu/ClickFlags.wav").Result ;
        var swipePanelSound = Addressables.LoadAssetAsync<AudioClip>("Assets/_ProjectRestaurant/Sounds/Menu/Swipe.mp3").Result;
        var backgroundSound = Addressables.LoadAssetAsync<AudioClip>("Assets/_ProjectRestaurant/Sounds/Menu/Background.mp3").Result ;
        
        _audioDic.Add(AudioName.ClickButton,clickButtonSound);
        _audioDic.Add(AudioName.HoverButton,selectButtonSound);
        _audioDic.Add(AudioName.SwipePanel,swipePanelSound);
        _audioDic.Add(AudioName.Background,backgroundSound);
    }
    
    private void ReleaseMenuAudio()
    {
        Addressables.Release("Assets/_ProjectRestaurant/Sounds/Menu/Click-Second.wav");
        Addressables.Release("Assets/_ProjectRestaurant/Sounds/Menu/ClickFlags.wav");
        Addressables.Release("Assets/_ProjectRestaurant/Sounds/Menu/Swipe.mp3");
        Addressables.Release("Assets/_ProjectRestaurant/Sounds/Menu/Background.mp3");
        
        _audioDic.Remove(AudioName.ClickButton);
        _audioDic.Remove(AudioName.HoverButton);
        _audioDic.Remove(AudioName.SwipePanel);
        _audioDic.Remove(AudioName.Background);
    }
}

public enum AudioName
{
    ClickButton,
    HoverButton,
    SwipePanel,
    Background
}
