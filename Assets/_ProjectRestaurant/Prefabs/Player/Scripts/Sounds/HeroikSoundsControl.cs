using System;
using UnityEngine;
using Zenject;

public class HeroikSoundsControl : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private LoadReleaseGameplay _loadReleaseGameplay;

    [Inject]
    private void ConstructZenject(LoadReleaseGameplay loadReleaseGameplay)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
    }

    public void PlayOneShotClip(AudioNameGamePlay nameClip)
    {
        switch (nameClip)
        {
            case AudioNameGamePlay.NotWorkTableSound: PlayOneShotNotWorkTable();
                break;
            case AudioNameGamePlay.ForbiddenSound: PlayOneShotForbiddenAction();
                break;
            default:
                Debug.LogError("Клип указан не правильно");
                break;
        }
    }

    private void PlayOneShotForbiddenAction()
    {
        AudioClip clip = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.ForbiddenSound];
        audioSource.PlayOneShot(clip);
    }
    
    private void PlayOneShotNotWorkTable()
    {
        AudioClip clip = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.NotWorkTableSound];
        audioSource.PlayOneShot(clip);
    }
}
