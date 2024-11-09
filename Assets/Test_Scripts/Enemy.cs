using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LightAttack lightAttack;
    [SerializeField] private ListEnemy listEnemy;
    private void OnDestroy()
    {
        listEnemy.TakeEnemiesArray();
        //lightAttack.ChangeColorShere(lightAttack.attackMaterial);
    }
}
