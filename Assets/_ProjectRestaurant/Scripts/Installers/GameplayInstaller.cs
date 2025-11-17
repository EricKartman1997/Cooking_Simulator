using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    // ScriptableObject
    [SerializeField] private FoodsForFurnitureContainer foodsForFurnitureContainer;
    [SerializeField] private RecipeContainer recipeContainer;
    
    [SerializeField] private BootstrapGameplay bootstrapGameplay;
    //[SerializeField] private AudioSource sFX;
    //[SerializeField] private AudioSource music;
    public override void InstallBindings()
    {
        // Container.Bind<AudioSource>().WithId("SFX").FromInstance(sFX);
        // Container.Bind<AudioSource>().WithId("Music").FromInstance(music);
        
        Container.Bind<FoodsForFurnitureContainer>().FromInstance(foodsForFurnitureContainer);
        Container.Bind<RecipeContainer>().FromInstance(recipeContainer);
        Container.Bind<BootstrapGameplay>().FromInstance(bootstrapGameplay).AsSingle();
        
        Container.BindInterfacesAndSelfTo<LoadReleaseGameplay>().AsSingle();
        Container.BindInterfacesAndSelfTo<SoundsServiceGameplay>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<FactoryPlayerGameplay>().AsSingle();
        Container.BindInterfacesAndSelfTo<FactoryEnvironment>().AsSingle();
        Container.BindInterfacesAndSelfTo<FactoryUIGameplay>().AsSingle();
        Container.BindInterfacesAndSelfTo<FactoryCamerasGameplay>().AsSingle();

        Container.BindInterfacesAndSelfTo<RecipeService>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<ProductsFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<ViewFactory>().AsSingle();
        
        
        
        
        Debug.Log("завершил инициализацию GameplayInstaller");
    }
}
