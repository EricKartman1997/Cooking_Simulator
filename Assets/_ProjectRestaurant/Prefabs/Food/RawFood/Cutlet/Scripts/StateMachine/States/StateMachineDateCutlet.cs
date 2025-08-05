
public class StateMachineDateCutlet
{
  private EnumRoasting _roasting;
  private float _timeRemaining;
  private float _timeCooking;
  private bool _isOnStove;
  private bool _isFire;

  public EnumRoasting Roasting
  {
    get => _roasting;
    set => _roasting = value;
  }

  public float TimeRemaining
  {
    get => _timeRemaining;
    set => _timeRemaining = value;
  }
  
  public bool IsOnStove
  {
    get => _isOnStove;
  }
  
  public float TimeCooking
  {
    get => _timeCooking;
    set => _timeCooking = value;
  }
  
  public bool IsFire
  {
    get => _isFire;
    set => _isFire = value;
  }
}
