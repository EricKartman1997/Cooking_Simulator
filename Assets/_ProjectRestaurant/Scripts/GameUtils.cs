using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class GameUtils
{
    //Этот метод проверяет, находится ли аниматор в состоянии с анимацией, и не достигло ли
    //нормализованное время конца анимации (но учтите: для зацикленных анимаций это не сработает,
    //так как они никогда не заканчиваются).
    public static bool IsAnimationPlaying(Animator animator, string animationName = null)
    {
        // Получаем информацию о текущем состоянии на слое 0
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
    
        // Если не указано имя анимации - проверяем любую анимацию
        if (string.IsNullOrEmpty(animationName))
        {
            // Проверяем, что анимация идет (нормализованное время меньше 1) и не находится в переходе
            return stateInfo.normalizedTime < 1f && !animator.IsInTransition(0);
        }
    
        // Если указано имя - проверяем именно эту анимацию
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1f && !animator.IsInTransition(0);
    }
    
    // Этот метод выполняет реальную проверку доступности Google Sheets,
    // отправляя HEAD-запрос к API Google Sheets.
    //
    // Используется для определения, появился ли интернет и доступен ли Google,
    // а не просто наличие сети (в отличие от Application.internetReachability).
    //
    // Метод:
    // - работает асинхронно (не блокирует главный поток Unity)
    // - имеет таймаут 5 секунд
    // - возвращает true, если Google Sheets доступен
    // - возвращает false при отсутствии интернета, блокировке,
    //   ошибке сети или таймауте
    //
    // Подходит для:
    // - периодической проверки соединения
    // - повторных попыток загрузки данных
    // - логики восстановления после оффлайн-старта игры
    public static async UniTask<bool> IsGoogleSheetAvailable(string sheetId)
    {
        // URL экспорта CSV для публичной таблицы
        string url = $"https://docs.google.com/spreadsheets/d/{sheetId}/export?format=csv";

        using (var request = UnityWebRequest.Head(url))
        {
            request.timeout = 5; // таймаут 5 секунд

            try
            {
                await request.SendWebRequest();

#if UNITY_2020_2_OR_NEWER
                return request.result == UnityWebRequest.Result.Success;
#else
            return !request.isNetworkError && !request.isHttpError;
#endif
            }
            catch
            {
                return false;
            }
        }
    }

}
