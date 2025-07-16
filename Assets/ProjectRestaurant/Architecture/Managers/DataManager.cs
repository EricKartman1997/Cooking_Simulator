using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : IDisposable
{
    private GameManager _gameManager;

    //public bool _isInitLevel; // ПОМЕНЯТЬ
    
    private float _score;
    private float[] _timeLevel;
    private int _orders;
    private int _level;
    private string _name;

    //public bool IsInitLevel => _isInitLevel;

    public DataManager(GameManager gameManager)
    {
        _gameManager = gameManager;
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
