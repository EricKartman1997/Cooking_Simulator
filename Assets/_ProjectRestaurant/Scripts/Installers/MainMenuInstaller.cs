using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    //[SerializeField] private AudioSource sFX;
    //[SerializeField] private AudioSource background;
    public override void InstallBindings()
    {
        //Container.Bind<AudioSource>().WithId("SFX").FromInstance(sFX);
        //Container.Bind<AudioSource>().WithId("Background").FromInstance(background);
        
        Container.BindInterfacesAndSelfTo<LoadReleaseMainMenuScene>().AsSingle();
        Container.BindInterfacesAndSelfTo<SoundsServiceMainMenu>().AsSingle();
        Container.Bind<FactoryUIMainMenuScene>().AsSingle();
        //Container.BindInterfacesAndSelfTo<FactoryUIMainMenuScene>().AsSingle();
    }
}
