using UnityEngine;
using Zenject;

public class CheckFactory : PlaceholderFactory<CheckType, Check> { }

public class CheckPrefabFactory : PlaceholderFactory<CheckType, Check, Transform, GameObject> { }