using TMPro;
using UnityEngine;

public class InfoAboutCheck : MonoBehaviour
{
    [SerializeField] private float _score;
    [SerializeField] private GameObject _dish;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI remTimeText;
    [SerializeField] private float _startTime = 6f;

    public float StartTime => _startTime;

    public void Initialized(float score, GameObject dish)
    {
        _score = score;
        _dish = dish;
    }

    private void Start()
    {
        costText.text = _score.ToString();
        //remTimeText.text = startTime.ToString();
    }
    
    private void Update()
    {
        _startTime -= Time.deltaTime;
        if (_startTime <= 0)
        {
            Debug.Log("Чек удален");
            EventBus.DeleteCheck.Invoke(this);
        }
        remTimeText.text = string.Format("{0:00}:{1:00}", 0f,  _startTime);
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
