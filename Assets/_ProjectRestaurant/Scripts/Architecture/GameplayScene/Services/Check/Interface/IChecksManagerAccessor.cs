public interface IChecksManagerAccessor
{
    ChecksManager Manager { get; set; }
}

public class ChecksManagerAccessor : IChecksManagerAccessor
{
    public ChecksManager Manager { get; set; }
}