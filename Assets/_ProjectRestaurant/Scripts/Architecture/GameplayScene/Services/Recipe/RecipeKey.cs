using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class RecipeKey : IEquatable<RecipeKey>
{
    public List<ProductType> ProductTypes { get; }
    
    public RecipeKey(List<Product> ingredients)
    {
        // Извлекаем ТИПЫ продуктов и сортируем
        ProductTypes = ingredients
            .Where(i => i != null)
            .Select(i => i.Type) // Используем enum или SO-идентификатор
            .OrderBy(t => t)
            .ToList();
    }
    
    public bool Equals(RecipeKey other)
    {
        if (other is null) return false;
        return ProductTypes.SequenceEqual(other.ProductTypes);
    }
    
    public override bool Equals(object obj) => Equals(obj as RecipeKey);
    
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 19;
            foreach (var type in ProductTypes)
            {
                hash = hash * 31 + type.GetHashCode();
            }
            return hash;
        }
    }
}
