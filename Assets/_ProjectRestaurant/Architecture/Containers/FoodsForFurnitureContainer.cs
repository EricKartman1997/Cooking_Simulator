using UnityEngine;

[CreateAssetMenu(fileName = "ListFoodsForFurniture", menuName = "Container/ListFoodsForFurniture")]
public class FoodsForFurnitureContainer : ScriptableObject
{
    [SerializeField] private FoodsForFurnitureConfig getTable,
        giveTable,
        oven,
        cuttingTable,
        distribution,
        suvide,
        blender,
        garbage,
        stove;

    public FoodsForFurnitureConfig GetTable => getTable;

    public FoodsForFurnitureConfig GiveTable => giveTable;

    public FoodsForFurnitureConfig Oven => oven;

    public FoodsForFurnitureConfig CuttingTable => cuttingTable;

    public FoodsForFurnitureConfig Distribution => distribution;

    public FoodsForFurnitureConfig Suvide => suvide;

    public FoodsForFurnitureConfig Blender => blender;

    public FoodsForFurnitureConfig Garbage => garbage;

    public FoodsForFurnitureConfig Stove => stove;
}
