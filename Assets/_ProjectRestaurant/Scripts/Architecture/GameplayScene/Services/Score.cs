using System;
using UnityEngine;

public class Score : IDisposable
{
    private TimeGame _timeGame;
    private float _score;
    private ScoreCheckVisitore _checkVisitore;
    private CheckContainer _checkContainer;
    
    public float ScorePlayer => _checkVisitore.Score ;

    public Score(TimeGame timeGame,CheckContainer checkContainer)
    {
        _timeGame = timeGame;
        _checkContainer = checkContainer;
        
        EventBus.AddScore += AddScore;
        
        _checkVisitore = new ScoreCheckVisitore(_checkContainer); // создать медиатор
        
    }

    public void Dispose()
    {
        EventBus.AddScore -= AddScore;
        Debug.Log("У объекта вызван Dispose : Score");
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
        private CheckContainer _checkContainer;

        public ScoreCheckVisitore(CheckContainer checkContainer)
        {
            _checkContainer = checkContainer;
            Debug.Log("Создать объект: ScoreCheckVisitore");
        }

        public void Visit(BakedFishCheck bakedFish)
        {
            Score += _checkContainer.BakedFish.Score;
        }

        public void Visit(BakedMeatCheck bakedMeat)
        {
            Score += _checkContainer.BakedMeat.Score;
        }

        public void Visit(BakedSaladCheck bakedSalad)
        {
            Score += _checkContainer.BakedSalad.Score;
        }

        public void Visit(FruitSaladCheck fruitSalad)
        {
            Score += _checkContainer.FruitSalad.Score;
        }

        public void Visit(CutletMediumCheck cutletMedium)
        {
            Score += _checkContainer.CutletMedium.Score;
        }

        public void Visit(WildBerryCocktailCheck wildBerryCocktail)
        {
            Score += _checkContainer.WildBerryCocktail.Score;
        }

        public void Visit(FreshnessCocktailCheck freshnessCocktail)
        {
            Score += _checkContainer.FreshnessCocktail.Score;
        }
    }
    
}


