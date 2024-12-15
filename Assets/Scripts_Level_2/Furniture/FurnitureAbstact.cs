using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FurnitureAbstact : MonoBehaviour
{
    protected abstract GameObject GiveObj(ref GameObject obj);                // отдать объект
    //protected abstract GameObject GiveObj();                            // отдать объект
    protected abstract void AcceptObject(GameObject obj, byte numberObj); // принять объект
    protected abstract void CreateResult(string nameBolud);               // создать объект
    protected abstract void TurnOn();                                     // включить
    protected abstract void TurnOff();                                    // выключить
}
