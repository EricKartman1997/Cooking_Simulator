public class ScoreService
{
    private TimeGameService _timeGameService;
    private float _score;
    private ScoreCheckVisitore _checkVisitore;
    private CheckContainer _checkContainer;
    
    public float ScorePlayer => _checkVisitore.Score ;

    public ScoreService(TimeGameService timeGameService,CheckContainer checkContainer)
    {
        _timeGameService = timeGameService;
        _checkContainer = checkContainer;
        
        _checkVisitore = new ScoreCheckVisitore(_checkContainer);
    }
    
    public void AddScore(int score, Check check)
    {
        check.Accept(_checkVisitore);
        _checkVisitore.Score += score + AdditionalScore();
    }
    private float AdditionalScore()
    {
        var remSeconds = _timeGameService.TimeLevel[0] - _timeGameService.CurrentSeconds;
        var remMinutes = _timeGameService.TimeLevel[1] - _timeGameService.CurrentMinutes;
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


