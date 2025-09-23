using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer mainAudioMixer;
    public override void InstallBindings()
    {
        Container.Bind<AudioMixer>().FromInstance(mainAudioMixer).AsSingle();
        
        Container.BindInterfacesAndSelfTo<LoadReleaseMusic>().AsSingle();
        Container.BindInterfacesAndSelfTo<SoundManager>().AsSingle();
    }
}
