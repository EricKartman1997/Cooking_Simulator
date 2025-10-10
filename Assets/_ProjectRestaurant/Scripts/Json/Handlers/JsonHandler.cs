using System;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonHandler : IDisposable, IStorageJson
{
    public void Dispose()
    {
        Debug.Log("Dispose JsonHandler");
    }

    public void Save(string key, object data, Action<bool> callback = null)
    {
        string path = BuildPath(key);
        string json = JsonConvert.SerializeObject(data);

        using (var fileStream = new StreamWriter(path))
        {
            fileStream.Write(json);
        }
        
        callback?.Invoke(true);
    }

    public void Load<T>(string key, Action<T> callback) where T : new()
    {
        string path = BuildPath(key);
        StreamReader filestream = null;

        try
        {
            if (!File.Exists(path))
            {
                // Создаём новый объект с конструктором по умолчанию
                var defaultData = new T();

                // Сохраняем его как новый JSON-файл
                var defaultJson = JsonConvert.SerializeObject(defaultData, Formatting.Indented);
                File.WriteAllText(path, defaultJson);

                Debug.Log($"Файл не найден, создан новый: {path}");
                callback?.Invoke(defaultData);
                return;
            }

            // Если файл существует — читаем и десериализуем
            filestream = new StreamReader(path);
            var json = filestream.ReadToEnd();
            var data = JsonConvert.DeserializeObject<T>(json);

            callback?.Invoke(data);
        }
        catch (JsonException ex)
        {
            Debug.LogError($"Ошибка десериализации JSON: {ex.Message}");
            callback?.Invoke(new T());
        }
        catch (IOException ex)
        {
            Debug.LogError($"Ошибка ввода-вывода при чтении файла: {path}. Ошибка: {ex.Message}");
            callback?.Invoke(new T());
        }
        catch (Exception ex)
        {
            Debug.LogError($"Неожиданная ошибка при загрузке данных: {ex.Message}");
            callback?.Invoke(new T());
        }
        finally
        {
            filestream?.Dispose();
        }
    }
    
    private string BuildPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key + ".json");
    }
}

public class GraphicSettings
{
    public bool IsFullScreen;
    public int QualityLevel;
    public int ResolutionSize;
    
    public GraphicSettings()
    {
        // Значения по умолчанию
        IsFullScreen = true;
        QualityLevel = 1;
        ResolutionSize = 0;
    }
}
