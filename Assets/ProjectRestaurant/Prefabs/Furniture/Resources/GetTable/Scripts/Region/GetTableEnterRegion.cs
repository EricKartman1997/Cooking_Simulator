using UnityEngine;

public class GetTableEnterRegion : MonoBehaviour
{
    [SerializeField] private GameObject objectOnTheTable;
    
    private Outline _outline;
    private Heroik _heroik;
    private GetTable script;

    private GameObject _objectOnTheTableCopy;
    
    void Start()
    {
        _outline = GetComponent<Outline>();
        objectOnTheTable.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            script = gameObject.AddComponent<GetTable>();
            script.Initialize(objectOnTheTable, _heroik);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = null;
            _outline.OutlineWidth = 0f;
            Destroy(script);
        }
    }
}
