using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RecipeContainerConfig
{
    //public enum StationType { Oven, CuttingTable, Blender, Suvide, Stove, }
    
    [SerializeField] private StationType station;
    [SerializeField] private List<Product> ingredients;
    [SerializeField] private Product dish;
    [SerializeField] private float cookingTime;

    public StationType Station => station;
    public List<Product> ListIngredients => ingredients;
    public Product Dish => dish;
    public float CookingTime => cookingTime;
}
