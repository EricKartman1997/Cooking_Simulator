using System;

public interface IInputBlocker
{
    bool IsBlocked(InputBlockType type);

    void Block(object source, InputBlockType type);
    void Unblock(object source, InputBlockType type);
}


[Flags]
public enum InputBlockType
{
    None      = 0,
    Movement  = 1 << 0, // WSAD
    OnPressE  = 1 << 1, // E
    Menu      = 1 << 2, // Esc
    All       = Movement | OnPressE | Menu
}

