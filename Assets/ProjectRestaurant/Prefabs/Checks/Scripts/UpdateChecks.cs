using UnityEngine;

public class UpdateChecks : MonoBehaviour
{
    [SerializeField] private Checks _checks;
    [SerializeField] private float _timeUpdateCheck = 0f;
    [SerializeField] private float _timeAddNewCheck = 3f;

    private void Start()
    {
        _checks = GetComponent<Checks>();
    }

    private void Update()
    {
        _timeUpdateCheck += Time.deltaTime;
        if (_checks.GetCheck1() == null && _timeUpdateCheck >= _timeAddNewCheck) 
        {
            _checks.AddCheck();
            _timeAddNewCheck = 10f;
            _timeUpdateCheck = 0f;
            //Debug.Log("добавил 1 чек");
        }
        else if (_checks.GetCheck2() == null && _timeUpdateCheck >= _timeAddNewCheck)
        {
            _checks.AddCheck();
            _timeAddNewCheck = 15f;
            _timeUpdateCheck = 0f;
            //Debug.Log("добавил 2 чек");
        }
        else if (_checks.GetCheck3() == null && _timeUpdateCheck >= _timeAddNewCheck)
        {
            _checks.AddCheck();
            _timeUpdateCheck = 0f;
            //Debug.Log("добавил 3 чек");
        }
        else if(_checks.GetCheck1() != null && _checks.GetCheck2() != null && _checks.GetCheck3() != null)
        {
            _timeUpdateCheck = 0f;
            _timeAddNewCheck = 5f;
        }
    }
}
