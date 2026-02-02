using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChecksPanalUI : MonoBehaviour,IPause
{
    [SerializeField] private GameObject content;
    
    private Dictionary<Check,GameObject> _dictionaryChecks = new Dictionary<Check, GameObject>();
    private IHandlerPause _handlerPause;

    [Inject]
    public void ConstructZenject(IHandlerPause handlerPause)
    {
        _handlerPause = handlerPause;
        _handlerPause.Add(this);
    }

    private void OnDestroy()
    {
        _handlerPause.Remove(this);
    }

    public void AddCheck(Check check, CheckPrefabFactory checksFactory, CheckType type)
    {
        // Проверка 1: null-объект
        if (check == null)
        {
            throw new ArgumentNullException(nameof(check), "Check cannot be null");
        }

        // Проверка 2: дублирование ключа
        if (_dictionaryChecks.ContainsKey(check))
        {
            throw new ArgumentException($"Check '{check}' already exists in the dictionary", nameof(check));
        }

        // Проверка 3: фабрика должна быть валидной
        if (checksFactory == null)
        {
            throw new ArgumentNullException(nameof(checksFactory), "ChecksFactory cannot be null");
        }

        // Проверка 4: контент должен быть установлен
        if (content == null)
        {
            throw new InvalidOperationException("Content reference is not set in inspector");
        }

        GameObject checkPrefab = checksFactory.Create(type, check, content.transform);
        _dictionaryChecks.Add(check, checkPrefab);
    }
    
    public void RemoveCheck(Check check)
    {
        if (_dictionaryChecks == null) return;

        if (_dictionaryChecks.TryGetValue(check, out GameObject checkObject))
        {
            // Удаляем объект со сцены
            if (checkObject != null)
            {
                Destroy(checkObject);
            }
            else
            {
                throw new ArgumentNullException("Чек отсутствует GameObject");
            }
            
            // Удаляем запись из словаря
            _dictionaryChecks.Remove(check);
        }
        else
        {
            Debug.LogWarning($"Check {check} not found in dictionary");
        }
    }

    private void HideChecks()
    {
        content.SetActive(false);
    }
    
    private void ShowChecks()
    {
        content.SetActive(true);
    }

    public void SetPause(bool isPaused)
    {
        if (isPaused == true)
        {
            HideChecks();
        }
        else
        {
            ShowChecks();
        }
    }
}
