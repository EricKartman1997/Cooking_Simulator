using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Furniture/GetTableConfig", fileName = "GetTableConfig")]
public class GetTableConfig : ScriptableObject
{ 
    [field: SerializeField] public IngredientName GiveFood { get; private set; }
    [field: SerializeField] public ViewDishName FoodView { get; private set; }
}
