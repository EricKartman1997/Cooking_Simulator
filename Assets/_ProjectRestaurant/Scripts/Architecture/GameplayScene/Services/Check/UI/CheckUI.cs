using UnityEngine;
using TMPro;

public class CheckUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI remTimeText;
    private Check _check;
    
    public void Init(Check check)
    {
        _check = check;
    }
    
    private void Start()
    {
        costText.text = _check.Score.ToString();
    }

    private void Update()
    {
        remTimeText.text = string.Format("{0:00}:{1:00}", 0f,  _check.StartTime);
    }
}
