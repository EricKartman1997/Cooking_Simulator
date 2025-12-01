using UnityEngine;
using Zenject;

public class CheckFactory : Factory<CheckType, Check> { }

public class CheckPrefabFactory : Factory<CheckType, Check, Transform, GameObject> { }