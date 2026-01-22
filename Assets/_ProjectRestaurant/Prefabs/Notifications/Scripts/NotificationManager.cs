using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class NotificationManager: INotificationGetter
{
    private List<NotificationImposible> _notificationsImposible = new List<NotificationImposible>();
    private List<NotificationReady> _notificationsReady = new List<NotificationReady>();

    private NotificationObject _currentNotification;
    private NotificationFactory _notificationFactory;

    [Inject]
    public NotificationManager(NotificationFactory notificationFactory)
    {
        _notificationFactory = notificationFactory;
    }

    public async UniTask CreateNotifications(byte ready, byte impossible)
    {
        GameObject empty = new GameObject("Notifications_Test");
        
        await _notificationFactory.GetNotificationReady(ready, _notificationsReady,empty);
        await UniTask.Yield();
        await _notificationFactory.GetNotificationImposible(impossible, _notificationsImposible, empty);
        await UniTask.Yield();
    }
    
    public async UniTask GetNotification(Transform parent,bool isReady = false)
    {
        if (isReady)
        {
            foreach (var notification in _notificationsReady)
            {
                if (notification.IsPlaying == false)
                {
                    CloseNotification();
                    _currentNotification = notification;
                    notification.SetParentNotification(parent);
                    await UniTask.Yield();
                    return;
                }
            }
            Debug.LogWarning("Notification ready - свободных нет");
            await UniTask.Yield();
        }
        else
        {
            foreach (var notification in _notificationsImposible)
            {
                if (notification.IsPlaying == false)
                {
                    CloseNotification();
                    _currentNotification = notification;
                    notification.SetParentNotification(parent);
                    await UniTask.Yield();
                    return;
                }
            }
            Debug.LogWarning("NotificationImposible - свободных нет");
            await UniTask.Yield();
        }
    }

    private void CloseNotification()
    {
        if (_currentNotification == null)
            return;
        
        _currentNotification.SetFinishStateAnimation();
    }
    
    

}
