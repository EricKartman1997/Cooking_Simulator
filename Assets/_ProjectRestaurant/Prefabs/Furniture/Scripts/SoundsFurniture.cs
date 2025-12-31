using UnityEngine;
using Zenject;

public class SoundsFurniture : MonoBehaviour,IPause
{
    [SerializeField] private AudioSource audioSource;
    private LoadReleaseGameplay _loadReleaseGameplay;
    private AudioClip _audioClipCurrent;
    
    private IHandlerPause _pauseHandler;
    
    [Inject]
    private void ConstructZenject(LoadReleaseGameplay loadReleaseGameplay,IHandlerPause pauseHandler)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
    }

    private void OnDestroy()
    {
        _pauseHandler.Remove(this);
    }

    public void SetPause(bool isPaused)
    {
        if (isPaused == true)
        {
            audioSource.Pause();
            return;
        }
        audioSource.UnPause();
    }
    
    public void PlayClip(AudioNameGamePlay nameClip)
    {
        switch (nameClip)
        {
            case AudioNameGamePlay.SuvideSound: PlaySuvideSound();
                break;
            // case AudioNameGamePlay.RubbishSound: PlayRubbishSound();
            //     break;
            // case AudioNameGamePlay.StartOvenSound: PlayStartOvenSound();
            //     break;
            case AudioNameGamePlay.CuttingTableSound: PlayCuttingTableSound();
                break;
            case AudioNameGamePlay.WorkOvenSound: PlayWorkOvenSound();
                break;
            case AudioNameGamePlay.StoveSound: PlayStoveSound();
                break;
            case AudioNameGamePlay.BlenderSound: PlayBlenderSound();
                break;
            case AudioNameGamePlay.OvenSecondSound: PlayOvenSecondSound();
                break;
            case AudioNameGamePlay.TimerSound: PlayTimerSound();
                break;
            // case AudioNameGamePlay.TakeOnTheTableSound: PlayTakeOnTheTableSound();
            //     break;
            // case AudioNameGamePlay.PutOnTheTableSound: PlayPutOnTheTableSound();
            //     break;
            default:
                Debug.LogError("Клип указан не правильно");
                break;
        }
    }
    
    public void PlayOneShotClip(AudioNameGamePlay nameClip)
    {
        switch (nameClip)
        {
            case AudioNameGamePlay.RubbishSound: PlayOneShotRubbishSound();
                break;
            case AudioNameGamePlay.StartOvenSound: PlayOneShotStartOvenSound();
                break;
            case AudioNameGamePlay.TakeOnTheTableSound: PlayOneShotTakeOnTheTableSound();
                break;
            case AudioNameGamePlay.PutOnTheTableSound2: PlayOneShotPutOnTheTableSound();
                break;
            case AudioNameGamePlay.DistributionSound: PlayOneShotDistributionSound();
                break;
            case AudioNameGamePlay.PutTheBerryBlender: PlayOneShotPutTheBerryBlenderSound();
                break;
            case AudioNameGamePlay.PutTheWater: PlayOneShotPutTheWaterSound();
                break;
            default:
                Debug.LogError("Клип указан не правильно");
                break;
        }
    }

    public void StopCurrentClip()
    {
        if (_audioClipCurrent == null)
        {
            Debug.Log("Останавливать нечего клипа нет");
            return;
        }
        
        if (audioSource.isPlaying == false)
        {
            Debug.Log("Останавливать нечего клип не играет");
            return;
        }
        
        audioSource.Stop();
            
    }

    private void PlaySuvideSound()
    {
        if (audioSource.isPlaying ) //&& audioSource.clip == _loadReleaseGameplay.AudioDic[AudioNameGamePlay.SuvideSound]
        {
            Debug.Log("дубляж клипа остановлен");
            return;
        }
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.SuvideSound];
        audioSource.clip = _audioClipCurrent;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    private void PlayCuttingTableSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.CuttingTableSound];
        audioSource.clip = _audioClipCurrent;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    private void PlayWorkOvenSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.WorkOvenSound];
        audioSource.clip = _audioClipCurrent;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    private void PlayStoveSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.StoveSound];
        audioSource.clip = _audioClipCurrent;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    private void PlayBlenderSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.BlenderSound];
        audioSource.clip = _audioClipCurrent;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    private void PlayOvenSecondSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.OvenSecondSound];
        audioSource.clip = _audioClipCurrent;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    private void PlayTimerSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.TimerSound];
        audioSource.clip = _audioClipCurrent;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    private void PlayOneShotTakeOnTheTableSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.TakeOnTheTableSound];
        //audioSource.clip = _audioClipCurrent;
        //audioSource.loop = false;
        audioSource.PlayOneShot(_audioClipCurrent);
    }
    
    private void PlayOneShotPutOnTheTableSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.PutOnTheTableSound2];
        //audioSource.clip = _audioClipCurrent;
        //audioSource.loop = false;
        audioSource.PlayOneShot(_audioClipCurrent);
    }

    private void PlayOneShotDistributionSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.DistributionSound];
        audioSource.PlayOneShot(_audioClipCurrent);
    }
    
    private void PlayOneShotRubbishSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.RubbishSound];
        //audioSource.clip = _audioClipCurrent;
        //audioSource.loop = true;
        audioSource.PlayOneShot(_audioClipCurrent);
    }
    
    private void PlayOneShotStartOvenSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.StartOvenSound];
        //audioSource.clip = _audioClipCurrent;
        //audioSource.loop = true;
        audioSource.PlayOneShot(_audioClipCurrent);
    }
    
    private void PlayOneShotPutTheBerryBlenderSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.PutTheBerryBlender];
        //audioSource.clip = _audioClipCurrent;
        //audioSource.loop = true;
        audioSource.PlayOneShot(_audioClipCurrent);
    }
    
    private void PlayOneShotPutTheWaterSound()
    {
        _audioClipCurrent = _loadReleaseGameplay.AudioDic[AudioNameGamePlay.PutTheWater];
        //audioSource.clip = _audioClipCurrent;
        //audioSource.loop = true;
        audioSource.PlayOneShot(_audioClipCurrent);
    }

    
}
