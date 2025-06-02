using UnityEngine;

public class HelperScriptFactory
{
    // Blender
    public BlenderPoints GetBlenderPoints(Transform firstPoint,Transform secondPoint,Transform thirdPoint,Transform parentFood, Transform parentReadyFood)
    {
        BlenderPoints blenderPoints = new BlenderPoints(firstPoint,secondPoint,thirdPoint,parentFood, parentReadyFood);
        return blenderPoints;
    }
    
    public BlenderView GetBlenderView(GameObject timer, Transform timerPoint, Transform timerParent, Animator animator)
    {
        BlenderView blenderView = new BlenderView(timer, timerPoint, timerParent,animator);
        return blenderView;
    }
    
    // CuttingTable
    public CuttingTablePoints GetCuttingTablePoints(Transform positionIngredient1, Transform positionIngredient2, Transform positionResult)
    {
        CuttingTablePoints cuttingTablePoints = new CuttingTablePoints(positionIngredient1,positionIngredient2,positionResult);
        return cuttingTablePoints;
    }
    
    public CuttingTableView GetCuttingTableView(Animator animator, NewTimer timer)
    {
        CuttingTableView cuttingTableView = new CuttingTableView(animator, timer);
        return cuttingTableView;
    }
    
    //Oven
    public OvenView GetOvenView(GameObject switchFirst, GameObject switchSecond, NewTimer timer,Animator animator)
    {
        OvenView ovenView = new OvenView(switchFirst,switchSecond,timer,animator);
        return ovenView;
    }
    
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
    
    //Suvide
    public SuvidePoints GetSuvidePoints(Transform firstPointIngredient, Transform secondPointIngredient, Transform thirdPointIngredient, Transform firstPointResult, Transform secondPointResult, Transform thirdPointResult)
    {
        SuvidePoints cuttingTablePoints = new SuvidePoints(firstPointIngredient,secondPointIngredient,thirdPointIngredient,firstPointResult,secondPointResult,thirdPointResult);
        return cuttingTablePoints;
    }
    
    public SuvideView GetSuvideView(GameObject waterPrefab, GameObject switchTimePrefab, GameObject switchTemperPrefab, NewTimer firstTimer, NewTimer secondTimer, NewTimer thirdTimer, Animator animator)
    {
        SuvideView suvideView = new SuvideView(waterPrefab, switchTimePrefab, switchTemperPrefab,firstTimer,secondTimer,thirdTimer,animator);
        return suvideView;
    }
}
