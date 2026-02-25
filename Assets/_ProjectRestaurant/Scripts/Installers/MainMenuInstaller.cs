using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private BootstrapMainMenu bootstrapMainMenu;
    public override void InstallBindings()
    {
        Container.Bind<BootstrapMainMenu>().FromInstance(bootstrapMainMenu).AsSingle();
        
        Container.BindInterfacesAndSelfTo<LoadReleaseMainMenuScene>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<SoundsServiceMainMenu>().AsSingle();

        Container.Bind<FactoryUIMainMenuScene>().AsSingle();
        Container.Bind<FactoryCamerasMenuScene>().AsSingle();
        Container.Bind<InternetUpdateService>().AsSingle();
    }
}
