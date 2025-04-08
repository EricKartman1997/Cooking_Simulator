using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperScriptFactory
{
    // Stove
    public StovePoints GetStovePoint()
    {
        StovePoints stovePoints = new StovePoints();
        return stovePoints;
    }
    
    public StoveView GetStoveView()
    {
        StoveView stoveView = new StoveView();
        return stoveView;
    }
}
