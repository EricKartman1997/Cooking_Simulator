using System;
using UnityEngine;

public static class EventBus //: IDisposable
{
    public static Action GameOver;
    public static Action AddOrder;
    public static Action<int,Check> AddScore;
    public static Action UpdateOrder;
    public static Action<Check> DeleteCheck;


    
    // public EventBus()
    // {
    //     Debug.Log("Создать объект: EventBus");
    //     _isInit = true;
    // }

    // public void Dispose()
    // {
    //     Debug.Log("У объекта вызван Dispose : EventBus");
    // }
}
