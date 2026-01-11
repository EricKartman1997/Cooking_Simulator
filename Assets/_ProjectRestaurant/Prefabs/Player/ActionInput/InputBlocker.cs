using System.Collections.Generic;

public class InputBlocker : IInputBlocker
{
    private readonly HashSet<object> _blockers = new();

    public bool IsBlocked => _blockers.Count > 0;

    public void Block(object source)
    {
        _blockers.Add(source);
    }

    public void Unblock(object source)
    {
        _blockers.Remove(source);
    }
}