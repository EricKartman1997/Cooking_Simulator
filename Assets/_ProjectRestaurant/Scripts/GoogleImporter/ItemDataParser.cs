using GoogleSpreadsheets;

public class ItemDataParser: IGoogleSheetParser
{
    protected StorageData StorageData;
    
    public virtual  string NameSheet { get; }
    
    
    public void Init(StorageData storageData)
    {
        StorageData = storageData;
    }
    
    public virtual void Parse(string header, string token)
    {
        
    }
}
