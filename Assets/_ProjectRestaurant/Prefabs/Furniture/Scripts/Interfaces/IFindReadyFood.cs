using System.Collections.Generic;
using UnityEngine;

public interface IFindReadyFood
{
    // возвращение определенного готового блюда
    public GameObject FindReadyFood();
    // подходит ли список ингредиентов к рецепту блюда
    public bool SuitableIngredients(List<GameObject> currentFruits, List<GameObject> requiredFruits);
}
