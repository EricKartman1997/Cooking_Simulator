using System;
using System.Collections;
using UnityEngine;

public class Score : IDisposable
{
    private GameManager _gameManager;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    private TimeGame _timeGame;
    private float _score;
    private bool _isInit;
    private ScoreCheckVisitore _checkVisitore;
    private ChecksFactory _checksFactory;
    
    public bool IsInit => _isInit;
    // public float ScorePlayer => _score => _checkVisitore.Score;
    public float ScorePlayer => _checkVisitore.Score ;

    public Score(CoroutineMonoBehaviour coroutineMonoBehaviour)
    {
        _coroutineMonoBehaviour = coroutineMonoBehaviour;
        
        _coroutineMonoBehaviour.StartCoroutine(Init());
    }

    public void Dispose()
    {
        EventBus.AddScore -= AddScore;
        Debug.Log("У объекта вызван Dispose : Score");
    }

    private IEnumerator Init()
    {
        EventBus.AddScore += AddScore;
        
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        
        while (_timeGame == null)
        {
            _timeGame = _gameManager.TimeGame;
            yield return null;
        }
        
        while (_checksFactory == null)
        {
            _checksFactory = _gameManager.ChecksFactory;
            yield return null;
        }
        
        _checkVisitore = new ScoreCheckVisitore(_checksFactory); // создать медиатор
        
        Debug.Log("Создать объект: Score");
        _isInit = true;
    }

    // public void AddScore(int score)
    // {
    //     _score += score + AdditionalScore();
    // }
    
    private void AddScore(int score, Check check)
    {
        check.Accept(_checkVisitore);
        _checkVisitore.Score += score + AdditionalScore();
    }
    private float AdditionalScore()
    {
        var remSeconds = _timeGame.TimeLevel[0] - _timeGame.CurrentSeconds;
        var remMinutes = _timeGame.TimeLevel[1] - _timeGame.CurrentMinutes;
        var multiplyMinutes = remMinutes * 60;
        var result = multiplyMinutes + remSeconds;
        return result;
    }
    
    private class ScoreCheckVisitore: ICheckVisitor
    {
        public float Score;
        private ChecksFactory _checksFactory;

        public ScoreCheckVisitore(ChecksFactory checksFactory)
        {
            _checksFactory = checksFactory;
            Debug.Log("Создать объект: ScoreCheckVisitore");
        }

        public void Visit(BakedFishCheck bakedFish)
        {
            Score += _checksFactory.BakedFish;
        }

        public void Visit(BakedMeatCheck bakedMeat)
        {
            Score += _checksFactory.BakedMeat;
        }

        public void Visit(BakedSaladCheck bakedSalad)
        {
            Score += _checksFactory.BakedSalad;
        }

        public void Visit(FruitSaladCheck fruitSalad)
        {
            Score += _checksFactory.FruitSalad;
        }

        public void Visit(CutletMediumCheck cutletMedium)
        {
            Score += _checksFactory.CutletMedium;
        }

        public void Visit(WildBerryCocktailCheck wildBerryCocktail)
        {
            Score += _checksFactory.WildBerryCocktail;
        }

        public void Visit(FreshnessCocktailCheck freshnessCocktail)
        {
            Score += _checksFactory.FreshnessCocktail;
        }
    }
    
}


