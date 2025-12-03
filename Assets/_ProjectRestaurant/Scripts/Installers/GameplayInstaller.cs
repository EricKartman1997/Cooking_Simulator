using System;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    // ScriptableObject
    [SerializeField] private FoodsForFurnitureContainer foodsForFurnitureContainer;
    [SerializeField] private RecipeContainer recipeContainer;
    [SerializeField] private CheckContainer checkContainer;
    
    [SerializeField] private BootstrapGameplay bootstrapGameplay;
    //[SerializeField] private AudioSource sFX;
    //[SerializeField] private AudioSource music;
    public override void InstallBindings()
    {
        // Container.Bind<AudioSource>().WithId("SFX").FromInstance(sFX);
        // Container.Bind<AudioSource>().WithId("Music").FromInstance(music);
        
        Container.Bind<FoodsForFurnitureContainer>().FromInstance(foodsForFurnitureContainer);
        Container.Bind<RecipeContainer>().FromInstance(recipeContainer);
        Container.Bind<CheckContainer>().FromInstance(checkContainer);
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
        Container.BindInterfacesAndSelfTo<HelperScriptFactory>().AsSingle();
        //Container.BindInterfacesAndSelfTo<ChecksFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<ChecksManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<UpdateChecks>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<TimeGame>().AsSingle();
        Container.BindInterfacesAndSelfTo<Orders>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameOver>().AsSingle();
        Container.BindInterfacesAndSelfTo<Score>().AsSingle();
        Container.BindInterfacesAndSelfTo<Menu>().AsSingle();
        
        //Container.Bind<TimeGameUI>().AsSingle();
        Container.Bind<ManagerMediator>().AsSingle().Lazy();
        
        BindCheckFactory();
        BindCheckPrefabFactory();
        //Debug.Log("завершил инициализацию GameplayInstaller");
    }

    private void BindCheckFactory()
    {
        Container.BindFactory<CheckType, Check, CheckFactory>()
            .FromMethod((container, type) =>
            {
                var checkContainer = container.Resolve<CheckContainer>();
                var checksManager = container.Resolve<ChecksManager>();

                CheckConfig config = type switch
                {
                    CheckType.BakedFish        => checkContainer.BakedFish,
                    CheckType.BakedMeat        => checkContainer.BakedMeat,
                    CheckType.BakedSalad       => checkContainer.BakedSalad,
                    CheckType.FruitSalad       => checkContainer.FruitSalad,
                    CheckType.CutletMedium     => checkContainer.CutletMedium,
                    CheckType.WildBerryCocktail => checkContainer.WildBerryCocktail,
                    CheckType.FreshnessCocktail => checkContainer.FreshnessCocktail,
                    _ => throw new ArgumentOutOfRangeException()
                };

                Check instance = type switch
                {
                    CheckType.BakedFish        => new BakedFishCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager),
                    CheckType.BakedMeat        => new BakedMeatCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager),
                    CheckType.BakedSalad       => new BakedSaladCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager),
                    CheckType.FruitSalad       => new FruitSaladCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager),
                    CheckType.CutletMedium     => new CutletMediumCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager),
                    CheckType.WildBerryCocktail => new WildBerryCocktailCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager),
                    CheckType.FreshnessCocktail => new FreshnessCocktailCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager),
                    _ => throw new ArgumentOutOfRangeException()
                };

                //container.Inject(instance);
                //Debug.Log("Сделал инджект в объект");

                return instance;
            });

    }

    private void BindCheckPrefabFactory()
    {
        Container.BindFactory<CheckType, Check, Transform, GameObject, CheckPrefabFactory>()
            .FromMethod((container, type, check, parent) =>
            {
                var checkContainer = container.Resolve<CheckContainer>();

                CheckConfig config = type switch
                {
                    CheckType.BakedFish => checkContainer.BakedFish,
                    CheckType.BakedMeat => checkContainer.BakedMeat,
                    CheckType.BakedSalad => checkContainer.BakedSalad,
                    CheckType.FruitSalad => checkContainer.FruitSalad,
                    CheckType.CutletMedium => checkContainer.CutletMedium,
                    CheckType.WildBerryCocktail => checkContainer.WildBerryCocktail,
                    CheckType.FreshnessCocktail => checkContainer.FreshnessCocktail,
                    _ => throw new ArgumentOutOfRangeException()
                };
                GameObject instance = container.InstantiatePrefab(config.Prefab, parent);
                CheckUI checkUI = instance.GetComponent<CheckUI>();
                checkUI.Init(check);
                return instance;
            });
    }
}
