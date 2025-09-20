using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : IDisposable
{
    private GameManager _gameManager;
    
    private float _score;
    private float[] _timeLevel;
    private int _orders;
    private int _level;
    private string _name;

    private bool _isInit;
    
    public bool IsInit => _isInit;

    public DataManager(GameManager gameManager)
    {
        _gameManager = gameManager;

        Debug.Log("Создать объект: DataManager");
        _isInit = true;
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : GameManager");
    }

    private void AllUpdate()
    {
        _score = _gameManager.Score.ScorePlayer;
        _timeLevel = _gameManager.TimeGame.TimeLevel;
        // _orders = ;
        // _level = ;
        // _name = ;

    }
}
