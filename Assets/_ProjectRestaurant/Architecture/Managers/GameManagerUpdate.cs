using System.Collections;
using UnityEngine;

public class GameManagerUpdate : MonoBehaviour
{
    private GameManager _gameManager;
    private bool _isInit;

    private IEnumerator Start()
    {
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }

        _isInit = true;
    }

    void Update()
    {
        if (_isInit == false)
        {
            return;
        }
        
        if (_gameManager.BootstrapLvl2.IsAllInit == true)
        {
            _gameManager.TimeGame.Update();
            _gameManager.UpdateChecks.Update();
        }
    }
}

