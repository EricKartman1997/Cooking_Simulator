using System.Collections.Generic;
using UnityEngine;

public class CheckContainer: MonoBehaviour
{
    [SerializeField] private InfoAboutCheck checkBakedMeat;
    [SerializeField] private InfoAboutCheck checkBakedFish;
    [SerializeField] private InfoAboutCheck checkFreshnessCocktail;
    [SerializeField] private InfoAboutCheck checkWildBerryCocktail;
    [SerializeField] private InfoAboutCheck checkFruitSalad;
    [SerializeField] private InfoAboutCheck checkMixBakedFruit;
    [SerializeField] private List<InfoAboutCheck> allPrefChecks;

    public InfoAboutCheck CheckBakedMeat => checkBakedMeat;
    public InfoAboutCheck CheckBakedFish => checkBakedFish;
    public InfoAboutCheck CheckFreshnessCocktail => checkFreshnessCocktail;
    public InfoAboutCheck CheckWildBerryCocktail => checkWildBerryCocktail;
    public InfoAboutCheck CheckFruitSalad => checkFruitSalad;
    public InfoAboutCheck CheckMixBakedFruit => checkMixBakedFruit;
    
    public List<InfoAboutCheck> AllPrefChecks => allPrefChecks;
}
