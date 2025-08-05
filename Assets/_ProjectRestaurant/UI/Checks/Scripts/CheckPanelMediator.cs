using System;

public class CheckPanelMediator : IDisposable
{
    private ChecksPanalUI _checksPanalUI;
    private Checks _checks;

    public CheckPanelMediator(Checks checks, ChecksPanalUI checksPanalUI )
    {
        _checksPanalUI = checksPanalUI;
        _checks = checks;
        _checks.AddCheckAction += OnAddCheck;
        _checks.RemoveCheckAction += OnRemoveCheck;
    }

    public void Dispose()
    {
        _checks.AddCheckAction -= OnAddCheck;
        _checks.RemoveCheckAction -= OnRemoveCheck;
    }

    private void OnAddCheck(Check check, ChecksFactory checksFactory, CheckType type) =>
        _checksPanalUI.AddCheck(check, checksFactory, type);


    private void OnRemoveCheck(Check check) => _checksPanalUI.RemoveCheck(check);
}
