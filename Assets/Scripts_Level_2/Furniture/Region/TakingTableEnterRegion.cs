using UnityEngine;

public class TakingTableEnterRegion : MonoBehaviour
{
    [SerializeField] private GameObject[] objectOnTheTable;
    
    private Heroik _heroik;
    private Outline _outline;
    private TakingTable script;

    void Start()
    {
        _outline = GetComponent<Outline>();
        foreach (var obj in objectOnTheTable)
        {
            obj.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            script = gameObject.AddComponent<TakingTable>();
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
