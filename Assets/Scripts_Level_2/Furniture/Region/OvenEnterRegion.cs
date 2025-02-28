using System;
using System.Collections.Generic;
using UnityEngine;

public class OvenEnterRegion : MonoBehaviour
{
    [SerializeField] private GameObject glassOn;
    [SerializeField] private GameObject glassOff;
    [SerializeField] private GameObject switchFirst;
    [SerializeField] private GameObject switchSecond;
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    [SerializeField] private Transform positionResult;
    [SerializeField] private Transform parentResult;
    private Dictionary<string, FoodReadyOven> _dictionaryProductName;
    private Heroik _heroik;
    
    private Outline _outline;
    private Oven script;

    [SerializeField] private ObjectsAndRecipes objectsAndRecipes;
    
    void Start()
    {
        _outline = GetComponent<Outline>();
        _dictionaryProductName = new Dictionary<string, FoodReadyOven>
        {
            { objectsAndRecipes.Meat.name, objectsAndRecipes.BakedMeat.GetComponent<FoodReadyOven>() },
            { objectsAndRecipes.Fish.name, objectsAndRecipes.BakedFish.GetComponent<FoodReadyOven>()},
            { objectsAndRecipes.Apple.name, objectsAndRecipes.BakedApple.GetComponent<FoodReadyOven>() },
            { objectsAndRecipes.Orange.name, objectsAndRecipes.BakedOrange.GetComponent<FoodReadyOven>() }
        };
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            if (!GetComponent<Oven>())
            {
                script = gameObject.AddComponent<Oven>();
                script.HeroikIsTrigger();
                script.Initialize( glassOn,  glassOff, switchFirst, switchSecond, timer, timerPoint, timerParent,_heroik,positionResult,parentResult,_dictionaryProductName);
            }
            else
            {
                script.HeroikIsTrigger();
                Debug.Log("Новый скрипт создан не был");
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        script.HeroikIsTrigger();
        _heroik = null;
        _outline.OutlineWidth = 0f;
        if (script.IsAllowDestroy())
        {
            Destroy(script);
            Debug.Log("скрипт был удален");
        }
    }
}
