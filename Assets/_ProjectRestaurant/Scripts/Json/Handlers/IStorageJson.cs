using System;

public interface IStorageJson
{
    void Save(string key, object data, Action<bool> callback = null);
    void Load<T>(string key,Action<T> callback) where T : new();
}
