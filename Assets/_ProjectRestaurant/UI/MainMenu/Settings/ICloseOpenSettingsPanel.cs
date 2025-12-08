public interface ICloseOpenSettingsPanel
{
    public bool IsOpen { get; }
    public void OpenPanel();
    
    public void ClosePanel();

}