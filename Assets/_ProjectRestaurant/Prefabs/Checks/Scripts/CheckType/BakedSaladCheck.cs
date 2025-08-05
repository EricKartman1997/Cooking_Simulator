using UnityEngine;

public class BakedSaladCheck:Check
{
    public BakedSaladCheck(GameObject prefab, float startTime, float score, GameObject dish) : base(prefab, startTime, score, dish)
    {
    }
    
    public override void Accept(ICheckVisitor checkVisitor) => checkVisitor.Visit(this);
}