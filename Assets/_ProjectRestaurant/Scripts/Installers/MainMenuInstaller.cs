using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<LoadReleaseMainMenuScene>().AsSingle();
        Container.BindInterfacesAndSelfTo<SoundsServiceMainMenu>().AsSingle();
        Container.Bind<FactoryUIMainMenuScene>().AsSingle();
        //Container.BindInterfacesAndSelfTo<FactoryUIMainMenuScene>().AsSingle();

        
    }
}
