using System.Collections.Generic;

public class PauseHandler: IHandlerPause
{
    private List<IPause> _handlers = new List<IPause>();

    public void Add(IPause handler) => _handlers.Add(handler);
    public void Remove(IPause handler) => _handlers.Remove(handler);

    public void SetPause(bool isPaused)
    {
        foreach (IPause item in _handlers)
        {
            item.SetPause(isPaused);
        }
    }
}
