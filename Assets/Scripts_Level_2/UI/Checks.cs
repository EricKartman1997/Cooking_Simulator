using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checks : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsImages;
    [SerializeField] private GameObject Content;
    
    [SerializeField] private List<GameObject> readyFood;
    public List<GameObject> ChecksList = new List<GameObject>(0); 
    
    private float _currentTime;
    private float _timeAddCheck = 3f;

    public  GameObject FirstCheck = null;
    public  GameObject SecondCheck = null;
    public  GameObject ThirdCheck = null;
    
    [SerializeField] private NumberOrders numberOrder;
    [SerializeField] private Level level;
    [SerializeField] private GameOver gameOver;
    [SerializeField] private TimeGame timeGame;
    [SerializeField] private Score score;
    void Start()
    {
        
    }
    void Update()
    {
        if (Heroik.MakeOrders == Level.NumberOrdersInLevel || (timeGame.GetSeconds() <= 0f && timeGame.GetMinutes() <= 0f) )
        {
            Debug.Log("Игра закончена, время больше не идет");
            Destroy(SecondCheck);
            Destroy(FirstCheck);
            Destroy(ThirdCheck);
            
            gameOver.ShowWindowScore();
            
            Time.timeScale = 0f;
            AudioListener.pause = true;
            
        }
        else
        {
            _currentTime += Time.deltaTime;
            if (FirstCheck == null && _currentTime >= _timeAddCheck)
            {
                //Debug.Log("поиск 1 чека");
                AddCheck(readyFood[Random.Range(0, readyFood.Count)],1);
                _timeAddCheck = _currentTime + 10f;
            }
            else if(SecondCheck == null && _currentTime >= _timeAddCheck)
            {
                //Debug.Log("поиск 2 чека");
                AddCheck(readyFood[Random.Range(0, readyFood.Count)],2);
                _timeAddCheck = _currentTime + 15f;
            }
            else if(ThirdCheck == null && _currentTime >= _timeAddCheck)
            {
                //Debug.Log("поиск 3 чека");
                AddCheck(readyFood[Random.Range(0, readyFood.Count)],3);
                _timeAddCheck = _currentTime + 15f;
            }
            else if(FirstCheck != null && SecondCheck != null && ThirdCheck != null)
            {
                _currentTime = 0f;
                _timeAddCheck = 5f;
            }
            else
            {
                //Debug.Log("Выполнены все условия");
            }
        }
    }

    private void AddCheck(GameObject check, byte number)
    {
        foreach (var pref in prefabsImages)
        {
            if(check.name == pref.name)
            {
                if (number == 1 && FirstCheck == null)
                {
                    //Debug.Log("создание 1 чека");
                    FirstCheck = Instantiate(pref,Content.transform) as GameObject;
                }
                else if (number == 2 && SecondCheck == null)
                {
                    //Debug.Log("создание 2 чека");
                    SecondCheck = Instantiate(pref,Content.transform) as GameObject;
                }
                else if (number == 3 && ThirdCheck == null)
                {
                    //Debug.Log("создание 3 чека");
                    ThirdCheck = Instantiate(pref,Content.transform) as GameObject;
                }
                else
                {
                    Debug.Log("неправильно введен number");
                }
            }
        }
    }
    public void DeleteCheck(GameObject food)
    {
        for (int i = 0; i < 1; i++)
        {
            if (FirstCheck != null)
            {
                if (FirstCheck.name == food.name + "(Clone)")
                {
                    score.AddScore(100,FirstCheck.GetComponent<InfoAboutCheck>().score);
                    Destroy(FirstCheck);
                    FirstCheck = null;
                    //Debug.Log("удалил первый чек");
                    Heroik.MakeOrders++;
                    numberOrder.UpdateOrders();
                    break;
                }
            }
            if (SecondCheck != null)
            {
                if (SecondCheck.name == food.name + "(Clone)")
                {
                    score.AddScore(100,SecondCheck.GetComponent<InfoAboutCheck>().score);
                    Destroy(SecondCheck);
                    SecondCheck = null;
                    //Debug.Log("удалил второй чек");
                    Heroik.MakeOrders++;
                    numberOrder.UpdateOrders();
                    break;
                }
            }
            if (ThirdCheck != null)
            {
                if (ThirdCheck.name == food.name + "(Clone)")
                {
                    score.AddScore(100,ThirdCheck.GetComponent<InfoAboutCheck>().score);
                    Destroy(ThirdCheck);
                    ThirdCheck = null;
                    //Debug.Log("удалил третий чек");
                    Heroik.MakeOrders++;
                    numberOrder.UpdateOrders();
                    break;
                }
            }
            else
            {
                Debug.Log("ошибка DeleteCheck");
                break;
            }
        }
        
    }
}


