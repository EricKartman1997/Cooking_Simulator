using System;

public class CheckPanelMediator : IDisposable
{
    private ChecksPanalUI _checksPanalUI;
    private ChecksManager _checksManager;

    public CheckPanelMediator(ChecksManager checksManager, ChecksPanalUI checksPanalUI )
    {
        _checksPanalUI = checksPanalUI;
        _checksManager = checksManager;
        _checksManager.AddCheckAction += OnAddCheckManager;
        _checksManager.RemoveCheckAction += OnRemoveCheckManager;
    }

    public void Dispose()
    {
        _checksManager.AddCheckAction -= OnAddCheckManager;
        _checksManager.RemoveCheckAction -= OnRemoveCheckManager;
    }

    private void OnAddCheckManager(Check check, ChecksFactory checksFactory, CheckType type) =>
        _checksPanalUI.AddCheck(check, checksFactory, type);


    private void OnRemoveCheckManager(Check check) => _checksPanalUI.RemoveCheck(check);
}
