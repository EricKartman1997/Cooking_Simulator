using UnityEngine;

public class HelperScriptFactory
{
    // Stove
    public StovePoints GetStovePoint(Transform positionRawFood, Transform parentRawFood)
    {
        StovePoints stovePoints = new StovePoints(positionRawFood, parentRawFood);
        return stovePoints;
    }
    
    public StoveView GetStoveView()
    {
        StoveView stoveView = new StoveView();
        return stoveView;
    }
}
