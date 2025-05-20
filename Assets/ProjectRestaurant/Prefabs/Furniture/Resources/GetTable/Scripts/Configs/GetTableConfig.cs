using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Furniture/GetTableConfig", fileName = "GetTableConfig")]
public class GetTableConfig : ScriptableObject
{ 
    [field: SerializeField] public EnumGiveFood GiveFood { get; private set; }
    [field: SerializeField] public EnumViewFood FoodView { get; private set; }
}
