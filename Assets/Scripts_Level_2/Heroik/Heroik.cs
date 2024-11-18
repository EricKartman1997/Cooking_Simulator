using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Heroik : MonoBehaviour
{
    [SerializeField] private LightAttack lightAttack;
    [SerializeField] private ListEnemy listEnemy;
    
    public GameObject _curentTakenObjects = null;// переделать
    public static bool IsBusyHands = false; // руки не заняты
    public static byte MakeOrders = 0;
    public GameObject[] TakenObjects;

    private void Start()
    {
        foreach (var Obj in TakenObjects)
        {
            Obj.SetActive(false);
        }
    }
    public void ActiveObjHands(GameObject objTable)
    {
        foreach (var Obj in TakenObjects)
        {
            if (Obj.name == objTable.name)
            {
                Obj.SetActive(true);
                _curentTakenObjects = Obj;
                IsBusyHands = true;
            }
        }
    }
    public GameObject GiveObjHands()
    {
        _curentTakenObjects.SetActive(false);
        IsBusyHands = false;
        return _curentTakenObjects;
        // не забывай прописывать _curentTakenObjects = null;
    }


    
}
