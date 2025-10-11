using Michsky.MUIP;
using UnityEngine;

public class SettingsViewController : MonoBehaviour
{
    private const string OPEN = "Open";
    private const string CLOSE = "Close";
    
    [SerializeField] private ButtonManager buttonBack;
    [SerializeField] private Animator animator;
    
    private bool _isPlayAnim;

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
        if(_isPlayAnim == true)
            return;
        
        animator.SetTrigger(OPEN);
        _isPlayAnim = true;
    }
    
    public void ClosePanel()
    {
        if(_isPlayAnim == true)
            return;
        
        animator.SetTrigger(CLOSE);
        _isPlayAnim = true;
    }
    
    public void FinishAnim()
    {
        _isPlayAnim = false;
    }
    
}
