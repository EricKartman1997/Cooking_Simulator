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
        await _loadReleaseGlobalScene.LoadSceneAsync("SceneGamePlay");
    }

}
