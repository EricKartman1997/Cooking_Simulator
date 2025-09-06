using Michsky.MUIP;
using UnityEngine;

public class SettingsViewController : MonoBehaviour
{
    private const string OPEN = "Open";
    private const string CLOSE = "Close";

    [SerializeField] private ButtonManager buttonBack;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        buttonBack.onClick.AddListener(ClosePanel);
    }

    private void OnDisable()
    {
        buttonBack.onClick.RemoveListener(ClosePanel);
    }

    public void OpenPanel()
    {
        animator.SetTrigger(OPEN);
    }
    
    public void ClosePanel()
    {
        animator.SetTrigger(CLOSE);
    }
    
}
