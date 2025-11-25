public interface ICheckVisitor
{
    void Visit(BakedFishCheck bakedFish);
    void Visit(BakedMeatCheck bakedMeat);
    void Visit(BakedSaladCheck bakedSalad);
    void Visit(FruitSaladCheck fruitSalad);
    void Visit(CutletMediumCheck cutletMedium);
    void Visit(WildBerryCocktailCheck wildBerryCocktail);
    void Visit(FreshnessCocktailCheck freshnessCocktail);
}
