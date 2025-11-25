using UnityEngine;

public class FruitSaladCheck: Check
{
    public FruitSaladCheck(GameObject prefab, float startTime, float score, GameObject dish, IDeleteCheck deleteCheck)
        : base(prefab, startTime, score, dish, deleteCheck)
    {
    }
    
    public override void Accept(ICheckVisitor checkVisitor) => checkVisitor.Visit(this);
}