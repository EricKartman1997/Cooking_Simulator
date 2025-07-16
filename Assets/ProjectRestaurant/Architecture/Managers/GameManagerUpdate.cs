using UnityEngine;

public class GameManagerUpdate : MonoBehaviour
{
    private GameManager _gameManager;
    public bool IsWork;// переделать
    
    private void Start()
    {
        _gameManager = StaticManagerWithoutZenject.GameManager;
    }

    void Update()
    {
        if (IsWork == true)
        {
            _gameManager.TimeGame.Update();
            _gameManager.UpdateChecks.Update();
        }
    }
}
