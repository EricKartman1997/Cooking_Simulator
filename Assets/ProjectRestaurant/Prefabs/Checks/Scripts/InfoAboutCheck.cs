using UnityEngine;

public class InfoAboutCheck : MonoBehaviour
{
    [SerializeField] private float _score = 0f;
    [SerializeField] private GameObject _dish;

    public void Initialized(float score, GameObject dish)
    {
        _score = score;
        _dish = dish;
    }

    public float GetScore()
    {
        return _score;
    }
    public string GetDish()
    {
        return _dish.name;
    }

}
