using System.Collections.Generic;
using Zenject;

public class PauseHandler: IHandlerPause
{
    private List<IPause> _handlers = new List<IPause>();
    private bool _isPaused;
    private IInputBlocker _inputBlocker;
    private SoundsServiceGameplay _soundsServiceGameplay;
    
    public bool IsPause => _isPaused;

    [Inject]
    private void ConstructZenject(IInputBlocker inputBlocker,SoundsServiceGameplay soundsServiceGameplay)
    {
        _inputBlocker = inputBlocker;
        _soundsServiceGameplay = soundsServiceGameplay;
    }
    public void Add(IPause handler) => _handlers.Add(handler);
    public void Remove(IPause handler) => _handlers.Remove(handler);

    public void SetPause(bool isPaused, InputBlockType type)
    {
        _isPaused = isPaused;
        
        if (isPaused == true)
        {
            _inputBlocker.Block(this, type);
            _soundsServiceGameplay.PauseSFX();
        }
        else
        {
            _inputBlocker.Unblock(this, type);
            _soundsServiceGameplay.UnPauseSFX();
        }
        
        foreach (IPause item in _handlers)
        {
            item.SetPause(isPaused);
        }
    }
}
