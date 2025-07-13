using UnityEngine;

public class UpdateBootstrap : MonoBehaviour
{
    private FieldsForScriptContainer _FC;
    private BootstrapLVL2 _bootstrapLvl2;

    private void Start()
    {
        _bootstrapLvl2 = StaticManagerWithoutZenject.BootstrapLVL2;
    }
    
    private void Update()
    {
        _bootstrapLvl2.TimeGame.Update();
        _bootstrapLvl2.UpdateChecks.Update();
    }
}
