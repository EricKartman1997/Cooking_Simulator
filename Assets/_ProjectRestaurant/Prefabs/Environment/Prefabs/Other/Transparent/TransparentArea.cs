using Cysharp.Threading.Tasks;
using UnityEngine;

public class TransparentArea : MonoBehaviour
{
    private TransparentObjs[] _objsList;
    private Transform _parent;

    public void Init(Transform parent)
    {
        _parent = parent;
    }
    
    void Start()
    {
        _objsList = _parent.GetComponentsInChildren<TransparentObjs>();
        //Debug.Log("закончил поиск");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_objsList.Length == 0)
            Debug.LogWarning("список пуст");
        
        if (other.GetComponent<Heroik>())
        {
            foreach (var obj in _objsList)
            {
                obj.TransparentOff().Forget();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_objsList.Length == 0)
            Debug.LogWarning("список пуст");
        
        if (other.GetComponent<Heroik>())
        {
            foreach (var obj in _objsList)
            {
                obj.TransparentOn().Forget();
            }
        }
    }
}
