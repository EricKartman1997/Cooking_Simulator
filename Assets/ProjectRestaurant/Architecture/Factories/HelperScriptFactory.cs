using UnityEngine;

public class HelperScriptFactory
{
    // Stove
    public StovePoints GetStovePoint(Transform positionRawFood, Transform parentRawFood, Transform positionReadyFood, Transform parentReadyFood)
    {
        StovePoints stovePoints = new StovePoints(positionRawFood, parentRawFood, positionReadyFood, parentReadyFood);
        return stovePoints;
    }
    
    public StoveView GetStoveView()
    {
        StoveView stoveView = new StoveView();
        return stoveView;
    }
}
