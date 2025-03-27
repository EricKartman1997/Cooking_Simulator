using UnityEngine;

[CreateAssetMenu(menuName = "Configs/HeroikConfig", fileName = "HeroikConfig")]
public class HeroikConfig : ScriptableObject
{
    [field: SerializeField] public MoveConfig MoveConfig { get; private set; }
}
