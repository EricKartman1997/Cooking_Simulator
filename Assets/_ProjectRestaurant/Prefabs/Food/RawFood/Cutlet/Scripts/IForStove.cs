using System;

public interface IForStove
{
    public Action PlaySoundAction { get; set; }
    public Action StopSoundAction { get; set;}
    public bool IsFire { get; set; }
    
    public bool IsOnStove { get; set; }
    
    public EnumRoasting Roasting { get;}
    
    public float TimeCooking { get;}
    
    public float TimeRemaining { get;}




}
