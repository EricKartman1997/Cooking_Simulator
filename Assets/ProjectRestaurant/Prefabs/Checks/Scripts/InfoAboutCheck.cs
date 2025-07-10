using System.Collections;
using TMPro;
using UnityEngine;

public class InfoAboutCheck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI remTimeText;
    [SerializeField] private float startTime;
    [SerializeField] private float score;
    [SerializeField] private GameObject dish;
    private Coroutine _updateTimeCoroutine;

    public float StartTime => startTime;

    private void Start()
    {
        costText.text = score.ToString();
        _updateTimeCoroutine = StartCoroutine(UpdateTime());

    }

    public float GetScore()
    {
        return score;
    }
    
    public string GetDish()
    {
        return dish.name;
    }
    
    public void StopUpdateTime()
    {
        if (_updateTimeCoroutine == null)
        {
            Debug.LogError("Ошибка корутина пуста");
            return;
        }
        
        StopCoroutine(_updateTimeCoroutine);
    }

    private IEnumerator UpdateTime()
    {
        while (startTime > 0)
        {
            startTime -= Time.deltaTime;
            remTimeText.text = string.Format("{0:00}:{1:00}", 0f,  startTime);
            yield return null;
        }
        Debug.Log("Чек удален");
        EventBus.DeleteCheck.Invoke(this);

    }
    
}
