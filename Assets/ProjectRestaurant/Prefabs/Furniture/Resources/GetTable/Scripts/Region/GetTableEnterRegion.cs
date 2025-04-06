using UnityEngine;

public class GetTableEnterRegion : MonoBehaviour
{
    [SerializeField] private GetTableConfig getTableConfig;
    [SerializeField] private Transform parentViewDish;
    [SerializeField] private Transform parentViewTable;
    
    private Outline _outline;
    private Heroik _heroik;
    private GetTable script;
    
    void Start()
    {
        _outline = GetComponent<Outline>();
        StaticManagerWithoutZenject.ViewFactory.GetProduct(getTableConfig.FoodView,parentViewDish);
        StaticManagerWithoutZenject.ViewFactory.GetViewTable(getTableConfig.ViewTable,parentViewTable);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            script = gameObject.AddComponent<GetTable>();
            script.Initialize(_heroik,getTableConfig);
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
