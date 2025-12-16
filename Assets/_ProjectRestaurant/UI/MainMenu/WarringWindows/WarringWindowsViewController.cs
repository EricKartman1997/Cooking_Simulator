using Michsky.MUIP;
using UnityEngine;

public class WarringWindowsViewController : MonoBehaviour
{
    private const string BOOLANIM = "IsShow";
    private const string UPDATEDATE = "Update date";
    private const string NOCONNECTION = "NO connection";
    
    [SerializeField] private ModalWindowManager warringWindow;
    [SerializeField] private GameObject connectionTheInternet;
    private Animator _animatorConnectionTheInternet;
    
    private ButtonManager _button;

    public ModalWindowManager WarringWindow => warringWindow;

    //public ButtonManager Button => _button;

    private void Awake()
    {
        _animatorConnectionTheInternet = connectionTheInternet.GetComponent<Animator>();
        _button = connectionTheInternet.GetComponentInChildren<ButtonManager>();
    }
    
    public void ShowConnectionTheInternet()
    {
        _animatorConnectionTheInternet.SetBool(BOOLANIM,true);
    }
    
    public void HideConnectionTheInternet()
    {
        _animatorConnectionTheInternet.SetBool(BOOLANIM,false);
    }
    
    public void UpdateDateTextButton()
    {
        _button.buttonText = UPDATEDATE;
    }
    
    public void NoConnectionTextButton()
    {
        _button.buttonText = NOCONNECTION;
    }
}
