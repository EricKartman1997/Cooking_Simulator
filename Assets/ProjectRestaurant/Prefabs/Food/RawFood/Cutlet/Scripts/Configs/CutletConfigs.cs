using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Food/Raw/Cutlet", fileName = "CutletConfig")]
public class CutletConfigs : ScriptableObject
{
    [field: Header("Current State")]
    [field: Header("Заполняйте поля текущего State и ниже")]
    [field: SerializeField] public EnumStateRoasting CurrentStateRoasting { get; private set; }
    
    [field: Header("Raw State Settings")]
    [field: SerializeField] public RawStateSettings RawStateSettings { get; private set; }
    
    [field: Header("Medium State Settings")]
    [field: SerializeField] public MediumStateSettings MediumStateSettings { get; private set; }
    
    [field: Header("Burn State Settings")]
    [field: SerializeField] public BurnStateSettings BurnStateSettings { get; private set; }

    
}

[Serializable]
public class RawStateSettings
{
    [field: SerializeField,Range(0,6)] public float RawTimeCooking { get; private set; }
    //[field: SerializeField,Range(0,6)] public float RawTimeRemaining { get; private set; }
    [field: SerializeField] public Material RawMaterial { get; private set; }
}

[Serializable]
public class MediumStateSettings
{
    [field: SerializeField,Range(0,6)] public float MediumTimeCooking { get; private set; }
    //[field: SerializeField,Range(0,6)] public float MediumTimeRemaining { get; private set; }
    [field: SerializeField] public Material MediumMaterial { get; private set; }
}

[Serializable]
public class BurnStateSettings
{
    [field: SerializeField,Range(0,6)] public float BurnTimeCooking { get; private set; }
    //[field: SerializeField,Range(0,6)] public float BurnTimeRemaining { get; private set; }
    [field: SerializeField] public Material BurnMaterial { get; private set; }
}
