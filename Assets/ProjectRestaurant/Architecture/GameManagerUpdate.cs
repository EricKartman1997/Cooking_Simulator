using UnityEngine;

public class GameManagerUpdate : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = StaticManagerWithoutZenject.GameManager;
    }

    void Update()
    {
        _gameManager.TimeGame.Update();
        _gameManager.UpdateChecks.Update();
    }
}
