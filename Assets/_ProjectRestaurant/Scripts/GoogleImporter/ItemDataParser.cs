using GoogleSpreadsheets;

public class ItemDataParser: IGoogleSheetParser
{
    protected IStorageJson JsonHandler;
    
    public virtual  string NameSheet { get; }
    
    
    public void Init(IStorageJson jsonHandler)
    {
        JsonHandler = jsonHandler;
    }
    
    public virtual void Parse(string header, string token)
    {
        
    }

    public virtual void Save()
    {
        
    }
    
}
