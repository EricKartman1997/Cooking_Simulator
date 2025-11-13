using System;
using UnityEngine;
using Zenject;

public class SoundsServiceGameplay : IDisposable
{
    private DiContainer _container;
    private LoadReleaseGameplay _loadReleaseGameplay;
    private GameObject empty = new GameObject("Sounds_Test");
    private AudioSource _sourceSFX;
    private AudioSource _sourceMusic;

    public SoundsServiceGameplay(DiContainer container, LoadReleaseGameplay loadReleaseGameplay)
    {
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
    }

    public void Dispose()
    {
        Debug.Log("Dispose SoundsServiceGameplay");
    }

    public void CreateSounds()
    {
        GameObject sound = _container.InstantiatePrefab(_loadReleaseGameplay.ServiceDic[ServiceNameGamePlay.SoundsObject], empty.transform);
        
        GameObject obj = sound.transform.Find("SFX")?.gameObject;
        GameObject obj1 = sound.transform.Find("Music")?.gameObject;

        _sourceSFX = obj.GetComponent<AudioSource>();
        _sourceMusic = obj1.GetComponent<AudioSource>();
        
        _container.Bind<AudioSource>().WithId("SFX").FromInstance(_sourceSFX);
        //_container.Bind<AudioSource>().WithId("Music").FromInstance(_sourceMusic);
    }
    
    public void SetMusic()
    {
        _sourceMusic.clip = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.Background];
        _sourceMusic.Play();
    }

    public void StopSounds()
    {
        _sourceSFX.Stop();
        _sourceMusic.Stop();
    }
}
