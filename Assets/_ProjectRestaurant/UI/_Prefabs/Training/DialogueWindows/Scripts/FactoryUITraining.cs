using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryUITraining
{
    private DiContainer _container;
    private LoadReleaseTraining _loadRelease;
    private GameObject empty = new GameObject("UI_TestDialogue");
    
    private EndDialogueUI _endDialogue;
    private StartDialogueUI _startDialogue;
    private TaskDialogueUI _taskDialogue;
    private MiniTaskDialogueUI _miniTaskDialogue;

    public EndDialogueUI EndDialogue => _endDialogue;

    public StartDialogueUI StartDialogue => _startDialogue;

    public TaskDialogueUI TaskDialogue => _taskDialogue;

    public MiniTaskDialogueUI MiniTaskDialogue => _miniTaskDialogue;


    public FactoryUITraining(DiContainer container, LoadReleaseTraining loadRelease,ISoundsService soundsService)
    {
        _container = container;
        _loadRelease = loadRelease;
    }
    
    public void CreateStartDialogue()
    {
        GameObject obj = _container.InstantiatePrefab(_loadRelease.UINameDic[UINameTraining.StartTraining], empty.transform);
        obj.GetComponent<Canvas>().worldCamera = Camera.main;
        _startDialogue = obj.GetComponentInChildren<StartDialogueUI>(true);
        _startDialogue.gameObject.SetActive(false);
    }
    
    public void CreateEndDialogue()
    {
        GameObject obj = _container.InstantiatePrefab(_loadRelease.UINameDic[UINameTraining.EndTraining], empty.transform);
        obj.GetComponent<Canvas>().worldCamera = Camera.main;
        _endDialogue = obj.GetComponentInChildren<EndDialogueUI>(true);
        _endDialogue.gameObject.SetActive(false);
    }
    
    public void CreateTaskDialogue()
    {
        GameObject obj = _container.InstantiatePrefab(_loadRelease.UINameDic[UINameTraining.TaskTraining], empty.transform);
        obj.GetComponent<Canvas>().worldCamera = Camera.main;
        _taskDialogue = obj.GetComponentInChildren<TaskDialogueUI>(true);
        _taskDialogue.gameObject.SetActive(false);
    }
    
    public void CreateMiniTaskDialogue()
    {
        GameObject obj = _container.InstantiatePrefab(_loadRelease.UINameDic[UINameTraining.MiniTaskTraining], empty.transform);
        obj.GetComponent<Canvas>().worldCamera = Camera.main;
        _miniTaskDialogue = obj.GetComponentInChildren<MiniTaskDialogueUI>(true);
        _miniTaskDialogue.gameObject.SetActive(false);
    }
}
