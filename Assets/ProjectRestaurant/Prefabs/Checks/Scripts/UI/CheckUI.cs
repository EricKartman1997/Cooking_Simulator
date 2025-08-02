using UnityEngine;
using TMPro;

public class CheckUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI remTimeText;
    private Coroutine _updateTimeCoroutine;
    private Check _check;
    
    public void Init(Check check)
    {
        _check = check;
        _check.StopTimeEvent += StopUpdateTime;
    }
    
    private void Start()
    {
        costText.text = _check.Score.ToString();
        _updateTimeCoroutine = StartCoroutine(_check.UpdateTime(remTimeText));
    }
    
    private void StopUpdateTime()
    {
        if (_updateTimeCoroutine == null)
        {
            Debug.LogError("Ошибка корутина пуста");
            return;
        }
        
        StopCoroutine(_updateTimeCoroutine);
    }

    // private IEnumerator UpdateTime()
    // {
    //     while (_startTime > 0)
    //     {
    //         _startTime -= Time.deltaTime;
    //         remTimeText.text = string.Format("{0:00}:{1:00}", 0f,  _startTime);
    //         yield return null;
    //     }
    //     Debug.Log("Чек удален");
    //     EventBus.DeleteCheck.Invoke(_check); // разобратся
    //
    // }
}
