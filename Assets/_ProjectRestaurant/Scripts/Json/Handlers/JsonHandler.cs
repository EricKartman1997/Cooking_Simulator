using System;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonHandler : IDisposable, IStorageJson, ISaveReadGraphicSettings
{
    // private GraphicSettings _jsonGraphicSettingsFile;
    //
    // public GraphicSettings ReadOnlyGraphicSettings => _jsonGraphicSettingsFile;

    public JsonHandler()
    {

        // Проверяем существование файла
        if (File.Exists(BuildPath(JsonPathName.GRAPHIC_SETTINGS_PATH)))
        {
            // Файл существует - загружаем данные
            Load<GraphicSettings>(JsonPathName.GRAPHIC_SETTINGS_PATH, data =>
            {
                _jsonGraphicSettingsFile = new GraphicSettings(data.IsFullScreen, data.QualityLevel, data.ResolutionSize);
            
                Debug.Log($"QL = {data.QualityLevel}, FS = {data.IsFullScreen}, RS = {data.ResolutionSize}");
                Debug.Log("Считал данные из файла");
            });
        }
        else
        {
            // Файла нет - создаем дефолтные настройки и сохраняем
            Debug.Log("Файл не найден, создаю дефолтные настройки");
        
            // Создаем дефолтный объект
            _jsonGraphicSettingsFile = new GraphicSettings(); // Пример дефолтных значений
        
            // Сохраняем дефолтные настройки
            Save(JsonPathName.GRAPHIC_SETTINGS_PATH, _jsonGraphicSettingsFile);
        
            Debug.Log($"Создал дефолтные настройки: QL = {_jsonGraphicSettingsFile.QualityLevel}, FS = {_jsonGraphicSettingsFile.IsFullScreen}, RS = {_jsonGraphicSettingsFile.ResolutionSize}");
        }
    }

    public void Dispose()
    {
        Save(JsonPathName.GRAPHIC_SETTINGS_PATH,_jsonGraphicSettingsFile);
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

    public void Load<T>(string key, Action<T> callback)
    {
        string path = BuildPath(key);
        StreamReader filestream = null;
    
        try
        {
            filestream = new StreamReader(path);
            var json = filestream.ReadToEnd();
            var data = JsonConvert.DeserializeObject<T>(json);
        
            callback?.Invoke(data);
        }
        catch (FileNotFoundException ex)
        {
            Debug.LogError($"Файл не найден: {path}. Ошибка: {ex.Message}");
            callback?.Invoke(default(T));
        }
        catch (DirectoryNotFoundException ex)
        {
            Debug.LogError($"Директория не найдена: {path}. Ошибка: {ex.Message}");
            callback?.Invoke(default(T));
        }
        catch (IOException ex)
        {
            Debug.LogError($"Ошибка ввода-вывода при чтении файла: {path}. Ошибка: {ex.Message}");
            callback?.Invoke(default(T));
        }
        catch (JsonException ex)
        {
            Debug.LogError($"Ошибка десериализации JSON: {ex.Message}");
            callback?.Invoke(default(T));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Неожиданная ошибка при загрузке данных: {ex.Message}");
            callback?.Invoke(default(T));
        }
        finally
        {
            filestream?.Dispose();
        }
    }

    public void SaveGraphicSettings(bool fullScreen, int qualityIndex, int resolutionIndex)
    {
        _jsonGraphicSettingsFile.IsFullScreen = fullScreen;
        _jsonGraphicSettingsFile.QualityLevel = qualityIndex;
        _jsonGraphicSettingsFile.ResolutionSize = resolutionIndex;
        Save(JsonPathName.GRAPHIC_SETTINGS_PATH,_jsonGraphicSettingsFile,null);
    }

    private string BuildPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key);
    }
}

public class GraphicSettings
{
    public bool IsFullScreen;
    public int QualityLevel;
    public int ResolutionSize;

    public GraphicSettings(bool isFullScreen, int qualityLevel, int resolutionSize)
    {
        IsFullScreen = isFullScreen;
        QualityLevel = qualityLevel;
        ResolutionSize = resolutionSize;
    }

    public GraphicSettings()
    {
        // Значения по умолчанию
        IsFullScreen = true;
        QualityLevel = 1;
        ResolutionSize = 0;
    }
}
