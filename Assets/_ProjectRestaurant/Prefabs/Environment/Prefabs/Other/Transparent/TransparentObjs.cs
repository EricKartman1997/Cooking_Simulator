using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TransparentObjs : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float duration = 0.5f;

    private CancellationTokenSource _cts;


    private void Awake()
    {
        _cts = new CancellationTokenSource();
    }

    public async UniTask TransparentOn()
    {
        await FadeAlpha(1f);
    }

    public async UniTask TransparentOff()
    {
        await FadeAlpha(0f);
    }

    private async UniTask FadeAlpha(float targetAlpha)
    {
        _cts.Cancel();
        _cts = new CancellationTokenSource();

        Color startColor = material.color;
        float startAlpha = startColor.a;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            Color c = material.color;
            c.a = newAlpha;
            material.color = c;

            await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
        }

        // финальное значение
        Color final = material.color;
        final.a = targetAlpha;
        material.color = final;
    }
}
