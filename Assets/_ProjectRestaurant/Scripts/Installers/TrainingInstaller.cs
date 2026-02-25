using System;
using UnityEngine;
using Zenject;

public class TrainingInstaller : MonoInstaller
{
    [SerializeField] private FoodsForFurnitureContainer foodsForFurnitureContainer;
    [SerializeField] private RecipeContainer recipeContainer;
    [SerializeField] private CheckContainer checkContainer;
    [SerializeField] private BootstrapTraining bootstrapTraining;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<InputBlocker>().AsSingle();
        Container.BindInterfacesAndSelfTo<PauseHandler>().AsSingle();
        
        Container.Bind<FoodsForFurnitureContainer>().FromInstance(foodsForFurnitureContainer);
        Container.Bind<RecipeContainer>().FromInstance(recipeContainer);
        Container.Bind<CheckContainer>().FromInstance(checkContainer);
        
        Container.BindInterfacesAndSelfTo<BootstrapTraining>().FromInstance(bootstrapTraining).AsSingle();
        
        
        Container.BindInterfacesAndSelfTo<LoadReleaseGameplay>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoadReleaseTraining>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<SoundsServiceGameplay>().AsSingle();
        Container.BindInterfacesAndSelfTo<OutlineManager>().AsSingle();
        
        Container.Bind<FactoryPlayerGameplay>().AsSingle();
        Container.Bind<FactoryEnvironment>().AsSingle();
        Container.Bind<FactoryEnvironmentTraining>().AsSingle();
        Container.Bind<FactoryUIGameplay>().AsSingle();
        Container.Bind<FactoryUITraining>().AsSingle();
        Container.Bind<FactoryCamerasGameplay>().AsSingle();
        Container.Bind<NotificationFactory>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<NotificationManager>().AsSingle();

        Container.Bind<RecipeService>().AsSingle();
        
        Container.Bind<ProductsFactory>().AsSingle();
        Container.Bind<ViewFactory>().AsSingle();
        Container.Bind<HelperScriptFactory>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<TimeGameService>().AsSingle();
        Container.Bind<OrdersService>().AsSingle();
        Container.Bind<ScoreService>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameOverService>().AsSingle();
        Container.BindInterfacesAndSelfTo<Menu>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<ChecksManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<UpdateChecks>().AsSingle();
        
        Container.Bind<DialogueManager>().AsSingle();
        
        BindCheckFactory();
        BindCheckPrefabFactory();
    }
    
    private void BindCheckFactory()
    {
        Container.BindFactory<CheckType, Check, CheckFactory>()
            .FromMethod((container, type) =>
            {
                var checkContainer = container.Resolve<CheckContainer>();
                var checksManager = container.Resolve<ChecksManager>();
                var pauseHandler = container.Resolve<PauseHandler>();

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
                    CheckType.BakedFish        => new BakedFishCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager,pauseHandler),
                    CheckType.BakedMeat        => new BakedMeatCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager,pauseHandler),
                    CheckType.BakedSalad       => new BakedSaladCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager,pauseHandler),
                    CheckType.FruitSalad       => new FruitSaladCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager,pauseHandler),
                    CheckType.CutletMedium     => new CutletMediumCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager,pauseHandler),
                    CheckType.WildBerryCocktail => new WildBerryCocktailCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager,pauseHandler),
                    CheckType.FreshnessCocktail => new FreshnessCocktailCheck(config.Prefab, config.StartTime, config.Score, config.Dish, checksManager,pauseHandler),
                    _ => throw new ArgumentOutOfRangeException()
                };
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
