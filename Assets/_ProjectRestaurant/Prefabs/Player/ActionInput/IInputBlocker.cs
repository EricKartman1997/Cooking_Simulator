public interface IInputBlocker
{
    bool IsBlocked { get; }
    void Block(object source);
    void Unblock(object source);
}
