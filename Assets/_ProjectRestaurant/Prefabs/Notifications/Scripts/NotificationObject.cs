using UnityEngine;

public abstract class NotificationObject : MonoBehaviour
{
    private const string FINISHFAST = "FinishFast";
    private Animator _animator;
    private bool _isPlaying;
    
    public bool IsPlaying => _isPlaying;
    
    private void Awake()
    {
        _animator =  GetComponent<Animator>();
        //_isPlaying = true;
    }

    private void OnDisable()
    {
        _isPlaying = false;
    }

    public void SetParentNotification(Transform parent)
    {
        _isPlaying = true;
        gameObject.SetActive(true);
        gameObject.transform.SetParent(parent);
    }

    public void SetFinishStateAnimation()
    {
        _animator.SetTrigger(FINISHFAST);
    }

    public void FinishAnimation()
    {
        _isPlaying = false;
        gameObject.SetActive(false);
    }
}
