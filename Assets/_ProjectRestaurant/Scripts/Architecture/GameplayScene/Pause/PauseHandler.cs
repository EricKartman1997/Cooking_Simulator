using System.Collections.Generic;
using Zenject;

public class PauseHandler: IHandlerPause
{
    private List<IPause> _handlers = new List<IPause>();
    private bool _isPaused;
    private IInputBlocker _inputBlocker;
    
    public bool IsPause => _isPaused;

    [Inject]
    private void ConstructZenject(IInputBlocker inputBlocker)
    {
        _inputBlocker = inputBlocker;
    }
    public void Add(IPause handler) => _handlers.Add(handler);
    public void Remove(IPause handler) => _handlers.Remove(handler);

    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
        
        if (isPaused == true)
        {
            _inputBlocker.Block(this);
        }
        else
        {
            _inputBlocker.Unblock(this);
        }
        
        foreach (IPause item in _handlers)
        {
            item.SetPause(isPaused);
        }
    }
}
