using UnityEngine;
using Zenject;

public class SoundsHandler : MonoBehaviour
{
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource music;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;

    public AudioSource Sfx => sfx;

    [Inject]
    private void ConstructZenject(LoadReleaseMainMenuScene loadReleaseMainMenuScene)
    {
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
    }

    private void Start()
    {
        SetMusic();
    }

    private void SetMusic()
    {
        music.clip = _loadReleaseMainMenuScene.AudioDic[AudioNameMainMenu.Background];
        music.Play();
    }
}
