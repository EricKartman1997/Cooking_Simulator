using System;

public interface IForStove
{
    public Action CutletFire { get; set;}
    public bool IsFire { get; set; }
    
    public bool IsOnStove { get; set; }
    
    public EnumRoasting Roasting { get;}
    
    public float TimeCooking { get;}
    
    public float TimeRemaining { get;}




}
