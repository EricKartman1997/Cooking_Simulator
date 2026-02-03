using UnityEngine;

public class HelperScriptFactory
{
    // Stove
    public StovePoints GetStovePoints(Transform positionRawFood)
    {
        StovePoints stovePoints = new StovePoints(positionRawFood);
        return stovePoints;
    }
    
    public StoveView GetStoveView()
    {
        StoveView stoveView = new StoveView();
        return stoveView;
    }

}
