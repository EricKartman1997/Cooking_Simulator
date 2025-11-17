using UnityEngine;

[CreateAssetMenu(menuName = "Configs/DecorationTableConfig", fileName = "DecorationTableConfig")]
public class DecorationTableConfig : ScriptableObject
{ 
    [field: SerializeField] public EnumDecorationTableTop DecorationTableTop { get; private set; }
    [field: SerializeField] public EnumDecorationLowerSurface DecorationLowerSurface { get; private set; }
}
//TODO удалить
