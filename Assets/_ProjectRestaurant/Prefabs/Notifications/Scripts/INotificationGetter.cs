using Cysharp.Threading.Tasks;
using UnityEngine;

public interface INotificationGetter
{
    UniTask GetNotification(Transform parent,bool isReady = false);
}
