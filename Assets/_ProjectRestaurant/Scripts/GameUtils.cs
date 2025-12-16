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
    public static async UniTask<bool> IsGoogleSheetsAvailable()
    {
        // Создаём HEAD-запрос (без загрузки тела ответа, быстрее чем GET)
        using (var request = UnityWebRequest.Head(
                   "https://sheets.googleapis.com"))
        {
            // Таймаут запроса (в секундах)
            request.timeout = 5;

            try
            {
                // Асинхронно отправляем запрос, не блокируя Unity
                await request.SendWebRequest();

                // Успешный результат означает, что Google Sheets доступен
                return request.result == UnityWebRequest.Result.Success;
            }
            catch
            {
                // Любая ошибка (нет интернета, таймаут, сбой сети)
                // считается отсутствием доступа
                return false;
            }
        }
    }

}
