using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FurnitureAbstact : MonoBehaviour
{
    public abstract GameObject GiveObj();// отдать объект
    public abstract void AcceptObject(); // принять объект
    public abstract void CreateResult(); // создать объект
    public abstract void TurnOn();       // включить
    public abstract void TurnOff();      // выключить
}
