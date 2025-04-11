using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Timer/Timer", fileName = "TimerConfig")]
public class ConfigTimer : ScriptableObject
{
    [field: SerializeField] public Sprite CirclePref { get; private set; }
    [field: SerializeField] public Sprite ArrowPref { get; private set; }
    [field: SerializeField] public float LifeTime { get; private set; }
}
