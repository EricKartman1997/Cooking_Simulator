using UnityEngine;

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
}
