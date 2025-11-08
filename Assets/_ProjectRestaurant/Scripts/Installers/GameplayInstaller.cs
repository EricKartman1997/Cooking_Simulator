using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private BootstrapGameplay bootstrapGameplay;
    [SerializeField] private AudioSource sFX;
    [SerializeField] private AudioSource music;
    public override void InstallBindings()
    {
        Container.Bind<AudioSource>().WithId("SFX").FromInstance(sFX);
        Container.Bind<AudioSource>().WithId("Music").FromInstance(music);
        
        Container.Bind<BootstrapGameplay>().FromInstance(bootstrapGameplay).AsSingle();
        
        Container.BindInterfacesAndSelfTo<LoadReleaseGameplay>().AsSingle();
        Container.BindInterfacesAndSelfTo<SoundsServiceGameplay>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<FactoryPlayerGameplay>().AsSingle();
        Container.BindInterfacesAndSelfTo<FactoryEnvironment>().AsSingle();
        Container.BindInterfacesAndSelfTo<FactoryUIGameplay>().AsSingle();
        Container.BindInterfacesAndSelfTo<FactoryCamerasGameplay>().AsSingle();
        Debug.Log("завершил инициализацию GameplayInstaller");
    }
}
