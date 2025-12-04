using UnityEngine;

public class BakedSaladCheck:Check
{
    public BakedSaladCheck(GameObject prefab, float startTime, float score, GameObject dish, IDeleteOverdueCheck deleteCheck,IHandlerPause pauseHandler)
        : base(prefab, startTime, score, dish, deleteCheck,pauseHandler)
    {
    }
    
    public override void Accept(ICheckVisitor checkVisitor) => checkVisitor.Visit(this);
}