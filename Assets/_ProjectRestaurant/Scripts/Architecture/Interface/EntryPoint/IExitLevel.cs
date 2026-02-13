using System;
using Cysharp.Threading.Tasks;

public interface IExitLevel
{
    event Action InitMenuButtons;
    
    UniTask ExitInMenuLevel();
    
}
