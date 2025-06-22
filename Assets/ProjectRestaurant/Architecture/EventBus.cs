using System;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static Action GameOver;
    public static Action AddOrder;
    public static Action<int,float> AddScore;
    public static Action UpdateOrder;
    public static Action PressE;
    public static Action<InfoAboutCheck> DeleteCheck;
}
