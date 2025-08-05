using UnityEngine;

public class BakedFishCheck: Check
{
    public BakedFishCheck(GameObject prefab, float startTime, float score, GameObject dish) : base(prefab, startTime, score, dish)
    {
    }

    public override void Accept(ICheckVisitor checkVisitor) => checkVisitor.Visit(this);

}