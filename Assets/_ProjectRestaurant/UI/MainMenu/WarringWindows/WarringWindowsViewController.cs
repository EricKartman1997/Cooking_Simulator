using Michsky.MUIP;
using UnityEngine;

public class WarringWindowsViewController : MonoBehaviour
{
    private const string BOOLANIM = "IsShow";
    private const string UPDATEDATE = "Update date";
    private const string NOCONNECTION = "No connection";
    
    [SerializeField] private ModalWindowManager warringWindow;
    [SerializeField] private GameObject connectionTheInternet;
    [SerializeField] private GameObject waitTheInternetConnection;
    private Animator _animatorConnectionTheInternet;
    private Animator _animatorWaitTheInternetConnection;
    
    private ButtonManager _button;

    public ModalWindowManager WarringWindow => warringWindow;

    //public ButtonManager Button => _button;

    private void Awake()
    {
        _animatorWaitTheInternetConnection = waitTheInternetConnection.GetComponent<Animator>();
        _animatorConnectionTheInternet = connectionTheInternet.GetComponent<Animator>();
        _button = connectionTheInternet.GetComponentInChildren<ButtonManager>();
    }
    
    //WaitTheInternetConnection
    public void ShowWaitTheInternetConnection()
    {
        waitTheInternetConnection.SetActive(true);
        _animatorWaitTheInternetConnection.SetBool(BOOLANIM,true);
    }
    
    public void HideWaitTheInternetConnection()
    {
        waitTheInternetConnection.SetActive(true);
        _animatorWaitTheInternetConnection.SetBool(BOOLANIM,false);
    }
    
    //ConnectionTheInternet
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
        _button.UpdateUI();
    }
    
    public void NoConnectionTextButton()
    {
        _button.buttonText = NOCONNECTION;
        _button.UpdateUI();
    }
}
