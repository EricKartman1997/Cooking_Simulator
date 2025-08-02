using System.Collections.Generic;
using UnityEngine;

public class CheckContainer: MonoBehaviour
{
    [SerializeField] private Check checkBakedMeat;
    [SerializeField] private Check checkBakedFish;
    [SerializeField] private Check checkFreshnessCocktail;
    [SerializeField] private Check checkWildBerryCocktail;
    [SerializeField] private Check checkFruitSalad;
    [SerializeField] private Check checkMixBakedFruit;
    [SerializeField] private List<Check> allPrefChecks;

    public Check CheckBakedMeat => checkBakedMeat;
    public Check CheckBakedFish => checkBakedFish;
    public Check CheckFreshnessCocktail => checkFreshnessCocktail;
    public Check CheckWildBerryCocktail => checkWildBerryCocktail;
    public Check CheckFruitSalad => checkFruitSalad;
    public Check CheckMixBakedFruit => checkMixBakedFruit;
    
    public List<Check> AllPrefChecks => allPrefChecks;
}
