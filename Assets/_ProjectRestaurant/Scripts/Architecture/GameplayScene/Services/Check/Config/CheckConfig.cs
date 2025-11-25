using System;
using UnityEngine;

[Serializable]
public class CheckConfig
{
    [SerializeField] private CheckUI prefab;
    [SerializeField, Range(3,50)] private float startTime;
    [SerializeField, Range(10,150)] private float score;
    [SerializeField] private GameObject dish;

    public GameObject Prefab => prefab.gameObject;

    public float StartTime => startTime;

    public float Score => score;

    public GameObject Dish => dish;
}