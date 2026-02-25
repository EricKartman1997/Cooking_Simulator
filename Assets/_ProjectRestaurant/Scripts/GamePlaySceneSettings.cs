using System.Collections.Generic;

public class GamePlaySceneSettings
{
    private byte _orders;
    private int _minutes;
    private int _seconds;
    private HashSet<CheckType> _dishSet;
    
    public byte Orders => _orders;
    public int Minutes => _minutes;
    public int Seconds => _seconds;
    
    public List<CheckType> DishList => new List<CheckType>(_dishSet);
    
    public GamePlaySceneSettings(GameSettings settings)
    {
        _orders = settings.Order;
        _minutes = settings.Minutes;
        _seconds = settings.Seconds;
        
        _dishSet = settings.FromDicToHashSet();
    }
}