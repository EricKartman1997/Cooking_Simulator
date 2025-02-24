using System;
using UnityEngine;
public class Garbage : MonoBehaviour
{
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private float _timeCurrent = 0.17f;
    private bool _heroikIsTrigger;
    
    public void Initialize(Heroik _heroik)
    {
        this._heroik = _heroik;
    }
    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && _heroikIsTrigger)
        {
            if(_timeCurrent >= 0.17f)
            {
                try
                { 
                    _heroik.GiveObjHands();
                }
                catch (Exception e)
                {
                    Debug.Log("Вам нечего выкидывать" + e);
                }
                _timeCurrent = 0f;
            }
            else
            {
                Debug.LogWarning("Ждите перезарядки кнопки");
            }
        }
    }
    
    public void HeroikIsTrigger()
    {
        _heroikIsTrigger = !_heroikIsTrigger;
    }

}
