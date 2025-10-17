using UnityEngine;
using Zenject;

public class BootstrapBootScene : MonoBehaviour
{
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    [Inject]
    private void ConstructZenject(LoadReleaseGlobalScene loadReleaseGlobalScene)
    {
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
    }
    private async void Start()
    {
        //Debug.Log("запуск сцены");
        await _loadReleaseGlobalScene.LoadSceneAsync("SceneMainMenu");
        //Debug.Log("загрузил сцену");
    }

}
