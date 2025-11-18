using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class RecipeKey : IEquatable<RecipeKey>
{
    public List<IngredientName> Ingredients;

    public RecipeKey(List<Product> items)
    {
        Ingredients = items
            .Where(i => i != null)
            .Select(i => i.Name)
            .OrderBy(n => n)
            .ToList();
    }

    public RecipeKey(List<IngredientName> names)
    {
        Ingredients = names
            .OrderBy(n => n)
            .ToList();
    }

    public bool Equals(RecipeKey other)
    {
        if (other == null) return false;
        return Ingredients.SequenceEqual(other.Ingredients);
    }

    public override bool Equals(object obj) =>
        Equals(obj as RecipeKey);

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (var ingredient in Ingredients)
                hash = hash * 23 + ingredient.GetHashCode();

            return hash;
        }
    }
}

