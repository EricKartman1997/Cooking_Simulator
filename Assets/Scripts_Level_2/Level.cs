using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static float[] TimeLevel;
    [SerializeField] private float _seconds = 0f;
    [SerializeField] private float _minutes = 0f;

    public byte RemainingOrders = 0;
    
    public static byte NumberOrdersInLevel;


    private void OnEnable()
    {
        _seconds = Random.Range(45, 60);
        _minutes = Random.Range(1, 2);
        NumberOrdersInLevel = (byte)Random.Range(3, 5);
        TimeLevel = new float[]{_seconds,_minutes};
    }
    public byte RemainingOrdersNow()
    {
        RemainingOrders = (byte)Mathf.Abs(Heroik.MakeOrders - NumberOrdersInLevel);
        return RemainingOrders ;
    }
    
}
