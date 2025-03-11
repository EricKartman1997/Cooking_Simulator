using System.Collections.Generic;
using UnityEngine;

public class ObjectsAndRecipes: MonoBehaviour
{
    public Product Apple;
    public Product Orange;
    public GameObject Lime;
    public GameObject Blueberry;
    public GameObject Strawberry;
    public GameObject Cherry;
    
    public Product BakedApple;
    public Product BakedOrange;

    public GameObject FruitSalad;
    public GameObject MixBakedFruit;
    public GameObject WildBerryCocktail;
    public GameObject FreshnessCocktail;
    
    public Product Meat;
    public Product Fish;
    
    public Product BakedMeat;
    public Product BakedFish;
    
    public GameObject Rubbish;

    public InfoAboutCheck CheckBakedMeat;
    public InfoAboutCheck CheckBakedFish;
    public InfoAboutCheck CheckFreshnessCocktail;
    public InfoAboutCheck CheckWildBerryCocktail;
    public InfoAboutCheck CheckFruitSalad;
    public InfoAboutCheck CheckMixBakedFruit;

    public List<GameObject> RequiredFruitSalad; // рецепт на салат

    public List<GameObject> RequiredMixBakedFruit; // рецепт на салат


}
