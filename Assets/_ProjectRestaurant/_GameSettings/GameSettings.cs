using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
public class GameSettings : SerializedScriptableObject
{
    [SerializeField, Range(0, 59)]
    private int _minutes;

    [SerializeField, Range(0, 59)]
    private int _seconds;

    [SerializeField, Range(1,10)]
    private byte _order;

    [OdinSerialize, DictionaryDrawerSettings(KeyLabel = "CheckType", ValueLabel = "Enabled")]
    private Dictionary<CheckType, bool> _checks = new Dictionary<CheckType, bool>();


    public int Minutes => _minutes;
    public int Seconds => _seconds;
    public byte Order => _order;
    public IReadOnlyDictionary<CheckType, bool> Checks => _checks;


    public HashSet<CheckType> FromDicToHashSet()
    {
        HashSet<CheckType> set = new HashSet<CheckType>();

        foreach (var kvp in _checks)
        {
            if (kvp.Value)
                set.Add(kvp.Key);
        }

        return set;
    }
}