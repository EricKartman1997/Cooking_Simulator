using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class NotificationFactory
{
    private LoadReleaseGameplay _loadReleaseGameplay;
    
    [Inject]
    public NotificationFactory(LoadReleaseGameplay loadReleaseGameplay)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
    }
    
    public async UniTask GetNotificationReady(byte count, List<NotificationReady> notifications,GameObject parent)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Object.Instantiate(_loadReleaseGameplay.NotificationDic[NotificationEnum.Ready],
                parent.transform);
            obj.SetActive(false);
            notifications.Add(obj.GetComponent<NotificationReady>());
            await UniTask.Yield();
        }
    }
    
    public async UniTask GetNotificationImposible(byte count, List<NotificationImposible> notifications,GameObject parent)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Object.Instantiate(_loadReleaseGameplay.NotificationDic[NotificationEnum.Imposible],
                parent.transform);
            obj.SetActive(false);
            notifications.Add(obj.GetComponent<NotificationImposible>());
            await UniTask.Yield();
        }
    }
}
