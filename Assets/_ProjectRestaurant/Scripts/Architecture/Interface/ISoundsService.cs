using UnityEngine;

public interface ISoundsService
{
    AudioSource SourceSfx {get;}

    AudioSource SourceMusic {get;}

    SoundManager SoundManager {get;}

    public void MuteSources();
    
    public void UnMuteSources();
}
