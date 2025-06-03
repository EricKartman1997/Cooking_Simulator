using System;
using UnityEngine;

[Serializable]
public class MoveConfig
{
    [field: SerializeField,Range(0,10)] public float MoveSpeed {get; private set;}
    [field: SerializeField, Range(0, 25)] public float  RotateSpeed {get; private set;}
    [field: SerializeField, Range(0, 10)] public float  Distance {get; private set;}
    //[field: SerializeField, Range(0, 10)] public float  Height {get; private set;}
}
