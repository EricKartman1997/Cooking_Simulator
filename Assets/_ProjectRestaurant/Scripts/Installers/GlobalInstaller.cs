using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer mainAudioMixer;
    [SerializeField] private GameSettings gameSettings;
    public override void InstallBindings()
    {
        Container.Bind<GameSettings>().FromInstance(gameSettings).AsSingle();
        Container.Bind<AudioMixer>().FromInstance(mainAudioMixer).AsSingle();

        Container.Bind<GamePlaySceneSettings>().AsSingle();
        Container.BindInterfacesAndSelfTo<JsonHandler>().AsSingle();
            
        Container.BindInterfacesAndSelfTo<LoadReleaseGlobalScene>().AsSingle();
        Container.BindInterfacesAndSelfTo<SoundManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<Graphic>().AsSingle();
        Container.BindInterfacesAndSelfTo<StorageData>().AsSingle();
    }
}
