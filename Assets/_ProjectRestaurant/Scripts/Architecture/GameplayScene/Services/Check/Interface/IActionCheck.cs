using System;

public interface IActionCheck
{
        event Action<Check, CheckPrefabFactory, CheckType> AddCheckAction;
        event Action<Check> RemoveCheckAction;
}