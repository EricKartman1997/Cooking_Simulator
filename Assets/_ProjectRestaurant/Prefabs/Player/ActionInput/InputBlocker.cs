using System.Collections.Generic;

public class InputBlocker : IInputBlocker
{
    private readonly Dictionary<InputBlockType, HashSet<object>> _blocks = new();

    public bool IsBlocked(InputBlockType type)
    {
        foreach (var pair in _blocks)
        {
            if ((pair.Key & type) != 0 && pair.Value.Count > 0)
                return true;
        }
        return false;
    }

    public void Block(object source, InputBlockType type)
    {
        if (!_blocks.TryGetValue(type, out var set))
        {
            set = new HashSet<object>();
            _blocks[type] = set;
        }

        set.Add(source);
    }

    public void Unblock(object source, InputBlockType type)
    {
        if (_blocks.TryGetValue(type, out var set))
        {
            set.Remove(source);
        }
    }
}