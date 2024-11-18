using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColliders : MonoBehaviour
{
    public Collider ColliderA;
    public Collider ColliderB;

    private void Start()
    {
        Physics.IgnoreCollision(ColliderA, ColliderB);
        Debug.Log("Отработал");
    }
}
