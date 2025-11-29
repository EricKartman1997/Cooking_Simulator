using UnityEngine;

public class FreshnessCocktailCheck: Check
{
    public FreshnessCocktailCheck(GameObject prefab, float startTime, float score, GameObject dish, IDeleteOverdueCheck deleteCheck)
        : base(prefab, startTime, score, dish, deleteCheck)
    {
    }
    
    public override void Accept(ICheckVisitor checkVisitor) => checkVisitor.Visit(this);
}