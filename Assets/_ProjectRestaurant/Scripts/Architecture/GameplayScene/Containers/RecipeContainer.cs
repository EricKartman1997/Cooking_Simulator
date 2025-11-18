using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeContainer", menuName = "Container/RecipeContainer")]
public class RecipeContainer : ScriptableObject
{
    [SerializeField] private List<RecipeContainerConfig> recipes;

    public List<RecipeContainerConfig> Recipes => recipes;
}