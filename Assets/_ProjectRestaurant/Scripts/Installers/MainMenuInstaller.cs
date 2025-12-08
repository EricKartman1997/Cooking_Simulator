using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private BootstrapMainMenu bootstrapMainMenu;
    [SerializeField] private AudioSource sFX;
    [SerializeField] private AudioSource music;
    public override void InstallBindings()
    {
        Container.Bind<AudioSource>().WithId("SFX").FromInstance(sFX);
        Container.Bind<AudioSource>().WithId("Music").FromInstance(music);
        Container.Bind<BootstrapMainMenu>().FromInstance(bootstrapMainMenu).AsSingle();
        
        Container.BindInterfacesAndSelfTo<LoadReleaseMainMenuScene>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<SoundsServiceMainMenu>().AsSingle();
        //Container.Bind<ISoundsService>().To<SoundsServiceMainMenu>().AsSingle();

        Container.Bind<FactoryUIMainMenuScene>().AsSingle();
        //Container.BindInterfacesAndSelfTo<FactoryUIMainMenuScene>().AsSingle();
    }
}
