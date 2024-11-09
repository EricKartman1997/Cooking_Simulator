using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string IsMerge(Interactable interactableObject)
    {
        switch (gameObject.name)
        {
            case "Apple":
                if (interactableObject.name == "Orange")
                {
                    return "FruitSalad";
                }
                break;
            case "Orange":
                if (interactableObject.name == "Apple")
                {
                    return "FruitSalad";
                }
                break;
            case "BakedOrange":
                if (interactableObject.name == "BakedApple")
                {
                    return "MixBakedFruit";
                }
                break;
            case "BakedApple":
                if (interactableObject.name == "BakedOrange")
                {
                    return "MixBakedFruit";
                }
                break;
            default:
                return "None";
        }
        //Debug.Log("Ошибка свича");
        return "None";
    }
}
