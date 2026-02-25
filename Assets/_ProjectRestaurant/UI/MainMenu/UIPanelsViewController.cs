using System.Collections.Generic;
using UnityEngine;

public class UIPanelsViewController : MonoBehaviour
{
    [SerializeField] private List<Canvas> canvasList;
    
    private void Awake()
    {
        foreach (var canvas in canvasList)
        {
            canvas.worldCamera = Camera.main;
        }
    }
}
